namespace FilesExchanger.Host

open FilesExchanger.Connector.External.TestClient
open WebSharper

module Server =

    [<Rpc>]
    let DoSomething input =
        let R (s: string) = System.String(Array.rev(s.ToCharArray()))
        async {
            return R input
        }
        
    [<Rpc>]
    let SendByWebSocket() =
        async {
            //printfn "We are on server"
            //Connector.run()
            let url = "ws://127.0.0.1:8082"
            let client = WSClientSimple(url)
            client.SendRequest "hello from client message" |> ignore
            return "one"
        }
