open FilesExchanger.NetworkTools.Models
open FilesExchanger.Tools.CryptographyTools.Rsa
open Newtonsoft.Json
open FilesExchanger.Host.Handlers.Models



let WsMessageModelTest =
    let model = {
        StringMessage = "superMessage";
        ByteMessage = Array.empty;
        ByteEncryptMessage = Array.empty
        BigIntArrMessage = Array.empty;
        MessageType = WsMessageType.FirstConnection
    }
    
    let jsonSer = JsonConvert.SerializeObject model
    
    let jsonDes = JsonConvert.DeserializeObject<WsMessageModel> jsonSer
    
    printfn $"message: {jsonDes.StringMessage}"
    
    0
    
let TestRsa() =
    let (e, d, n) = Rsa.init()
    
    let bytes = [|22uy; 1uy; 2uy;123uy;112uy;33uy;12uy;44uy;92uy|]
    
    let encryptBytes = EncryptorContext.RsaContext.Encrypt e n bytes
    
    let decryptedBytes = EncryptorContext.RsaContext.Decrypt d n encryptBytes
    
    1

[<EntryPoint>]
let main _ =
    TestRsa() |> ignore
    0