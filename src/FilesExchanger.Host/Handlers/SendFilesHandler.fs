namespace FilesExchanger.Host.InternalContext

open FilesExchanger.Application.FilesConvertor
open FilesExchanger.Connector.WebSocketsClient
open WebSharper

module SendFilesHandler =
    
    [<Rpc>]
    let Send url filePath =
        async {
            let bytes = FilesConvertorContext.FileToBytes filePath      
            
            let wsClient = WebSocketClientContext(url)
            
            wsClient.SendRequest bytes |> ignore
            
            return 1
        }
    
    
    

