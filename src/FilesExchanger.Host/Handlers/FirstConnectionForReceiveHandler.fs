namespace FilesExchanger.Host.Handlers

open FilesExchanger.NetworkTools
open FilesExchanger.NetworkTools.Models
open FilesExchanger.Tools.CryptographyTools.Rsa


open WebSharper

module FirstConnectionForReceiveHandler =
    let initRsa() =
        let (e, d, n) = Rsa.init()
        RsaKeysInfo.setOwnKeys e d n
        ()
    
    let InitConnectionResponse() =
        let (e, d) = RsaKeysInfo.getOwnKeysForEncrypt()
        let model = {
            StringMessage = "";
            ByteMessage = Array.empty;
            ByteEncryptMessage = Array.empty;
            BigIntArrMessage = [|e; d|];
            MessageType = WsMessageType.Key
        }
        model
    
    [<Rpc>]
    let Connect() =
        async {
            // get ip and port
            let (ip, port) = IpAddressContext.GetLocalIpAndPortForSuave()
            
            // init Rsa
            initRsa()
            
            // init connection response
            let connectionResponse = InitConnectionResponse()
            
            // create websocket context
            let wsContext = WebSocketNetworkContext()
            
            // get model of first connection
            let model = wsContext.GetModel ip port connectionResponse
            
            // get connection device name            
            let connectedDeviceName = model.StringMessage
            
            return connectedDeviceName
        }

