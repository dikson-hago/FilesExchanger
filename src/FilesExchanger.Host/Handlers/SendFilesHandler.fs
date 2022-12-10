namespace FilesExchanger.Host.InternalContext

open FilesExchanger.Application.Compression
open FilesExchanger.Application.FilesConvertor
open FilesExchanger.Host.Handlers.Models
open FilesExchanger.NetworkTools.Models

open FilesExchanger.Tools.CryptographyTools.Rsa
open FilesExchanger.NetworkTools
open WebSharper

module SendFilesHandler =
    let ConvertBytes fileBytes =
        Haffman.compressBytes fileBytes
        
    let EncryptBytes (bytes: byte[]) =
        let (e, n) = RsaKeysInfo.getExternalKeysForEncrypt()
        let encryptedBytes = EncryptorContext.RsaContext.Encrypt e n bytes
        encryptedBytes
        
    let ConvertToSendFilesMessage (bytes : byte[][]) (fileName : string) =
        let model = {
            StringMessage = fileName;
            ByteMessage = Array.empty;
            ByteEncryptMessage = bytes;
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
            // get bytes of file
            let filesBytes = FilesConvertorContext.FileToBytes filePath
            
            // encrypt bytes
            let encryptedBytes = EncryptBytes filesBytes
            
            // get file name
            let fileName = GetFileNameByFilePath filePath
            
            // build message model
            let messageModel = ConvertToSendFilesMessage encryptedBytes fileName
            
            // build WebSocket context
            let wsContext = WebSocketNetworkContext()
            let wsAddress = ExternalIpInfo.GetWebSocketAddress()
            
            // send message model
            let res = wsContext.SendModel messageModel wsAddress
            
            return res
        }
    
    
    

