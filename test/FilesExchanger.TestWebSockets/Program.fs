open System
open System.Collections.Generic
open FilesExchanger.Application.CompressionTools
open FilesExchanger.Application.FilesConvertor
open FilesExchanger.Host.Handlers.Models

open FilesExchanger.Application.IpConvertor
open FilesExchanger.NetworkTools
open FilesExchanger.Tools.CompressionTools.Haffman
open FilesExchanger.Tools.CryptographyTools.Rsa
open FilesExchanger.NetworkTools.Models

let CompressBytes fileBytes =
        let (bitList, codesInfo) = CompresstionContext.HaffmanContext.compress fileBytes
        
        let codesFinalInfo = BitListConverter.ToBitListInfo codesInfo
        
        let (bytes, amount) = bitList.getCompressionInfo()
        
        let compressionInfo = {
            CodesInfoJson = codesFinalInfo;
            BitsAmount = amount
        }
        
        (bytes |> List.toArray, compressionInfo)
            
let EncryptBytes (bytes: byte[]) =
    let (e, n) = RsaKeysInfo.getExternalKeysForEncrypt()
    let encryptedBytes = EncryptorContext.RsaContext.Encrypt2 e n bytes
    
    let encryptionInfo = {
        E = e
        N = n
    }
    
    (encryptedBytes, encryptionInfo)
    
let ConvertToSendFilesMessage (bytes : byte[]) (compressionInfo : CompressionInfoType)
                    (encryptionInfo : EncryptionInfoType) (fileName : string) =
        let model = {
            ByteMessage = bytes;
            CompressionInfo = compressionInfo
            EncryptionInfo = encryptionInfo;
            StringMessage = fileName;
            MessageType = WsMessageType.File
        }
        model

[<EntryPoint>]
let main _ =
    
    let filePath = "/home/user12/Documents/Projects/git/FilesExchanger/test/testFrom/testFrom.txt"
    let fileName = "testFrom.txt"
    let localIp = "127.0.1.1"
    let localPort = 8082
    let deviceName = IpAddressConvertor.encrypt localIp localPort
    
    let address = "ws://127.0.1.1:8082"
    for i in 1..100 do
        printfn "print code:"
        let code = Convert.ToInt32(Console.ReadLine())
        
        if code = 1 then
            let ws = WebSocketNetworkContext()
            let model = {
                ByteMessage = Array.empty;
                CompressionInfo = {CodesInfoJson = Dictionary<byte, BitsListInfo>(); BitsAmount = 0}
                EncryptionInfo = {E = 0; N = 0};
                StringMessage = deviceName;
                MessageType = WsMessageType.FirstConnection
            }
            let res = ws.SendModel model address
            let (e, n) = (res.EncryptionInfo.E, res.EncryptionInfo.N)
            
            RsaKeysInfo.setExternalKeys e n |> ignore
        else
            let ws = WebSocketNetworkContext()
            
            let filesBytes = FilesConvertorContext.FileToBytes filePath
            
            // encrypt bytes
            let (encryptedBytes, encryptionInfo) = EncryptBytes filesBytes
            
            // compress bytes
            let (compressedBytes, compressionInfo) = CompressBytes encryptedBytes
            
            let model = ConvertToSendFilesMessage compressedBytes
                                   compressionInfo encryptionInfo fileName
            
            ws.SendModel model address |> ignore

    0


