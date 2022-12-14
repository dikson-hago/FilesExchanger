namespace FilesExchanger.Host.JsModules

open FilesExchanger.Host.Handlers
open FilesExchanger.Host.JsModules.Templates

open WebSharper.UI
open WebSharper.UI.Notation
open WebSharper

[<JavaScript>]
module DeviceNameJsModule =
    let Run() =
        let deviceName = Var.Create ""
        JsTemplates.MainTemplate.GenerateDeviceNameForm()
            .GenerateDeviceName(fun e ->
                async {
                    let! randName = GenerateDeviceNameHandler.GetName()
                    deviceName := $"Name: {randName}"
                }
                |> Async.StartImmediate
            )
            .DeviceNameResponse(deviceName.View)
            .Doc()