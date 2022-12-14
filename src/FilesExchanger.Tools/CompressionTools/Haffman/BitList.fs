namespace FilesExchanger.Application.CompressionTools

open System

type BitsListInfo = {
    BitsListSize : int
    BytesArr : byte[]
}
    

type BitsList() =
    let allBitsArr = [|128; 64; 32; 16; 8; 4; 2; 1|]
    let mutable bytesList = List.empty
    
    let mutable bitsAmount = 0
    
    let mutable nowBitId = 8
    
    member this.addBit bit =
        let addBitForFirst bitValue = function
            | h :: t ->
                if bitValue = 1 then
                    let intValue = ((int) h + allBitsArr.[nowBitId])
                    let byteValue = (byte) intValue
                    byteValue :: t
                else h :: t
            
        if nowBitId = 8 then
            nowBitId <- 0
            bytesList <- ((byte) 0) :: bytesList
        
        bytesList <- addBitForFirst bit bytesList
        nowBitId <- nowBitId + 1  
        bitsAmount <- bitsAmount + 1
        
    member this.getAllBits() =
        seq {
            let mutable listOfBytes = bytesList |> List.rev

            let mutable bitId = 8
            let mutable byteIntValue = 0
            for step in 1 .. bitsAmount do
                if bitId = 8 then
                    bitId <- 0
                    byteIntValue <- (int) (listOfBytes |> List.head)
                    listOfBytes <- (listOfBytes |> List.tail)
                
                let nowTwoPowerValue = allBitsArr.[bitId]
                bitId <- bitId + 1
                
                if byteIntValue >= nowTwoPowerValue then
                    byteIntValue <- byteIntValue - nowTwoPowerValue
                    yield 1
                else yield 0
        }
        
    member this.getAllBitsInAsIntList() = this.getAllBits() |> Seq.toList
   
    member this.getListInfo() = (bytesList, bitsAmount)
    
    member this.setBitsList (model : BitsList) =
        let allBits = model.getAllBits()
        for bit in allBits do this.addBit bit
        
    member this.getCompressionInfo() =
        (bytesList, bitsAmount)
        
    member this.setCompressionInfo bytes amount =
        bytesList <- bytes
        bitsAmount <- amount
        
    member this.setBitsListInfo (info: BitsListInfo) =
        bytesList <- info.BytesArr |> Array.toList
        bitsAmount <- info.BitsListSize
        
    member this.getBitsListTypeInfo() =
        {BitsListSize = bitsAmount
         BytesArr = (bytesList |> List.toArray)}

