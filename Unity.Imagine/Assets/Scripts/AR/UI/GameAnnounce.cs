
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//------------------------------------------------------------
// NOTICE:
// カウントダウンなどの通知系統の管理を行う
//
//------------------------------------------------------------
// TIPS:
// TimeCount クラスの hideImage プロパティは内部で使用します
// 外部から値を変更しないでください
//
//------------------------------------------------------------

public class GameAnnounce : MonoBehaviour
{
  [SerializeField]
  [Tooltip("サドンデス：画像")]
  Image _suddenDeath = null;
  /// <summary> サドンデス画像の表示状態 </summary>
  public bool hideSuddenDeath
  {
    get { return _suddenDeath.enabled; }
    set { _suddenDeath.enabled = !value; }
  }


  [SerializeField]
  GameTimeCount _start = null;
  /// <summary> ゲーム開始時のカウントダウン表示 </summary>
  public GameTimeCount startCount { get { return _start; } }

  [SerializeField]
  GameTimeCount _finish = null;
  /// <summary> ゲーム終了時のカウントダウン表示 </summary>
  public GameTimeCount finishCount { get { return _finish; } }


  // TIPS: UI を全て非表示にする
  void Start()
  {
    _suddenDeath.enabled = false;

    _start.hideImage = true;
    _finish.hideImage = true;
  }
}

[System.Serializable]
public class GameTimeCount
{
  [SerializeField]
  [Tooltip("切り替えに使用する Canvas の Image")]
  Image _image = null;
  /// <summary> カウントダウン用の <see cref="Canvas"/> の表示状態 </summary>
  public bool hideImage
  {
    get { return _image.enabled; }
    set { _image.enabled = !value; }
  }

  [SerializeField]
  [Tooltip("カウントダウンに使用する画像リスト")]
  Sprite[] _sprites = null;


  /// <summary> カウントダウン開始 </summary>
  public IEnumerator PlayCountDown()
  {
    _image.enabled = true;

    // TIPS:
    // 小数点以下の値を丸めてインデックスにするため、わざと少し大きい値を使用する
    float time = 3.5f;

    while (time > 0f)
    {
      time -= Time.deltaTime;

      // TIPS: インデックスを生成、画像を切り替える
      int index = Mathf.Clamp(Mathf.RoundToInt(time), 0, _sprites.Length - 1);
      _image.sprite = _sprites[index];

      yield return null;
    }

    _image.enabled = false;
  }
}
