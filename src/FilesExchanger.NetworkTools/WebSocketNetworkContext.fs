namespace FilesExchanger.NetworkTools

open FilesExchanger.NetworkTools
open FilesExchanger.NetworkTools.Models
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
        let bytes = WsMessageModelAndBytesConvertor.ConvertModelToBytes model
        let ws = WebSocketSendContext(url)
        
        let respBytes = ws.SendBytes bytes
        let respModel = WsMessageModelAndBytesConvertor.ConvertBytesToModel respBytes
        
        respModel
        
    member this.GetModel ip port responseModel =
        let bytesResponse = SuaveContext.run ip port responseModel
        
        let modelResponse = WsMessageModelAndBytesConvertor.ConvertBytesToModel bytesResponse
        
        modelResponse
        