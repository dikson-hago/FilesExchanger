namespace FilesExchanger.Tools.CryptographyTools.Rsa

module RsaKeysInfo =
    let mutable rsaOwnKeysInfo = {
        E = 0;
        D = 0;
        N = 0;
    }
    
    let mutable rsaExternalKeysInfo = {
        E = 0;
        D = 0;
        N = 0;
    }
    
    let setOwnKeys e d n =
        rsaOwnKeysInfo <- {
            E = e;
            D = d
            N = n
        }
        
    let setExternalKeys e n =
        rsaExternalKeysInfo <- {
            E = e;
            D = 0
            N = n
        }
    
    let getOwnKeysForEncrypt() = (rsaOwnKeysInfo.E, rsaOwnKeysInfo.N)
    
    let getOwnKeysForDecrypt() = (rsaOwnKeysInfo.D, rsaOwnKeysInfo.N)
    
    let getExternalKeysForEncrypt() = (rsaExternalKeysInfo.E, rsaExternalKeysInfo.N)



    
    