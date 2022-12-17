open System
open System.Collections.Generic
open FilesExchanger.Host.Handlers.Models.EncryptorContext
open FilesExchanger.NetworkTools.Models
open FilesExchanger.Tools.CryptographyTools.Rsa
open Newtonsoft.Json
open FilesExchanger.Host.Handlers.Models
open FilesExchanger.Application.CompressionTools

let WsMessageModelTest (dict : Dictionary<byte, BitsListInfo>) =    
    let model = {
        ByteMessage = Array.empty
        CompressionInfo = {CodesInfoJson = dict; BitsAmount = 0}
        EncryptionInfo = {E = 0; N = 0};
        StringMessage = ""
        MessageType = WsMessageType.FirstConnection
    }
    
    let jsonSer = JsonConvert.SerializeObject model
    
    let jsonDes = JsonConvert.DeserializeObject<WsMessageModel> jsonSer
    
    //let jsonDesDict = JsonConvert.DeserializeObject<Dictionary<byte, BitsListInfo>> jsonDes.CompressionInfo.CodesInfo
    
    //printfn $"message: {jsonDes.StringMessage}"
    
    0
    
let TestRsa() =
    let (e, d, n) = RsaContext.InitRsa()
    
    let bytes = [|22uy; 1uy; 2uy;123uy;112uy;33uy;12uy;44uy;92uy|]
    
    let encryptBytes = EncryptorContext.RsaContext.Encrypt2 e n bytes
    
    let decryptedBytes = EncryptorContext.RsaContext.Decrypt2 d n encryptBytes
    
    1
    
let TestBitList() =
    let bitsList = BitsList()
    let bits = [|1;1;0;1;1;0|]
    
    for bit in bits do
        bitsList.addBit bit
    
    let allBits = bitsList.getAllBits()
    
    for bit in allBits do
        printfn $"%d{bit}"
        
    1
    
let TestCompression() =
    let bytes = [|1uy; 1uy; 1uy;1uy;2uy;2uy;2uy;3uy;3uy|]
    let (res, codesInfo) =
        CompresstionContext.HaffmanContext.compress bytes
        
    let json = JsonConvert.SerializeObject codesInfo
    
    let (compressedBytes, bitsAmount) = res.getCompressionInfo()
    
    let bitsList = new BitsList()
    bitsList.setCompressionInfo compressedBytes bitsAmount
    
    let res = CompresstionContext.HaffmanContext.decompress bitsList codesInfo
    
    1


let TestInt64() =
    let x1 = 9223372036854775807L
    let len1 = (BitConverter.GetBytes x1) |> Array.length
    
    let x2 = 0L
    let len2 = (BitConverter.GetBytes x2) |> Array.length
    
    0
    
let TestRsaTest() =
    let x = RsaContext.InitRsa()
    x
    
[<EntryPoint>]
let main _ =
    let s = "C:\sd\d"
    printfn $"%d{s.LastIndexOf('\\')}"
    // TestRsaTest() |> ignore
    (*let dict = new Dictionary<byte, BitsListInfo>()
    dict[1uy] <- {BitsListSize = 0; BytesArr = [|1uy;2uy|]}
    
    WsMessageModelTest(dict) |> ignore*)
    //TestCompression() |> ignore
    //TestInt64() |> ignore
   // TestCompression() |> ignore
    0