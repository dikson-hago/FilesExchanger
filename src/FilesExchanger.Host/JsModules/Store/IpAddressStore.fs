namespace FilesExchanger.Host.JsModules.Store

open WebSharper

[<JavaScript>]
module IpAddressStore =
    let mutable externalIpAddress = ""

