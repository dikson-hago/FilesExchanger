namespace FilesExchanger.Application.CompressionTools

open System.Collections.Generic

module HaffmanTreeContext =
    let buildDictionaryInfoForTree tree =
        let dict = Dictionary<byte, BitsList>()

        let rec run bitArr = function
            | Nil(byteValue, _) ->
                let bitList = BitsList()
                for bit in bitArr do bitList.addBit bit
                dict[byteValue] <- bitList
                ()
            | Node(_, L, R) ->
                run (bitArr @ [0]) L
                run (bitArr @ [1]) R
                ()
        run [] tree
        
        dict
        