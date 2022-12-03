namespace FilesExchanger.Host.JsModules

open FilesExchanger.Host.JsModules.Templates

open WebSharper
open WebSharper.UI
open WebSharper.UI.Notation

[<JavaScript>]
module DownloadFileJsModule =
    let Run() =
        let rvReversed = Var.Create ""
        JsTemplates.MainTemplate.DownloadFileForm()
            .Download(fun e ->
                async {
                    rvReversed := "res"
                }
                |> Async.StartImmediate
            )
            .Doc()
