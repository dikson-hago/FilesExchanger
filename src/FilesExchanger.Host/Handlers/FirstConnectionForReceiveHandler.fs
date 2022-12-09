namespace FilesExchanger.Host.Handlers

open FilesExchanger.Connector.Suave
open FilesExchanger.NetworkTools

open WebSharper

module FirstConnectionForReceiveHandler =
    [<Rpc>]
    let Connect() =
        async {
            let (ip, port) = IpAddressContext.GetLocalIpAndPortForSuave()
            
            let wsContext = WebSocketNetworkContext()
            let model = wsContext.GetModel ip port
                        
            let connectedDeviceName = model.StringMessage
            
            return connectedDeviceName
        }

