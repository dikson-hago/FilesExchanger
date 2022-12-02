namespace FilesExchanger.Connector.WebSocketsClient

open System
open System.Net.WebSockets
open System.Text
open System.Threading

type WebSocketClientContext (url) =
    
    let ws = new ClientWebSocket()     
    let lockConnection = Object()

    let connect() =
        lock lockConnection ( fun () ->
            if not (ws.State = WebSocketState.Open) then
                ws.ConnectAsync(Uri(url), CancellationToken.None) 
                |> Async.AwaitTask |> Async.RunSynchronously // await
            else ()
        ) 

    let receive () =  
        lock lockConnection ( fun () ->
            let rec readStream finalText endOfMessage =
                let buffer = ArraySegment(Array.zeroCreate<byte> 1024)
                let result = ws.ReceiveAsync(buffer, CancellationToken.None) |> Async.AwaitTask |> Async.RunSynchronously

                let text = finalText + Encoding.UTF8.GetString (buffer.Array |> Array.take result.Count)
                if result.EndOfMessage then text
                else readStream text true
            readStream "" false
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

