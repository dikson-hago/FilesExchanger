namespace FilesExchanger.NetworkTools

open System.Net
open System.Net.Sockets

module IpAddressContext =
    let BuildWebSocketAddress address = "ws://" + address
    
    let BuildAddressByIpAndPort ip port = ip + ":" + (port |> string)
    
    let GetLocalIpAndPortForSuave() = ("127.0.0.1", 8082)
    
    let GetLocalIpAddressAndPort() =
        if System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable() = false
            then ("", 0)
        else
            let host = Dns.GetHostEntry(Dns.GetHostName())
            
            let localIpAddress = host.AddressList
                                 |> Array.find(fun ip -> ip.AddressFamily = AddressFamily.InterNetwork)
            
            (localIpAddress |> string, 8082)
            

