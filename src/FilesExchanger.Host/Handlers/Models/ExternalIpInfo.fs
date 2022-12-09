namespace FilesExchanger.Host.Handlers.Models

open FilesExchanger.Application.IpConvertor
open FilesExchanger.NetworkTools

module ExternalIpInfo =
    let mutable ip = ""
    let mutable port = 0
    let mutable deviceName = ""
    
    let GetIpAddress() = IpAddressContext.BuildAddressByIpAndPort ip port
    
    let GetWebSocketAddress() = IpAddressContext.BuildWebSocketAddress (GetIpAddress())
    
    let SetDeviceName newDeviceName =
        deviceName <- newDeviceName
        let (newIp, newPort) = IpAddressConvertor.decrypt newDeviceName
        
        ip <- newIp
        port <- newPort
