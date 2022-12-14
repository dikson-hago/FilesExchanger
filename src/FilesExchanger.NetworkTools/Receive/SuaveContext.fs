namespace FilesExchanger.Connector.Suave

open System.Collections.Generic
open FilesExchanger.Application.CompressionTools
open FilesExchanger.NetworkTools
open FilesExchanger.NetworkTools.Models

open System.Threading
open Suave
open Suave.Sockets
open Suave.Sockets.Control
open Suave.WebSocket


module SuaveContext =    
    let mutable gotBytes = Array.empty
    
    let mutable cts = new CancellationTokenSource()
    
    let mutable responseMessage = {
            ByteMessage = Array.empty
            CompressionInfo = {CodesInfoJson = Dictionary<byte, BitsListInfo>(); BitsAmount = 0};
            EncryptionInfo = {E = 0; N = 0}
            StringMessage = ""
            MessageType = WsMessageType.StatusOK
        }

    let ws (webSocket : WebSocket) (context: HttpContext) =
        socket {
          let mutable loop = true
          
          while loop do
                let! msg = webSocket.read()

                match msg with
                | (Text, data, true) ->
                    gotBytes <- data
                    
                    // send response
                    let byteResponse =
                        WsMessageModelAndBytesConvertor.ConvertModelToBytes responseMessage
                        
                    do! webSocket.send Text (byteResponse |> ByteSegment) true
                    
                    loop <- false
                    cts.Cancel()
                | (Close, _, _) ->
                    let emptyResponse = [||] |> ByteSegment
                    do! webSocket.send Close emptyResponse true
                    loop <- false
                | _ -> ()
        }

        
    let run ip port respMessage =
        try
            responseMessage <- respMessage
            
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

