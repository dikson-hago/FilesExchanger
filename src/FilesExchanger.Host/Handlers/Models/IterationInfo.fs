namespace FilesExchanger.Host.Handlers.Models

type SendFileIterationType =
    | Init = 0
    | DeviceNameInit = 1
    | FirstConnectionInit = 2
    | SendFileInit = 3
    
type ReceiveFileIterationType =
    | Init = 0
    | DeviceNameInit = 1
    | FirstConnectionInit = 2
    | ReceiveFileInit = 3

module IterationInfo =
    let mutable sendFileIterationValue = SendFileIterationType.Init
    let mutable receiveFileIterationValue = ReceiveFileIterationType.Init