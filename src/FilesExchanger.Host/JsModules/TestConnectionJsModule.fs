namespace FilesExchanger.Host.JsModules

open FilesExchanger.Host.Handlers
open FilesExchanger.Host.JsModules.Store
open FilesExchanger.Host.JsModules.Templates

open WebSharper
open WebSharper.UI
open WebSharper.UI.Notation

[<JavaScript>]
module TestConnectionJsModule =
    let Run() =
        let testConnectionResponse = Var.Create ""
        JsTemplates.MainTemplate.TryConnectForm()
            .TryConnect(fun e ->
                async {
                    let address = e.Vars.Address.Value
                    try
                        let! deviceName = GenerateDeviceNameHandler.GetName()
                        let! ipUrl = TestConnectHandler.TestConnect address deviceName
                        IpAddressStore.externalIpAddress <- ipUrl
                        testConnectionResponse := "connected"
                    with
                        | :? System.Exception as ex -> testConnectionResponse := "disconnected"
                }
                |> Async.StartImmediate
            )
            .TestConnectionResponse(testConnectionResponse.View)
            .Doc()
