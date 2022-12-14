namespace FilesExchanger.Application.CompressionTools

type 't HaffmanTree =
        | EmptyNil
        | Nil of byte * int
        | Node of 't * 't HaffmanTree * 't HaffmanTree