namespace FilesExchanger.Connector.Suave

open System.Text
open System.Threading
open Suave
open Suave.Sockets
open Suave.Sockets.Control
open Suave.WebSocket

module SuaveContext =    
    let mutable gotBytes = Array.empty
    
    let mutable cts = new CancellationTokenSource()

    let ws (webSocket : WebSocket) (context: HttpContext) =
        socket {
          let mutable loop = true
          
          while loop do
                let! msg = webSocket.read()

                match msg with
                | (Text, data, true) ->
                    gotBytes <- data
                    
                    //printfn "get string: %s" (data |> Encoding.UTF8.GetString)
                    
                    // send response
                    let response = "ok from host"
                    let byteResponse =
                        response
                        |> System.Text.Encoding.ASCII.GetBytes
                        |> ByteSegment
                        
                    do! webSocket.send Text byteResponse true
                    
                    loop <- false
                    cts.Cancel()
                | (Close, _, _) ->
                    let emptyResponse = [||] |> ByteSegment
                    do! webSocket.send Close emptyResponse true
                    loop <- false
                | _ -> ()
        }

        
    let run ip port =
        try
            cts <- new CancellationTokenSource()
            
            gotBytes <- Array.empty
            
            let app: WebPart =
                choose [
                    handShake ws
                ]
            
            let myCfg =
                {
                  defaultConfig with
                      bindings = [ HttpBinding.createSimple HTTP ip port ]
                      cancellationToken = cts.Token
                }

            startWebServer myCfg app
            
            gotBytes
        with
            | :? System.Exception as ex -> gotBytes

