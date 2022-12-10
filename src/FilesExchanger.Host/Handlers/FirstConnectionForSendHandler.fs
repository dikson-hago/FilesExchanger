namespace FilesExchanger.Host.Handlers

open FilesExchanger.NetworkTools.Models
open FilesExchanger.Host.Handlers.Models
open FilesExchanger.NetworkTools
open FilesExchanger.Tools.CryptographyTools.Rsa

open WebSharper

module FirstConnectionForSendHandler =
    
    let ConvertToFirstConnectionMessage (message : string) =
        let model = {
            StringMessage = message;
            ByteMessage = Array.empty;
            ByteEncryptMessage = Array.empty;
            BigIntArrMessage = Array.empty;
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
                res.BigIntArrMessage.[0] res.BigIntArrMessage.[1]
             
            return res
        }
