namespace FilesExchanger.Host.JsModules

open FilesExchanger.Host.Handlers
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
                    let filePath = e.Vars.TargetFolderLocation.Value
                    
                    let! res = DownloadFilesHandler.DownloadFile filePath
                    
                    downloadResult := $"Status: {res}"
                }
                |> Async.StartImmediate
            )
            .DownloadFileResponse(downloadResult.View)
            .Doc()
