namespace FilesExchanger.Host.Handlers

open FilesExchanger.Connector.WebSocketsClient
open WebSharper
open FilesExchanger.Application.Tools

module TestConnectHandler =
    [<Rpc>]
    let TestConnect ipPort deviceName =
        async {
            let url = IpTools.BuildWebSocketAddress ipPort
            
            let wsClient = WebSocketClientContext(url)
            
            wsClient.SendString deviceName |> ignore
            
            return url
        }
