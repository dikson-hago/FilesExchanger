namespace FilesExchanger.Host.Handlers

open System.Collections.Generic
open FilesExchanger.Application.CompressionTools
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
                
            return res.StringMessage
        }
