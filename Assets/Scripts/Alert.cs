using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using System.Threading.Tasks;

public class Alert : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _alertText;
    [SerializeField] private GameObject _alert;

    public static Action<string> OnShowAlert;
    
    private void Awake()
    {
        OnShowAlert += ShowAlert;
    }

    private async void ShowAlert(string obj)
    {
        _alert.gameObject.SetActive(true);
        _alertText.text = obj;
        await Task.Delay(2000);
        _alert.gameObject.SetActive(false);
    }
}
