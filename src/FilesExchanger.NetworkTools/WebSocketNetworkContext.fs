namespace FilesExchanger.NetworkTools

open System.Text
open Newtonsoft.Json

open FilesExchanger.Connector.Models
open FilesExchanger.Connector.Suave
open FilesExchanger.Connector.WebSocketsClient


type WebSocketNetworkContext() =
    member this.SendBytes url bytes =
        let ws = WebSocketSendContext(url)
        ws.SendBytes bytes

    member this.SendString url str =
        let ws = WebSocketSendContext(url)
        ws.SendString str
        
    member this.GetBytes ip port = SuaveContext.run ip port
    
    member this.SendModel (model : WsMessageModel) url =
        let modelJson = JsonConvert.SerializeObject model
        let bytes = modelJson |> Encoding.ASCII.GetBytes
        
        let ws = WebSocketSendContext(url)
        ws.SendBytes bytes
        
    member this.GetModel ip port =
        let jsonResp = (SuaveContext.run ip port)
                                |> Encoding.UTF8.GetString
        
        let res = JsonConvert.DeserializeObject<WsMessageModel> jsonResp
        
        res
        