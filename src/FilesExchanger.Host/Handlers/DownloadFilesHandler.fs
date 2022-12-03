namespace FilesExchanger.Host.Handlers

open FilesExchanger.Application.FilesConvertor
open FilesExchanger.Connector.WebSocketsClient
open WebSharper

module DownloadFilesHandler =
    [<Rpc>]
    let DownloadFile targetUrl targetFileUrl =
        async {
            let wsClient = WebSocketClientContext(targetUrl)
            
            let bytes = wsClient.GetBytes()
            
            // save file
            FilesConvertorContext.BytesToFile targetFileUrl bytes
            
            return 1
        }

