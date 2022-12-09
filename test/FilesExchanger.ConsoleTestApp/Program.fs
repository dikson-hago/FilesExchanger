open FilesExchanger.Connector.Models

open Newtonsoft.Json

[<EntryPoint>]
let main _ =
    let model = {
        StringMessage = "superMessage";
        ByteMessage = Array.empty
        BigIntArrMessage = Array.empty;
        MessageType = WsMessageType.FirstConnection
    }
    
    let jsonSer = JsonConvert.SerializeObject model
    
    let jsonDes = JsonConvert.DeserializeObject<WsMessageModel> jsonSer
    
    printfn $"message: {jsonDes.StringMessage}"
    
    0