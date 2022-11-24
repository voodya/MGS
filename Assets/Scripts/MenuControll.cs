
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using DG.Tweening;

public class MenuControll : MonoBehaviour
{
    [Header("Menu elements")]
    [SerializeField] private Button _musicToggle;
    [SerializeField] private Button _soundToggle;
    [SerializeField] private TMP_InputField _ipField;
    [SerializeField] private TMP_InputField _portField;
    [SerializeField] private TMP_InputField _streamField;
    [SerializeField] private Slider _musicVolume;
    [SerializeField] private Slider _soundVolume;

    [Space]
    [Header("HamburgerBtn")]
    [SerializeField] private Button _openCloseBtn;

    [Space]
    [Header("Control elements")]
    [SerializeField] private AudioSource _audioSourceMusic;
    [SerializeField] private AudioSource _audioSourceSound;

    private bool _menuState = false;
    private bool _musicState = false;
    private bool _soundState = false;

    private RectTransform _menuRect;

    private TextMeshProUGUI _musicToggleText;
    private TextMeshProUGUI _soundToggleText;

    public static Action OnMuteMusic;

    private string _port;
    private string _ip;

    private void Awake()
    {
        OnMuteMusic += MuteMusic;
        _musicToggle.onClick.AddListener(ChangeMusicState);
        _soundToggle.onClick.AddListener(ChangeSoundState);
        _ipField.onEndEdit.AddListener(SetIp);
        _portField.onEndEdit.AddListener(SetPort);
        _streamField.onEndEdit.AddListener(SetStream);
        _musicVolume.onValueChanged.AddListener(SetMusicVolume);
        _soundVolume.onValueChanged.AddListener(SetSoundVolume);
        _openCloseBtn.onClick.AddListener(ChangeMenuState);
        _menuRect = GetComponent<RectTransform>();
        _musicToggleText = _musicToggle.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _soundToggleText = _soundToggle.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    private void MuteMusic()
    {
        _musicState = true;
        _audioSourceMusic.mute = true;
        _musicToggleText.text = "Music: Off";
    }

    private void ChangeMenuState()
    {
        AudioControl.OnPlayClickSound?.Invoke();
        if (!_menuState)
        {
            _menuRect.DOAnchorPos(Vector2.zero, 0.5f);
        }
        else
        {
            _menuRect.DOAnchorPos(new(-Screen.width, 0), 0.5f);
            if (!string.IsNullOrEmpty(_port) && !string.IsNullOrEmpty(_ip)) RestTests.OnReconnect?.Invoke(_port, _ip);
        }
        _menuState = !_menuState;
    }

    private void SetSoundVolume(float arg0)
    {
        _audioSourceSound.volume = arg0;
    }

    private void SetMusicVolume(float arg0)
    {
        _audioSourceMusic.volume = arg0;
    }

    private void SetStream(string arg0)
    {
        VideoControll.OnSetVideoSource?.Invoke(arg0);
    }

    private void SetPort(string arg0)
    {
        AudioControl.OnPlayClickSound?.Invoke();
        if (!string.IsNullOrEmpty(arg0)) _port = arg0;
    }

    private void SetIp(string arg0)
    {
        AudioControl.OnPlayClickSound?.Invoke();
        if (!string.IsNullOrEmpty(arg0)) _ip = arg0;
    }

    private void ChangeSoundState()
    {
        
        _soundState = !_soundState;
        _audioSourceSound.mute = _soundState;
        if (!_soundState)
            _soundToggleText.text = "Sound: On";
        else
            _soundToggleText.text = "Sound: Off";
        AudioControl.OnPlayClickSound?.Invoke();
    }

    private void ChangeMusicState()
    {
        _musicState = !_musicState;
        _audioSourceMusic.mute = _musicState;
        if (!_musicState)
            _musicToggleText.text = "Music: On";
        else
            _musicToggleText.text = "Music: Off";
        AudioControl.OnPlayClickSound?.Invoke();
    }
}
