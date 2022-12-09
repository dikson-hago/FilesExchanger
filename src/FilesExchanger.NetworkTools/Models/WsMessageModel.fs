module FilesExchanger.Connector.Models

type WsMessageType =
    | File
    | Key
    | CompressionInfo
    | FirstConnection
    | StatusOK

type WsMessageModel = {
    ByteMessage : byte[];
    
    BigIntArrMessage : int64[];
    
    StringMessage : string;
    
    MessageType : WsMessageType
    }    
    
    
    

