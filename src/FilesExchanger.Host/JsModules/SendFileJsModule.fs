namespace FilesExchanger.Host.JsModules

open FilesExchanger.Host.InternalContext
open FilesExchanger.Host.JsModules.Store
open FilesExchanger.Host.JsModules.Templates

open WebSharper
open WebSharper.UI
open WebSharper.UI.Notation

[<JavaScript>]
module SendFileJsModule =
    let Run() =
        let sendFileResponse = Var.Create ""
        JsTemplates.MainTemplate.SendFileForm()
            .OnSend(fun e ->
                async {
                    try
                        let filePath = e.Vars.FileLocation.Value
                        let! res = SendFilesHandler.Send IpAddressStore.externalIpAddress filePath
                        sendFileResponse := "ok"
                    with 
                       | :? System.Exception as ex -> sendFileResponse := "error"
                }
                |> Async.StartImmediate
            )
            .SendFileResponse(sendFileResponse.View)
            .Doc()