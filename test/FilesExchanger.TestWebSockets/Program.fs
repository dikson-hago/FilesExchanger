open System
open FilesExchanger.Application.FilesConvertor
open FilesExchanger.Host.Handlers.Models
open FilesExchanger.NetworkTools.Models

open FilesExchanger.Application.IpConvertor
open FilesExchanger.NetworkTools
open FilesExchanger.Tools.CryptographyTools.Rsa

[<EntryPoint>]
let main _ =
    
    let filePath = "/home/user12/Documents/Projects/git/FilesExchanger/test/testFrom/testFrom.txt"
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
                StringMessage = deviceName;
                ByteMessage = Array.empty
                ByteEncryptMessage = Array.empty
                BigIntArrMessage = Array.empty;
                MessageType = WsMessageType.FirstConnection
            }
            let res = ws.SendModel model address
            let (e, n) = (res.BigIntArrMessage.[0], res.BigIntArrMessage.[1])
            
            RsaKeysInfo.setExternalKeys e n |> ignore
        else
            let ws = WebSocketNetworkContext()
            
            // get bytes of file
            let bytes = FilesConvertorContext.FileToBytes filePath
 
            let (e, n) = RsaKeysInfo.getExternalKeysForEncrypt()
            let encryptBytes = EncryptorContext.RsaContext.Encrypt e n bytes
            
            let model = {
                StringMessage = "testFrom.txt";
                ByteMessage = Array.empty
                ByteEncryptMessage = encryptBytes
                BigIntArrMessage = Array.empty;
                MessageType = WsMessageType.File
            }
            
            ws.SendModel model address |> ignore

    0


