namespace FilesExchanger.Host.Handlers

open FilesExchanger.Application.IpConvertor
open FilesExchanger.NetworkTools

open WebSharper

module GenerateDeviceNameHandler =
      [<Rpc>]
      let GetName() =
          async {
               let (localIp, port) = IpAddressContext.GetLocalIpAddressAndPort()
               
               let name = IpAddressConvertor.encrypt localIp port
               
               return name
          }