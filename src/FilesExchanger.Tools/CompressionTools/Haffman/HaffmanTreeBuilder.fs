namespace FilesExchanger.Application.CompressionTools

open System.Collections.Generic
open Microsoft.FSharp.Collections

module HaffmanTreeBuilder =
    let buildTreeByNodesList list =
        let getTwoMins = function
            | first :: second :: t ->
                (first, second, t)
        
        let getNodeValue = function
            | Node(value, L, R) -> value
            | Nil(b, value) -> value
            
        let rec putNodeToList node = function
            | [] -> [node]
            | hNode :: t ->
                let hValue = getNodeValue hNode
                let nodeValue = getNodeValue node
                
                if nodeValue < hValue then node :: hNode :: t
                else hNode :: putNodeToList node t
        
        let rec buildHaffmanList = function
            | [h] -> h
            | h :: t ->
                let (firstNode, secondNode, remindTree) = getTwoMins (h :: t)
                
                let firstNodeValue = getNodeValue firstNode
                let secondNodeValue = getNodeValue secondNode
                
                let newNode = Node(firstNodeValue + secondNodeValue, firstNode, secondNode)
                
                let newList = putNodeToList newNode remindTree
                
                buildHaffmanList newList
                
        let res = buildHaffmanList list
        res
        
    let buildTreeByDictionaryAlph (alph : Dictionary<byte, BitsList>) =
        let mutable tree = Node(0, EmptyNil, EmptyNil)
        
        let rec run byteValue bits = function
            | EmptyNil ->
                if bits = [] then Nil(byteValue, 0)
                else
                    let bit = bits |> List.head
                    let newNode = run byteValue (bits |> List.tail) EmptyNil
                    if bit = 0 then Node(0, newNode, EmptyNil)
                    else Node(0, EmptyNil, newNode)
            | Node(value, L, R) ->
                let bit = bits |> List.head
                if bit = 0 then
                    let leftNode = run byteValue (bits |> List.tail) L
                    Node(value, leftNode, R)
                else
                    let rightNode = run byteValue (bits |> List.tail) R
                    Node(value, L, rightNode)
                
        
        for byteInfo in alph do
            let byteValue = byteInfo.Key
            let bits = byteInfo.Value.getAllBitsInAsIntList()
            
            tree <- run byteValue bits tree
        
        tree