namespace FilesExchanger.Host.InternalContext

open System.Net.WebSockets
open FilesExchanger.Application.Compression
open FilesExchanger.Application.FilesConvertor
open FilesExchanger.Connector.WebSocketsClient
open FilesExchanger.Host.Handlers.Models
open FilesExchanger.Connector.Models

open FilesExchanger.NetworkTools
open WebSharper

module SendFilesHandler =
    let ConvertBytes fileBytes =
        Haffman.compressBytes fileBytes
        
    let ConvertToSendFilesMessage (bytes : byte[]) (fileName : string) =
        let model = {
            StringMessage = fileName;
            ByteMessage = bytes
            BigIntArrMessage = Array.empty;
            MessageType = WsMessageType.File
        }
        model
        
    let GetFileNameByFilePath (filePath : string) =
        let id = filePath.LastIndexOf("/")
        let fileName = filePath.[(id + 1)..]
        
        fileName
        
    
    [<Rpc>]
    let Send filePath =
        async {            
            let filesBytes = FilesConvertorContext.FileToBytes filePath
            let convertedBytes = ConvertBytes filesBytes
            let fileName = GetFileNameByFilePath filePath
            
            let messageModel = ConvertToSendFilesMessage convertedBytes fileName
            
            let wsContext = WebSocketNetworkContext()
            let wsAddress = ExternalIpInfo.GetWebSocketAddress()
            
            let res = wsContext.SendModel messageModel wsAddress
            
            (*let wsClient = WebSocketSendContext(ExternalIpInfo.GetWebSocketAddress())
            wsClient.SendRequest convertedBytes |> ignore*)
            
            return res
        }
    
    
    

