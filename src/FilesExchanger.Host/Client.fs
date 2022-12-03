namespace FilesExchanger.Host

open FilesExchanger.Host.JsModules

open WebSharper
open WebSharper.UI.Templating

[<JavaScript>]
module Templates =

    type MainTemplate = Template<"Main.html", ClientLoad.FromDocument, ServerLoad.WhenChanged>

[<JavaScript>]
module Client =
    let SendFileModule() = SendFileJsModule.Run()
            
    let DownloadFileModule() = DownloadFileJsModule.Run()
            
    let TestConnectionModule () = TestConnectionJsModule.Run()
        
    let DeviceNameModule () = DeviceNameJsModule.Run()
            
            
