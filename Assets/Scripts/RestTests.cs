using System.Threading.Tasks;
using UnityEngine;
using NativeWebSocket;
using Newtonsoft.Json;
using System.Text;
using UnityEngine.UI;
using System;

public class RestTests : MonoBehaviour
{
    [SerializeField] private Image _connectionIndicator;

    WebSocket websocket;
    private Config _target;

    public static Action<bool> OnConnectionStatusChenget;
    public static Action<string, string> OnReconnect;

    [System.Serializable]
    public struct PostStruct
    {
        public string operation;
    }

    private async void Awake()
    {
        OnConnectionStatusChenget += SetConnectionStatus;
        OnReconnect += Reconnect;
        _target = await ConfigLoader.GetConfig();
        ConnectToWS();
    }

    private void SetConnectionStatus(bool obj)
    {
        if(!obj)
            _connectionIndicator.color = Color.red;
        else
            _connectionIndicator.color = Color.green;
    }

    private async void Reconnect(string port, string ip)
    {
        if (websocket.State == WebSocketState.Open) await websocket.Close();
        _target = new Config(port, ip);
        ConnectToWS();
    }

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

    private async void Messeges()
    {   
        PostStruct test = new PostStruct()
        {
            operation = "getCurrentOdometer"
        };

        PostStruct test2 = new PostStruct()
        {
            operation = "getRandomStatus"
        };

        string data = JsonConvert.SerializeObject(test2);

        while (Application.isPlaying)
        {
            if (websocket.State != WebSocketState.Open) return;
            await websocket.Send(Encoding.UTF8.GetBytes(data));
            await websocket.SendText(data);
            await Task.Delay(10000);
        }
    }

    void Update()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        websocket.DispatchMessageQueue();
#endif
    }

    private async void OnApplicationQuit()
    {
        await websocket.Close();
        Debug.Log("application us close");
    }
}
