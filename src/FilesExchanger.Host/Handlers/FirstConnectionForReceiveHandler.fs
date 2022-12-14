namespace FilesExchanger.Host.Handlers

open System.Collections.Generic
open FilesExchanger.Application.CompressionTools
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
        let (e, n) = RsaKeysInfo.getOwnKeysForEncrypt()
        let model = {
            ByteMessage = Array.empty
            CompressionInfo = {CodesInfoJson = Dictionary<byte, BitsListInfo>(); BitsAmount = 0}
            EncryptionInfo = {E = e; N = n}
            StringMessage = "";
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

