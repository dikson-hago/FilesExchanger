namespace FilesExchanger.Host.Handlers

open FilesExchanger.Application.FilesConvertor
open FilesExchanger.Host.Handlers.Models
open FilesExchanger.NetworkTools
open FilesExchanger.NetworkTools.Models
open FilesExchanger.Tools.CryptographyTools.Rsa

open WebSharper

module DownloadFilesHandler =
    let DecryptBytes (bytes : byte[][]) =
        let (d, n) = RsaKeysInfo.getOwnKeysForDecrypt()
        
        let decryptBytes = EncryptorContext.RsaContext.Decrypt d n bytes
        
        decryptBytes
    
    let InitConnectionResponse() =
        let model = {
            StringMessage = "";
            ByteMessage = Array.empty;
            ByteEncryptMessage = Array.empty;
            BigIntArrMessage = Array.empty;
            MessageType = WsMessageType.StatusOK
        }
        model
    
    [<Rpc>]
    let DownloadFile targetFolderUrl =
        async {
            // get local ip and port
            let (localIp, port) = IpAddressContext.GetLocalIpAndPortForSuave()

            // build response model
            let responseModel = InitConnectionResponse()
            
            // build webSocket context
            let wsContext = WebSocketNetworkContext()
            
            // get model
            let wsModel = wsContext.GetModel localIp port responseModel
            
            // encrypt bytes
            let encryptedBytes = wsModel.ByteEncryptMessage
            let bytes = DecryptBytes encryptedBytes
            
            // get file fileName
            let fileName = wsModel.StringMessage
            
            // get file path
            let filePath = FilesConvertorContext.ConcatFolderAndFile targetFolderUrl fileName
            
            // save file
            FilesConvertorContext.BytesToFile filePath bytes
            
            return 1
        }

