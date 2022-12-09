namespace FilesExchanger.Host.JsModules

open FilesExchanger.Host.Handlers
open FilesExchanger.Host.JsModules.Templates

open WebSharper
open WebSharper.UI
open WebSharper.UI.Notation

[<JavaScript>]
module FirstConnectionForSendJsModule =
    let Run() =
        let testConnectionResponse = Var.Create ""
        JsTemplates.MainTemplate.TryConnectForSendForm()
            .TryConnect(fun e ->
                async {
                    let externalDeviceName = e.Vars.Address.Value
                    try
                        let! res = FirstConnectionForSendHandler.Connect externalDeviceName
                        testConnectionResponse := $"Connected to device: {externalDeviceName}"
                    with
                        | :? System.Exception as ex -> testConnectionResponse := "disconnected"
                }
                |> Async.StartImmediate
            )
            .TestConnectionResponse(testConnectionResponse.View)
            .Doc()
