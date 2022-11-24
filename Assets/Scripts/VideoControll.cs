using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoControll : MonoBehaviour
{
    [SerializeField] private Button _playBtn;
    private VideoPlayer _player;
    private string _url;

    public static Action<string> OnSetVideoSource;

    private void Awake()
    {
        OnSetVideoSource += SetUrl;
        _playBtn.onClick.AddListener(PlayVideoStream);
        _player = GetComponent<VideoPlayer>();
    }

    private void SetUrl(string obj)
    {
        _url = obj;
    }

    private void PlayVideoStream()
    {
        if (string.IsNullOrEmpty(_url)) return;
        _player.source = VideoSource.Url;
        _player.url = _url;
        if(_player.audioTrackCount != 0) 
        _player.Play();
    }
}
