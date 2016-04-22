
using UnityEngine;
using UnityEngine.UI;

public class GameFinish : MonoBehaviour
{
  // TIPS: 各プレイヤー用の表示位置
  [SerializeField]
  Image _player1 = null;
  [SerializeField]
  Image _player2 = null;

  // TIPS: 勝ち、負けの画像
  [SerializeField]
  Sprite _victory = null;
  [SerializeField]
  Sprite _defeat = null;


  [SerializeField]
  [Tooltip("花火のエフェクト")]
  OneTimeEffect _fireworks = null;


  [SerializeField]
  [Tooltip("紙吹雪のエフェクト")]
  EndlessEffect _paper = null;
  // TIPS: 紙吹雪のインスタンス保持
  EndlessEffect _paperInstance = null;

  [SerializeField, Range(0f, 500f)]
  [Tooltip("紙吹雪のエフェクト開始位置：高さ")]
  float _paperOffset = 100f;


  void Start()
  {
    // TIPS: 起動時に隠す
    _player1.enabled = false;
    _player2.enabled = false;
  }


  /// <summary> プレイヤー１勝利 </summary>
  public void WinnerP1() { ActivateImage(true); }
  /// <summary> プレイヤー２勝利 </summary>
  public void WinnerP2() { ActivateImage(false); }

  // TIPS: 画像有効化
  void ActivateImage(bool p1win)
  {
    _player1.enabled = true;
    _player2.enabled = true;
    _player1.sprite = p1win ? _victory : _defeat;
    _player2.sprite = p1win ? _defeat : _victory;
  }


  /// <summary> 花火打ち上げ </summary>
  public OneTimeEffect PlayFireworks(Transform winner)
  {
    var instance = Instantiate(_fireworks);
    instance.transform.position = winner.position;
    return instance;
  }


  /// <summary> 紙吹雪エフェクト開始 </summary>
  public void PaperActivate(Transform winner)
  {
    // TIPS: すでに紙吹雪生成済みならスキップ
    if (_paperInstance != null) { return; }

    _paperInstance = Instantiate(_paper);
    _paperInstance.transform.position = winner.transform.position;
    _paperInstance.transform.position += Vector3.up * _paperOffset;
    _paperInstance.particle.loop = true;
    _paperInstance.particle.Play();
  }
}
