namespace FilesExchanger.Tools.CompressionTools.Haffman

open System.Collections.Generic
open FilesExchanger.Application.CompressionTools

module BitListConverter =
    let ToBitListInfo (codesInfo : Dictionary<byte, BitsList>) =
        let res = Dictionary<byte, BitsListInfo>()
        
        for el in codesInfo do
            let info = el.Value.getBitsListTypeInfo()
            res[el.Key] <- info
            
        res
        
    let ToBitList (codesInfoDict : Dictionary<byte,BitsListInfo>) =
        let res = Dictionary<byte, BitsList>()
        
        for el in codesInfoDict do
            let bitsList = BitsList()
            bitsList.setBitsListInfo el.Value
            res[el.Key] <- bitsList
        
        res

