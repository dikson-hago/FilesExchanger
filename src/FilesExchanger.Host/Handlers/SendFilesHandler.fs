namespace FilesExchanger.Host.InternalContext

open FilesExchanger.Application.CompressionTools
open FilesExchanger.Application.FilesConvertor
open FilesExchanger.Host.Handlers.Errors
open FilesExchanger.Host.Handlers.Models
open FilesExchanger.NetworkTools.Models
open FilesExchanger.Tools.CryptographyTools.Rsa
open FilesExchanger.NetworkTools
open FilesExchanger.Tools.CompressionTools.Haffman
open WebSharper

module SendFilesHandler =
    let CompressBytes fileBytes =
        let (bitList, codesInfo) = CompresstionContext.HaffmanContext.compress fileBytes
        
        let codesFinalInfo = BitListConverter.ToBitListInfo codesInfo
        
        let (bytes, amount) = bitList.getCompressionInfo()
        
        let compressionInfo = {
            CodesInfoJson = codesFinalInfo;
            BitsAmount = amount
        }
        
        (bytes |> List.toArray, compressionInfo)
            
    let EncryptBytes (bytes: byte[]) =
        let (e, n) = RsaKeysInfo.getExternalKeysForEncrypt()
        let encryptedBytes = EncryptorContext.RsaContext.Encrypt2 e n bytes
        
        let encryptionInfo = {
            E = e
            N = n
        }
        
        (encryptedBytes, encryptionInfo)
        
    let ConvertToSendFilesMessage (bytes : byte[]) (compressionInfo : CompressionInfoType)
                    (encryptionInfo : EncryptionInfoType) (fileName : string) =
        let model = {
            ByteMessage = bytes;
            CompressionInfo = compressionInfo
            EncryptionInfo = encryptionInfo;
            StringMessage = fileName;
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
            // check iteration
            let iterationInfo = SendFileIterationType.SendFileInit
            if IterationInfo.sendFileIterationValue < iterationInfo then
                return ErrorTexts.IncorrectIteration
            else
                try 
                    // get bytes of file
                    let filesBytes = FilesConvertorContext.FileToBytes filePath
                    
                    // encrypt bytes
                    let (encryptedBytes, encryptionInfo) = EncryptBytes filesBytes
                    
                    // compress bytes
                    let (compressedBytes, compressionInfo) = CompressBytes encryptedBytes
                    
                    // get file name
                    let fileName = GetFileNameByFilePath filePath
                    
                    // build message model
                    let messageModel = ConvertToSendFilesMessage compressedBytes
                                           compressionInfo encryptionInfo fileName
                    
                    // build WebSocket context
                    let wsContext = WebSocketNetworkContext()
                    let wsAddress = ExternalIpInfo.GetWebSocketAddress()
                    
                    // send message model
                    wsContext.SendModel messageModel wsAddress |> ignore
                    
                    return ErrorTexts.StatusOk
                with
                    | :? System.Exception as ex -> return ErrorTexts.ConnectionError
        }
    
    
    

