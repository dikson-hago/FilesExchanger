namespace FilesExchanger.Host.Handlers

open System.Collections.Generic
open FilesExchanger.Application.CompressionTools
open FilesExchanger.Host.Handlers.Errors
open FilesExchanger.NetworkTools.Models
open FilesExchanger.Host.Handlers.Models
open FilesExchanger.NetworkTools
open FilesExchanger.Tools.CryptographyTools.Rsa

open WebSharper

module FirstConnectionForSendHandler =
    
    let ConvertToFirstConnectionMessage (message : string) =
        let model = {
            ByteMessage = Array.empty
            CompressionInfo = {CodesInfoJson = Dictionary<byte, BitsListInfo>(); BitsAmount = 0}
            EncryptionInfo = {E = 0; N = 0};
            StringMessage = message;
            MessageType = WsMessageType.FirstConnection
        }
        model
    
    [<Rpc>]
    let Connect externalDeviceName =
        async {
            let iterationInfo = SendFileIterationType.FirstConnectionInit
            if IterationInfo.sendFileIterationValue < iterationInfo then
                return $"{ErrorTexts.IncorrectIteration}"
            else
                try
                    ExternalIpInfo.SetDeviceName externalDeviceName

                    // get device name
                    let! localDeviceName = GenerateDeviceNameHandler.GetName()
                    
                    // build message model
                    let messageModel = localDeviceName |> ConvertToFirstConnectionMessage

                    // send message by websockets
                    let webSocketContext = WebSocketNetworkContext()
                    let wsAddress = ExternalIpInfo.GetWebSocketAddress()
                    
                    // send message for first connection
                    let res = webSocketContext.SendModel messageModel wsAddress
                    
                    // set external keys
                    RsaKeysInfo.setExternalKeys
                        res.EncryptionInfo.E res.EncryptionInfo.N
        
                    IterationInfo.sendFileIterationValue <- SendFileIterationType.SendFileInit
                    
                    return ErrorTexts.ConnectionOk
                with
                    | :? System.Exception as ex -> return ErrorTexts.ConnectionError
        }
