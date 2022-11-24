using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class Parser : MonoBehaviour
{
    public static void ParseJson(string json)
    {
        Data targetMessege = JsonConvert.DeserializeObject<Data>(json);

        switch (targetMessege.Operation)
        {
            case "odometer_val":
                Odometr.OdometrValueChenget?.Invoke(targetMessege.Value);
                break;
            case "currentOdometer":
                Odometr.OdometrValueChenget?.Invoke(targetMessege.GettedValue);
                break;
            case "randomStatus":
                RestTests.OnConnectionStatusChenget?.Invoke(targetMessege.Status == true);
                    if(targetMessege.Status == true) Odometr.OdometrValueChenget?.Invoke(targetMessege.GettedValue);
                break;
            default:
                break;
        }
    }
}

[SerializeField]
public class Data
{
    [JsonProperty("operation")] public string Operation { get; set; }
    [JsonProperty("value")] public float Value { get; set; }
    [JsonProperty("status")] public bool? Status { get; set; }
    [JsonProperty("odometer")] public float GettedValue { get; set; }
}

