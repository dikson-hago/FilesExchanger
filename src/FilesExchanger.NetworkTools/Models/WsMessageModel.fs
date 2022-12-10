namespace FilesExchanger.NetworkTools.Models

type WsMessageType =
    | File
    | Key
    | CompressionInfo
    | FirstConnection
    | StatusOK

type WsMessageModel = {
    ByteMessage : byte[]
    
    ByteEncryptMessage : byte[][]
    
    BigIntArrMessage : int64[];
    
    StringMessage : string;
    
    MessageType : WsMessageType
    }    
    
    
    

