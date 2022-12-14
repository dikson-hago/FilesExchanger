namespace FilesExchanger.Host

open WebSharper
open WebSharper.Sitelets
open WebSharper.UI
open WebSharper.UI.Server

type EndPoint =
    | [<EndPoint "/post">] Post
    | [<EndPoint "/get">] Get

module Templating =
    open WebSharper.UI.Html

    // Compute a menubar where the menu item for the given endpoint is active
    let MenuBar (ctx: Context<EndPoint>) endpoint : Doc list =
        let ( => ) txt act =
             li [if endpoint = act then yield attr.``class`` "active"] [
                a [attr.href (ctx.Link act)] [text txt]
             ]
        [
            "Post" => EndPoint.Post
            "Get" => EndPoint.Get
        ]

    let Main ctx action (title: string) (body: Doc list) =
        Content.Page(
            Templates.MainTemplate()
                .Title(title)
                .MenuBar(MenuBar ctx action)
                .Body(body)
                .Doc()
        )

module Site =
    open WebSharper.UI.Html

    open type WebSharper.UI.ClientServer

    let HomePage ctx =
        Templating.Main ctx EndPoint.Post "Post" [
            h1 [] [text "Page for send files"]
            div [] [client (Client.DeviceNameModule())]
            div [] [client (Client.TestConnectionForSendModule())]
            div [] [client (Client.SendFileModule())]
        ]

    let AboutPage ctx =
        Templating.Main ctx EndPoint.Get "Get" [
            h1 [] [text "Page for get files"]
            div [] [client (Client.DeviceNameModule())]
            div [] [client (Client.TestConnectionForReceiveModule())]
            div [] [client (Client.DownloadFileModule())]
        ]

    [<Website>]
    let Main =
        Application.MultiPage (fun ctx endpoint ->
            match endpoint with
            | EndPoint.Post -> HomePage ctx
            | EndPoint.Get -> AboutPage ctx
        )
