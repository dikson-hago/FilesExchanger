open System.Text.Json
open FilesExchanger.Application.FilesConvertor
open FilesExchanger.Application.IpConvertor
open FilesExchanger.Connector.Models
open FilesExchanger.NetworkTools

let targetFolder = "/home/user12/Documents/Projects/git/FilesExchanger/test/testTo"
let localIp = "127.0.0.1"
let localPort = 8082

[<EntryPoint>]
let main _ =
    let res = IpAddressConvertor.encrypt localIp localPort
    printfn $"address: {res}"
    
    for i in 1 .. 100 do 
        let ws = WebSocketNetworkContext()
        let res = ws.GetModel localIp localPort
        
        if res.MessageType = WsMessageType.FirstConnection then
            printfn $"get message: {res.StringMessage}"
        else
            let fileName = res.StringMessage
            let filePath = FilesConvertorContext.ConcatFolderAndFile targetFolder fileName
            FilesConvertorContext.BytesToFile filePath res.ByteMessage
        
    (*let res = IpAddressConvertor.encrypt localIp localPort
    printfn $"address: {res}"
    let (ip, port) = IpAddressConvertor.decrypt res*)
    ///startWebServer {defaultConfig with logger = Targets.create Verbose [||] } app
    //startWebServer myCfg app
    0