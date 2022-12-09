namespace FilesExchanger.Host.JsModules

open FilesExchanger.Host.Handlers
open FilesExchanger.Host.JsModules.Templates

open WebSharper
open WebSharper.UI
open WebSharper.UI.Notation


[<JavaScript>]
module FirstConnectionForReceiveJsModule =
    let Run() =
        let connectionStatus = Var.Create ""
        JsTemplates.MainTemplate.TryConnectForReceiveForm()
            .WaitConnection(fun e ->
                async {
                    try
                        let! connectedDeviceName = FirstConnectionForReceiveHandler.Connect()
                        connectionStatus := $"Connected device: {connectedDeviceName}"
                    with
                        | :? System.Exception as ex -> connectionStatus := "disconnected"
                }
                |> Async.StartImmediate
            )
            .TestConnectionResponse(connectionStatus.View)
            .Doc()
