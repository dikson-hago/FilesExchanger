namespace FilesExchanger.Host.Handlers

open FilesExchanger.Application.FilesConvertor
open FilesExchanger.NetworkTools

open WebSharper

module DownloadFilesHandler =
    [<Rpc>]
    let DownloadFile targetFolderUrl =
        async {
            let (localIp, port) = IpAddressContext.GetLocalIpAndPortForSuave()
            
            // get file
            let wsContext = WebSocketNetworkContext()
            
            let wsModel = wsContext.GetModel localIp port
            
            let bytes = wsModel.ByteMessage
            let fileName = wsModel.StringMessage
            
            let filePath = FilesConvertorContext.ConcatFolderAndFile targetFolderUrl fileName
            
            // save file
            FilesConvertorContext.BytesToFile filePath bytes
            
            return 1
        }

