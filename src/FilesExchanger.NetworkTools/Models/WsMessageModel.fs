namespace FilesExchanger.NetworkTools.Models

open System.Collections.Generic
open FilesExchanger.Application.CompressionTools

type WsMessageType =
    | File
    | Key
    | FirstConnection
    | StatusOK
    
type CompressionInfoType = {
    CodesInfoJson : Dictionary<byte, BitsListInfo>
    BitsAmount : int
}

type EncryptionInfoType = {
    E : int64
    N : int64
}

type WsMessageModel = {
    ByteMessage : byte[]
    
    CompressionInfo : CompressionInfoType
    
    EncryptionInfo : EncryptionInfoType;
    
    StringMessage : string
    
    MessageType : WsMessageType
    }

    
    

