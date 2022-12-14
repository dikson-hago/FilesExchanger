namespace FilesExchanger.Application.CompressionTools

open System.Collections.Generic

module CompresstionContext =
    module HaffmanContext =
        let compress (bytes : byte[]) = Haffman.compress bytes
        
        let decompress (bitsList : BitsList) (alph : Dictionary<byte, BitsList>) =
            Haffman.decompress bitsList alph