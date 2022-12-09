open System
open System.Net.WebSockets
open System.Text
open System.Threading
open FilesExchanger.Application.FilesConvertor
open FilesExchanger.Connector.Models

open FilesExchanger.Application.IpConvertor
open FilesExchanger.NetworkTools

(*type WebSocketClientContext (url) =
    
    let ws = new ClientWebSocket()     
    let lockConnection = Object()

    let connect() =
        lock lockConnection ( fun () ->
            if not (ws.State = WebSocketState.Open) then
                ws.ConnectAsync(Uri(url), CancellationToken.None) 
                    |> Async.AwaitTask
                    |> Async.RunSynchronously // await
            else ()
        ) 

    let receive () =  
        lock lockConnection ( fun () ->
            let rec readStream finalText endOfMessage =
                let buffer = ArraySegment(Array.zeroCreate<byte> 1024)
                
                let result = ws.ReceiveAsync(buffer, CancellationToken.None)
                                 |> Async.AwaitTask
                                 |> Async.RunSynchronously

                let text = finalText
                           + Encoding.UTF8.GetString (buffer.Array |> Array.take result.Count)
                
                if result.EndOfMessage then text
                else readStream text true
            readStream "" false
        )
        
    let receiveBytes () =  
        lock lockConnection ( fun () ->
            let rec readStream allBytes =
                let buffer = ArraySegment(Array.zeroCreate<byte> 1024)
                
                let result = ws.ReceiveAsync(buffer, CancellationToken.None)
                                 |> Async.AwaitTask
                                 |> Async.RunSynchronously

                let bytesBacket = (buffer.Array |> Array.take result.Count) |> Array.toList
                
                if result.EndOfMessage then (allBytes @ bytesBacket)
                else readStream (allBytes @ bytesBacket)

            (readStream List.empty) |> List.toArray
        )

    let sendRequest bytes =
        let bytesMessage = ArraySegment(bytes, 0, bytes.Length)

        if not (ws.State = WebSocketState.Open) then connect()
        
        // send request
        ws.SendAsync(bytesMessage, WebSocketMessageType.Text, true, CancellationToken.None)
                        |> Async.AwaitTask |> Async.RunSynchronously
        // read response
        receive()

    member this.SendRequest request = sendRequest request
    
    member this.SendString (str : string) =
                   let bytes = Encoding.UTF8.GetBytes(str)
                   sendRequest bytes
                   
    member this.GetBytes _ = receiveBytes()
    
    
let initAlph =
            let mutable res = []
            for i in 'a'..'z' do res <- i :: res
            for i in 'A'..'Z' do res <- i :: res
            for i in '0'..'9' do res <- i :: res
            res*)
[<EntryPoint>]
let main _ =
    //printfn "%A" initAlph
    (*let res = IpAddressConvertor.encrypt "127.0.0.1" 12
    printfn $"%s{res}"
    
    let (ip, port) = IpAddressConvertor.decrypt res
    
    printfn $"%s{ip} %d{port}"*)
    
    
    let filePath = "/home/user12/Documents/Projects/git/FilesExchanger/test/testFrom/testFrom.txt"
    let localIp = "127.0.0.1"
    let localPort = 8082
    let deviceName = IpAddressConvertor.encrypt localIp localPort
    
    let address = "ws://127.0.0.1:8082"
    for i in 1..100 do
        printfn "print code:"
        let code = Convert.ToInt32(Console.ReadLine())
        
        if code = 1 then
            let ws = WebSocketNetworkContext()
            let model = {
                StringMessage = deviceName;
                ByteMessage = Array.empty
                BigIntArrMessage = Array.empty;
                MessageType = WsMessageType.FirstConnection
            }
            ws.SendModel model address
            
        else
            let ws = WebSocketNetworkContext()
            let bytes = FilesConvertorContext.FileToBytes filePath

            let model = {
                StringMessage = "testFrom.txt";
                ByteMessage = bytes
                BigIntArrMessage = Array.empty;
                MessageType = WsMessageType.File
            }
            ws.SendModel model address
    0


