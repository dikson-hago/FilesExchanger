namespace FilesExchanger.Application.CompressionTools

open System.Collections.Generic

module Haffman =
 
    let compress (bytes: byte[]) =
        let buildFrequencesDictionary(bytes : byte[]) =
            let mutable res = Dictionary<byte, int>()
            
            let condition byte =
                if res.ContainsKey(byte) = false then res.Add(byte, 1)
                else res[byte] <- res[byte] + 1
            
            let rec fillRes = function
                | [] -> 0
                | byte :: t ->
                    condition byte |> ignore
                    fillRes t |> ignore
                    0
            
            fillRes (bytes |> Array.toList) |> ignore
            res |> Dictionary

        let dictionaryToSortedListOfNodes (dict : Dictionary<byte, int>) =
            let mutable res = List.empty
            for el in dict do res <- Nil(el.Key, el.Value) :: res
            res
        
        let compressBytes (bytes : byte[]) (bytesAlphCodes: Dictionary<byte, BitsList>) =
            let res = BitsList()
            for byte in bytes do
                let bitList = bytesAlphCodes[byte]
                let allBits = bitList.getAllBits()
                
                for bit in allBits do res.addBit bit
            res

        // count bytes frequence
        let bytesFrequenceDict = buildFrequencesDictionary bytes
        
        // create list of Nodes
        let listNodes = dictionaryToSortedListOfNodes bytesFrequenceDict
       
        // build tree by list of Nodes
        let tree = HaffmanTreeBuilder.buildTreeByNodesList listNodes
        
        // build aphabet for all bytes
        let bytesAlphCodes = HaffmanTreeContext.buildDictionaryInfoForTree tree
        
        // compress bytes
        let compressedBytes = compressBytes bytes bytesAlphCodes
        
        (compressedBytes, bytesAlphCodes)
        
    let decompress (bitsList : BitsList) (alph : Dictionary<byte, BitsList>) =
        let tree = HaffmanTreeBuilder.buildTreeByDictionaryAlph alph
        
        let mutable res = List.empty
        
        let rec run bits = function
            | Nil(byteValue, _) ->
                res <- res @ [byteValue]
               
                if bits = [] then []
                else run bits tree
                
            | Node(_, L, R) ->
                let bit = bits |> List.head
                if bit = 0 then run (bits |> List.tail) L
                else run (bits |> List.tail) R
                
        let bits = bitsList.getAllBitsInAsIntList()
        run bits tree |> ignore
        
        res