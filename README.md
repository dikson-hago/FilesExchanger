# Files Exchanger

## Desclaimer
```
    Current project was and experimental project with F# language. It helps 
people to exchange files in local network, without using popular messangers.
```

## Example of exchanging files
User1 - have to send file<br/>
User2 - have to receive file
### Step 1 (open the page and generate device name)
```
    User1 should open the page http://localhost:5010/post and press 
button "GenerateDeviceName", for generation personal "UserDeviceName_1" in local network.
```
![plot](common/img/send1.png)

### Step 2 (open the page and generate device name)
```
    User2 have to open the page "http://localhost:5010/get" and press 
button "GenerateDeviceName", for generation personal "UserDeviceName_2" in local network.
```
![plot](common/img/get1.png)

### Step 3 (first connection)
```
1. User2 have to tell to the User1 of his "UserDeviceName2". 
2. User2 press button "Wait connection"
3. User1 put UserDeviceName2 into label and press button "Connect"
4. 
    4.1. User1 get the message: "Status: Connected"
    4.2. User2 get the meccage: "Status: Connected"
```
![plot](common/img/get2.png)
![plot](common/img/send3.1.png)

### Step 4
```
1. User2 print the target folder for file from User1,  and press button "Get file"
2. User1 print the location of file, which he want to send and then press button "Send"
3. User1 get message: "Status: OK"
4. User2 get message "Status: OK"
```
![plot](common/img/send4.1.png)
![plot](common/img/get4.png)


## Plans for future
```
- improve compression algorythm (make it faster)
- improve crypto algorythm (add new)
- add button "Choose file"
```
# Profit!
![plot](common/img/post3.png)

## Short description, how it works inside

### Used tools
```
- Websharper for UI
- Haffman algorythm for compression data
- RSA algorythm for encryption data
- Suave server for receiving of client messages
```

### Sending of data process inside
```
1. Data encrypted by RSA algorythm
2. Data compressed by Haffman algorythm
3. Program opens websocket on special port for send file
```

### Receive of data process inside
```
1. Program run Suave server for get new data
2. When program get new data, current data decompressed by Haffman tree
3. Then current data decrypted by RSA algorythm
```