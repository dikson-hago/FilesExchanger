namespace FilesExchanger.Host.Handlers

open WebSharper

module GenerateDeviceNameHandler =
      [<Rpc>]
      let GetName() =
          let mutable deviceName = "deviceMainName"
          async {
               return deviceName
          }