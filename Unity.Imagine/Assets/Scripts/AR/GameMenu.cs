
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//------------------------------------------------------------
// NOTICE:
// ゲームシーンの UI(ボタン) を管理する
//
//------------------------------------------------------------
// TIPS:
// start と back のボタンは、外部でコールバック処理を登録します
//
//------------------------------------------------------------

public class GameMenu : MonoBehaviour
{
  [SerializeField]
  Button _start = null;
  public Button start { get { return _start; } }

  [SerializeField]
  Button _back = null;
  public Button back { get { return _back; } }

  [SerializeField]
  Button _hint = null;
  public Button hint { get { return _hint; } }

  [SerializeField]
  CanvasGroup _group = null;
  public CanvasGroup group { get { return _group; } }


  [SerializeField]
  [Tooltip("AR マーカー認識のヒントを表示するプレハブを指定")]
  MarkerHintCanvas _hintCanvas = null;


  void Start() { _hint.onClick.AddListener(OnCreateHintCanvas); }

  // TIPS: ヒント表示キャンバスを生成する
  void OnCreateHintCanvas()
  {
    MarkerHintCanvas canvas = Instantiate(_hintCanvas);

    // 生成したキャンバスが削除されるときにボタンを復旧する
    canvas.AddListener(() => _group.interactable = true);

    // ボタンを無効化
    _group.interactable = false;
  }


  /// <summary> ボタンを全て無効化 </summary>
  public void ButtonSetActive(bool state)
  {
    _start.interactable = state;
    _back.interactable = state;
    _hint.interactable = state;
  }

  /// <summary> 全てのボタンのアルファ値を指定した値に変更する <para>
  /// TIPS: <see cref="CanvasGroup"/> に対する操作ではない </para></summary>
  public void ButtonSetAlpha(float alpha)
  {
    _start.image.SetAlpha(alpha);
    _back.image.SetAlpha(alpha);
    _hint.image.SetAlpha(alpha);
  }

  /// <summary> 戻るボタンだけ有効化 </summary>
  public void BackMenuActivate()
  {
    ButtonSetActive(false);
    _back.interactable = true;
    ButtonSetAlpha(0f);
    _back.image.SetAlpha(1f);
  }
}

public static class ImageExtension
{
  /// <summary> color プロパティのアルファ値を変更 </summary>
  public static void SetAlpha(this Image image, float alpha)
  {
    Color temp = image.color;
    temp.a = alpha;
    image.color = temp;
  }
}
