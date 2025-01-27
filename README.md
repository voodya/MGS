# Test task junior 2021 WebSocket connection (Legacy)

https://github.com/user-attachments/assets/2461c4ef-1e66-4df5-8c8d-19cf43505552

# Connect example

``` C#
private async void ConnectToWS()
{
    Debug.Log("application us running");

    string Targeturl = $"ws://{_target.Ip}:{_target.Port}/ws";

    websocket = new WebSocket(Targeturl);
    
    websocket.OnOpen += () =>
    {
        Debug.Log("Connection open!");
        _connectionIndicator.color = Color.green;
        Messeges();
    };

    websocket.OnError += async (e) =>
    {
        if (websocket.State != WebSocketState.Open) return;
        Debug.Log("Error! " + e);
        _connectionIndicator.color = Color.red;
        Alert.OnShowAlert?.Invoke($"Disconnected or some error to {Targeturl}");
        await websocket.Connect();
    };

    websocket.OnClose += (e) =>
    {
        Debug.Log("Connection closed!");
        _connectionIndicator.color = Color.red;
    };

    websocket.OnMessage += (bytes) =>
    {
        var message = Encoding.UTF8.GetString(bytes);
        Debug.Log(message);
        Parser.ParseJson(message);
    };

    await websocket.Connect();
}
```
