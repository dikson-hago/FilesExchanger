namespace FilesExchanger.Application.FilesConvertor

open System.IO

module FilesConvertorContext =
    let FileToBytes filePath = filePath |> File.ReadAllBytes
    let BytesToFile filePath bytes = File.WriteAllBytes(filePath, bytes)
    let ByteToHex bytes = 
        bytes 
        |> Array.map (fun (x : byte) -> System.String.Format("{0:X2} ", x))
        |> String.concat System.String.Empty
    let GetByteArrForPrint filePath = FileToBytes filePath |> ByteToHex
    
    

