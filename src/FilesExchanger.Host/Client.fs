namespace FilesExchanger.Host

open WebSharper
open WebSharper.UI
open WebSharper.UI.Templating
open WebSharper.UI.Notation

[<JavaScript>]
module Templates =

    type MainTemplate = Template<"Main.html", ClientLoad.FromDocument, ServerLoad.WhenChanged>

[<JavaScript>]
module Client =
    let SendFileModule () =
        let rvReversed = Var.Create ""
        Templates.MainTemplate.SendFileForm()
            .OnSend(fun e ->
                async {
                    //let! x = Server.SendByWebSocket
                    let res = "Try to send message"
                    //let! res = Server.DoSomething e.Vars.TextToReverse.Value
                    rvReversed := res
                }
                |> Async.StartImmediate
            )
            .Doc()
            
    let GetFileModule () =
        let rvReversed = Var.Create ""
        Templates.MainTemplate.GetFileForm()
            .Download(fun e ->
                async {
                    let! res = Server.DoSomething "hello"
                    //let res = "Pending..."
                    rvReversed := res
                }
                |> Async.StartImmediate
            )
            .Doc()
            
    let ConnectionModule () =
        let rvReversed = Var.Create ""
        Templates.MainTemplate.ConnectionForm()
            .TryConnect(fun e ->
                async {
                    let! res = Server.SendByWebSocket() //Server.DoSomething "hello"
                    //printfn "res=%s" res
                    //let! x = Server.SendByWebSocket
                    //let res = "Pending..."
                    rvReversed := res
                }
                |> Async.StartImmediate
            )
            .ConnectionReserved(rvReversed.View)
            .Doc()
            
            
