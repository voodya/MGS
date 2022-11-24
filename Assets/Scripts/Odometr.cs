using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class Odometr : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _odometrTextValue;

    private float _odometrValue = 0f;
    private float _odometrTargetValue = 0f;
    private bool _isProcess = false;

    public static Action<float> OdometrValueChenget;

    private void Awake()
    {
        OdometrValueChenget += SmoothValue;
    }

    private async void SmoothValue(float obj)
    {
        if (_isProcess) 
        {
            _odometrTargetValue = obj;
            return;
        }
        if (obj == _odometrValue) return;
        
        _isProcess = true;
        _odometrTargetValue = obj;
        float step = 0f;
        while (step <= 1 && Application.isPlaying)
        {
            step += 0.05f;
            _odometrValue = Mathf.Lerp(_odometrValue, _odometrTargetValue, step);
            _odometrTextValue.text = _odometrValue.ToString();
            Debug.Log("value changing");
            await Task.Delay(100);
        }
        _isProcess = false;
    }
}
