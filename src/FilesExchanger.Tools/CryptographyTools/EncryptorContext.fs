namespace FilesExchanger.Host.Handlers.Models

open System

open FilesExchanger.Tools.CryptographyTools.Rsa

module EncryptorContext =
    module RsaContext = 
        let Encrypt e n (bytes : byte[]) =
            let intArr = bytes |> Array.map(fun x -> x |> int64)
            
            let encryptedInt = (Rsa.encrypt e n intArr) |> List.toArray
            
            let encryptedBytes = encryptedInt |> Array.map(fun x -> x |> BitConverter.GetBytes)
            
            encryptedBytes

        let Decrypt d n (bytes : byte[][]) =
            let encryptedUInt = bytes |> Array.map(fun x -> BitConverter.ToInt64 x)
            
            let decryptedInt = (Rsa.decrypt d n encryptedUInt) |> List.toArray
            
            let bytesResponse = decryptedInt |> Array.map(fun x -> x |> byte)
            
            bytesResponse
