open System.Collections.Generic
open FilesExchanger.Application.CompressionTools
open FilesExchanger.Application.FilesConvertor
open FilesExchanger.Application.IpConvertor
open FilesExchanger.Host.Handlers.Models
open FilesExchanger.Host.Handlers.Models.EncryptorContext
open FilesExchanger.NetworkTools.Models
open FilesExchanger.NetworkTools
open FilesExchanger.Tools.CompressionTools.Haffman
open FilesExchanger.Tools.CryptographyTools.Rsa

let targetFolder = "/home/user12/Documents/Projects/git/TestFolder/ToFolder"
let localIp = "127.0.0.1"
let localPort = 8082

let initRsa() =
    let (e, d, n) = RsaContext.InitRsa()
    RsaKeysInfo.setOwnKeys e d n
    0
        
let InitConnectionResponseForKeys() =
        let (e, n) = RsaKeysInfo.getOwnKeysForEncrypt()
        let model = {
            ByteMessage = Array.empty
            CompressionInfo = {CodesInfoJson = Dictionary<byte, BitsListInfo>(); BitsAmount = 0}
            EncryptionInfo = {E = e; N = n}
            StringMessage = "";
            MessageType = WsMessageType.Key
        }
        model
        
let InitConnectionResponseForFile bytes =
        let (e, n) = RsaKeysInfo.getOwnKeysForEncrypt()
        
        let encryptMessage = EncryptorContext.RsaContext.Encrypt e n bytes
        
        let model = {
            ByteMessage = Array.empty
            CompressionInfo = {CodesInfoJson = Dictionary<byte, BitsListInfo>(); BitsAmount = 0};
            EncryptionInfo = {E = 0; N = 0}
            StringMessage = ""
            MessageType = WsMessageType.StatusOK
        }
        model
    
let DecompressBytes (bytes : byte[]) (compressionInfo : CompressionInfoType) =
        let bitsList = BitsList()
        bitsList.setCompressionInfo (bytes |> Array.toList) compressionInfo.BitsAmount
        
        let codesInfoBitsList = BitListConverter.ToBitList compressionInfo.CodesInfoJson
        
        let res = CompresstionContext.HaffmanContext.decompress bitsList codesInfoBitsList
        res
        
let DecryptBytes (bytes : byte[]) =
        let (d, n) = RsaKeysInfo.getOwnKeysForDecrypt()
        
        let decryptBytes = EncryptorContext.RsaContext.Decrypt2 d n bytes
        
        decryptBytes
        
let TestSimpleConnection _ =
    let res = IpAddressConvertor.encrypt localIp localPort
    printfn $"address: {res}"
    initRsa() |> ignore
    
    for i in 1 .. 100 do
        let connectionResponse = InitConnectionResponseForKeys()
        
        let ws = WebSocketNetworkContext()
        let res = ws.GetModel localIp localPort connectionResponse
        
        if res.MessageType = WsMessageType.FirstConnection then
            printfn $"get message: {res.StringMessage}"
        else
            // get file name
            let fileName = res.StringMessage
            
            // decompress bytes
            let decompressedBytes = DecompressBytes res.ByteMessage res.CompressionInfo
            
            // encrypt bytes
            let bytes = DecryptBytes (decompressedBytes |> List.toArray)
            
            let filePath = FilesConvertorContext.ConcatFolderAndFile targetFolder fileName
            FilesConvertorContext.BytesToFile filePath bytes

[<EntryPoint>]
let main _ =
    
    TestSimpleConnection() |> ignore
    (*let res = IpAddressConvertor.encrypt localIp localPort
    printfn $"address: {res}"
    let (ip, port) = IpAddressConvertor.decrypt res*)
    ///startWebServer {defaultConfig with logger = Targets.create Verbose [||] } app
    //startWebServer myCfg app
    0