using UnityEngine;
using System.Collections;

/// <summary>
/// このクラスにサウンド系を使わせる。
/// ほかのスクリプトから呼び出すときは、こいつのプロパティを使う
/// </summary>

public class PrintSceneSoundController : MonoBehaviour {

    private AudioPlayer _bgmPlayer = null;
    /// <summary>
    /// BGM専用
    /// </summary>
    public int isPlayBGM
    {
        set
        {
            _bgmPlayer.Stop();
            _bgmPlayer.Play(value, 1.0f, true);
        }
    }
    private AudioPlayer _sePlayer = null;
    /// <summary>
    /// SE専用
    /// </summary>
    public int isPlaySE
    {
        set
        {
            _sePlayer.Play(value);
        }
    }
    [SerializeField]
    private GameObject _audioPlayer = null;

    void Start()
    {
        _bgmPlayer = _audioPlayer.GetComponent<AudioPlayer>();
        _sePlayer = _audioPlayer.GetComponent<AudioPlayer>();
        _bgmPlayer.Play(1, 1.0f, true);
    }

}
