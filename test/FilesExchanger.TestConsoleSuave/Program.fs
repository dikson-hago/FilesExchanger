open FilesExchanger.Application.FilesConvertor
open FilesExchanger.Application.IpConvertor
open FilesExchanger.Host.Handlers.Models
open FilesExchanger.NetworkTools.Models
open FilesExchanger.NetworkTools
open FilesExchanger.Tools.CryptographyTools.Rsa

let targetFolder = "/home/user12/Documents/Projects/git/TestFolder/ToFolder"
let localIp = "127.0.0.1"
let localPort = 8082

let initRsa() =
    let (e, d, n) = Rsa.init()
    RsaKeysInfo.setOwnKeys e d n
    0
        
let InitConnectionResponseForKeys() =
        let (e, n) = RsaKeysInfo.getOwnKeysForEncrypt()
        let model = {
            StringMessage = "";
            ByteMessage = Array.empty
            ByteEncryptMessage = Array.empty
            BigIntArrMessage = [|e; n|]
            MessageType = WsMessageType.Key
        }
        model
        
let InitConnectionResponseForFile bytes =
        let (e, n) = RsaKeysInfo.getOwnKeysForEncrypt()
        
        let encryptMessage = EncryptorContext.RsaContext.Encrypt e n bytes
        
        let model = {
            StringMessage = "";
            ByteMessage = Array.empty
            ByteEncryptMessage = encryptMessage
            BigIntArrMessage = Array.empty
            MessageType = WsMessageType.File
        }
        model
        
        
let TestSimpleConnection _=
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
            
            // encrypt bytes
            let encryptBytes = res.ByteEncryptMessage
            let (d, n) = RsaKeysInfo.getOwnKeysForDecrypt()
            
            let bytes = EncryptorContext.RsaContext.Decrypt d n encryptBytes
            
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