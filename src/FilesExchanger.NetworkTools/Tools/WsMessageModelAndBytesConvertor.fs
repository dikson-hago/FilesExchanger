namespace FilesExchanger.NetworkTools

open System.Text
open FilesExchanger.NetworkTools.Models
open Newtonsoft.Json

module WsMessageModelAndBytesConvertor =
    let ConvertModelToBytes model =
        let modelJson = JsonConvert.SerializeObject model
        let bytes = modelJson |> Encoding.ASCII.GetBytes
        bytes
    
    let ConvertBytesToModel (bytes : byte[]) =
        let jsonResp = bytes |> Encoding.UTF8.GetString
        let res = JsonConvert.DeserializeObject<WsMessageModel> jsonResp
        res

    

