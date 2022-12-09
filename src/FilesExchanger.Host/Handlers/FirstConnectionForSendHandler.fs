namespace FilesExchanger.Host.Handlers

open FilesExchanger.Connector.Models
open FilesExchanger.Host.Handlers.Models
open FilesExchanger.NetworkTools

open WebSharper

module FirstConnectionForSendHandler =
    
    let ConvertToFirstConnectionMessage (message : string) =
        let model = {
            StringMessage = message;
            ByteMessage = Array.empty
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
            
            let res = webSocketContext.SendModel messageModel wsAddress
            
            return res
        }
