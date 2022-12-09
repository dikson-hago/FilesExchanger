namespace FilesExchanger.Application.Cryptography

open System

module Rsa = 
    module Crypto =
        let sqrMod m e n =
            let mutable midres = 1L
            for _ in 1L..e do midres <- ((midres % n) * m) % n
            midres
            
        let encrypt e n m = [for num in m -> sqrMod num e n]

        let decrypt  d n c = [for num in c -> sqrMod num d n]

    let init _ =
        let maxRandomValue = 10000
        let minRandomValue = 300

        let getIntSqrt (n: int64) = int64 (sqrt (double n))

        let rec findNumber (valueStep: int64) condition = 
            if condition valueStep = true then valueStep
            else findNumber (valueStep + 1L) condition

        let isPrime (n : int64) =
            let sqrtN = getIntSqrt n
            let rec check divider =
                if divider > sqrtN then true
                else (n % divider <> 0) && check (divider + 1L)
            check 2

        let areCoprime (firstValue: int64) (secondValue: int64) =
            let rec gcd = function
                | (x: int64), 0L -> x
                | (x: int64), (y: int64) -> gcd (y, x % y)

            let coprime = gcd >> (=) 1

            coprime (firstValue, secondValue)

        let getPQ _ = 
            let randomNumber = Random().Next(minRandomValue, maxRandomValue)

            let p = findNumber randomNumber isPrime
            let q = findNumber (p + 1L) isPrime

            (p, q)

        let getD p q =
            let num = (p - 1L) * (q - 1L)
            findNumber (p + q + 1L) (areCoprime num)

        let getE d p q = 
            let modValue = (p - 1L) * (q - 1L)
            let condition d p q e = ((e % modValue) * (d % modValue)) % modValue = 1L
            findNumber 2 (condition d p q)

        let (p, q) = getPQ()
        let n = p * q
        let d = getD p q
        let e = getE d p q

        (e, d, n)

    let encrypt = Crypto.encrypt

    let decrypt = Crypto.decrypt