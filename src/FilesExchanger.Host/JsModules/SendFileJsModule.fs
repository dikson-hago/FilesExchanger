namespace FilesExchanger.Host.JsModules

open FilesExchanger.Host.InternalContext
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
                    let filePath = e.Vars.FileLocation.Value
                    try
                        let! res = SendFilesHandler.Send filePath
                        sendFileResponse := "ok"
                    with 
                       | :? System.Exception as ex -> sendFileResponse := "error"
                }
                |> Async.StartImmediate
            )
            .SendFileResponse(sendFileResponse.View)
            .Doc()