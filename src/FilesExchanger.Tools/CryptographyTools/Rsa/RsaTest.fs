namespace FilesExchanger.Application.CryptographyTools.Rsa

open FilesExchanger.Tools.CryptographyTools.Rsa

type RsaTestResult =
    | Passed
    | Failed

module RsaTest =
    let run e d n =
        let testArr = [|1L; 2L; 3L; 4L; 129L; 6L; 7L; 8L; 254L; 255L|]    
        let encryptedInt = Rsa.encrypt e n testArr
        let decryptedInt = Rsa.decrypt d n encryptedInt

        let mutable res = Passed
        
        for i in 0..9 do
            if decryptedInt.[i] <> testArr.[i]
                then res <- Failed
        res
            

