namespace FilesExchanger.Application.IpConvertor

module IpAddressConvertor =
    let ipPortSeparator = '@'
    let ipSeparator = '#'
    
    let encrypt (ip : string) (port : int) =
        let encryptEl el =
            if el = '.' then ipSeparator
            else
                let num = int (el) - int ('0')
                char (int ('a') + num)
        
        let rec run = function
            | [] -> []
            | h::t ->
                let el = encryptEl h
                el :: run t
                
        let encrypt_ip = System.String.Concat(Array.ofList(run (Seq.toList ip)))
        let encrypt_port = System.String.Concat(Array.ofList(run (Seq.toList (port |> string))))
        
        let res = encrypt_ip + (ipPortSeparator |> string) + encrypt_port
        
        res

    let decrypt (encryptIpPort : string) =
        let sep_id = encryptIpPort.IndexOf(ipPortSeparator)
        let encrypt_ip = encryptIpPort.[0..(sep_id - 1)]
        let encrypt_port = encryptIpPort.[(sep_id + 1)..]
        
        let decryptEl el =
            if el = ipSeparator then '.'
            else
                let num = int (el) - int ('a')
                char (int('0') + num)
                
        let rec run = function
            | [] -> []
            | h :: t ->
                let el = decryptEl h
                el :: (run t)
                
        let ip = System.String.Concat(Array.ofList(run (Seq.toList encrypt_ip))) 
        let port = System.String.Concat(Array.ofList(run (Seq.toList encrypt_port))) |> int
        
        (ip, port)
            