namespace FilesExchanger.Host.Handlers.Models

open FilesExchanger.Application.CryptographyTools.Rsa
open FilesExchanger.Tools.CryptographyTools.Rsa

open System
open Microsoft.FSharp.Collections

module EncryptorContext =
    module RsaContext =
        let InitRsa() =
            let mutable rsaTestResult = RsaTestResult.Failed
            
            let mutable e = 0L
            let mutable d = 0L
            let mutable n = 0L
            
            while (rsaTestResult <> RsaTestResult.Passed) do
                let (e1, d1, n1) = Rsa.init()
                e <- e1
                d <- d1
                n <- n1
                let res = (RsaTest.run e d n)
                rsaTestResult <- res
            (e, d, n)
        
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
            
        let Encrypt2 e n (bytes : byte[]) =
            let intArr = bytes |> Array.map(fun x -> x |> int64)
            
            let encryptedInt = (Rsa.encrypt e n intArr) |> List.toArray
            
            let encryptedBytes = encryptedInt |> Array.collect(fun x -> x |> BitConverter.GetBytes)
            
            encryptedBytes
            
            
        let Decrypt2 d n (bytes : byte[]) =
            let rec cutBytes = function
                | [] -> []
                | (h : byte) :: t ->
                    let newElement = ((h :: t) |> List.take 8) |> List.toArray
                    let valueInt64 = BitConverter.ToInt64 newElement
                    let remindBytes = (h :: t) |> List.removeManyAt 0 8
                    valueInt64 :: (cutBytes remindBytes)
            
            let encryptedInt64 = (cutBytes (bytes |> Array.toList))
                        
            let decryptedInt = (Rsa.decrypt d n encryptedInt64) |> List.toArray
            
            let bytesResponse = decryptedInt |> Array.map(fun x -> x |> byte)
            
            bytesResponse


        
