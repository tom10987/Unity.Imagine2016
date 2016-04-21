
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

//------------------------------------------------------------
// NOTICE:
// AR マーカーをうまく認識できない場合の
// ヒントを表示するキャンバスを管理する
//
//------------------------------------------------------------
// TIPS:
// ボタンのパラメータを外部から変更させたくないので、
// AddListerner メソッドのみ外部からアクセスできるようにしています
//
//------------------------------------------------------------

public class MarkerHintCanvas : MonoBehaviour
{
  [SerializeField]
  Button _button = null;

  /// <summary> ボタンのコールバック処理を登録する </summary>
  public void AddListener(UnityAction callBack)
  {
    // TIPS:
    // 外部で生成したインスタンスをデリゲートに持たせても削除できないので、
    // 内部にてインスタンス削除の命令を追加してボタンに渡す
    UnityAction call = () =>
    {
      callBack();
      Destroy(gameObject);
    };
    _button.onClick.AddListener(call);
  }
}
