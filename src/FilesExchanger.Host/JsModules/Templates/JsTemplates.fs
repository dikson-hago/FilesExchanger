namespace FilesExchanger.Host.JsModules.Templates
 
open WebSharper
open WebSharper.UI.Templating

[<JavaScript>]
module JsTemplates =
    type MainTemplate = Template<"Main.html", ClientLoad.FromDocument, ServerLoad.WhenChanged>