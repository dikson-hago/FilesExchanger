namespace FilesExchanger.Host.Handlers

open System.Collections.Generic
open FilesExchanger.Application.CompressionTools
open FilesExchanger.Application.FilesConvertor
open FilesExchanger.Host.Handlers.Models
open FilesExchanger.NetworkTools
open FilesExchanger.NetworkTools.Models
open FilesExchanger.Tools.CompressionTools.Haffman
open FilesExchanger.Tools.CryptographyTools.Rsa

open Newtonsoft.Json
open WebSharper
open WebSharper.UI

module DownloadFilesHandler =
    let DecryptBytes (bytes : byte[]) =
        let (d, n) = RsaKeysInfo.getOwnKeysForDecrypt()
        
        let decryptBytes = EncryptorContext.RsaContext.Decrypt2 d n bytes
        
        decryptBytes

    let DecompressBytes (bytes : byte[]) (compressionInfo : CompressionInfoType) =
        let bitsList = BitsList()
        
        bitsList.setCompressionInfo (bytes |> Array.toList) compressionInfo.BitsAmount
        
        let codesInfoFinal = BitListConverter.ToBitList compressionInfo.CodesInfoJson
        
        let res = CompresstionContext.HaffmanContext.decompress bitsList codesInfoFinal
        res
    
    let InitConnectionResponse() =
        let model = {
            ByteMessage = Array.empty
            CompressionInfo = {CodesInfoJson = Dictionary<byte, BitsListInfo>(); BitsAmount = 0};
            EncryptionInfo = {E = 0; N = 0}
            StringMessage = ""
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
            
            // decompress bytes
            let decompressedBytes =
                    DecompressBytes wsModel.ByteMessage wsModel.CompressionInfo
            
            // encrypt bytes
            let bytes = DecryptBytes (decompressedBytes |> List.toArray)
            
            // get file fileName
            let fileName = wsModel.StringMessage
            
            // get file path
            let filePath = FilesConvertorContext.ConcatFolderAndFile targetFolderUrl fileName
            
            // save file
            FilesConvertorContext.BytesToFile filePath bytes
            
            return 1
        }

