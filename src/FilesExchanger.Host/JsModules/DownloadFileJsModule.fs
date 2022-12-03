namespace FilesExchanger.Host.JsModules

open FilesExchanger.Host.JsModules.Templates

open WebSharper
open WebSharper.UI
open WebSharper.UI.Notation

[<JavaScript>]
module DownloadFileJsModule =
    let Run() =
        let downloadResult = Var.Create ""
        JsTemplates.MainTemplate.DownloadFileForm()
            .Download(fun e ->
                async {
                    downloadResult := "ok"
                }
                |> Async.StartImmediate
            )
            .DownloadFileResponse(downloadResult.View)
            .Doc()
