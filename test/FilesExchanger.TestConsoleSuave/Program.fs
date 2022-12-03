﻿open Suave
open Suave.Operators
open Suave.Filters
open Suave.Logging
open Suave.Sockets
open Suave.Sockets.Control
open Suave.WebSocket

let ws (webSocket : WebSocket) (context: HttpContext) =
    socket {
      let mutable loop = true

      while loop do
            let! msg = webSocket.read()

            match msg with
            | (Text, data, true) ->
                let str = UTF8.toString data
                printfn "get message: %s" str
                // send response
                let response = "ok"
                let byteResponse =
                    response
                    |> System.Text.Encoding.ASCII.GetBytes
                    |> ByteSegment
                    
                do! webSocket.send Text byteResponse true
                loop <- false
            | (Close, _, _) ->
                let emptyResponse = [||] |> ByteSegment
                do! webSocket.send Close emptyResponse true
                loop <- false
            | _ -> ()
    }

let app: WebPart =
    choose [
        handShake ws
    ]
    
let myCfg =
  { defaultConfig with
      bindings = [ HttpBinding.createSimple HTTP "127.0.0.1" 8082 ]
    }

[<EntryPoint>]
let main _ = 
    // startWebServer {defaultConfig with logger = Targets.create Verbose [||] } app
    startWebServer myCfg app
    0