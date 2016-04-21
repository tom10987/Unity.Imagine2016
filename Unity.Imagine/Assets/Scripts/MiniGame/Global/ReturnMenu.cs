
using UnityEngine;
using UnityEngine.UI;

public class ReturnMenu : MonoBehaviour {

  [SerializeField]
  AudioPlayer _audioPlayer;

  [SerializeField]
  TimeCount _counter = null;

  [SerializeField]
  ScoreCompare _scoreCompare = null;

  [SerializeField]
  [Tooltip("レフェリーのボード")]
  Text _board = null;

  [SerializeField]
  LayerMask _mask;

  [SerializeField, Range(1f, 5f)]
  float _delayTime = 2.0f;

  [SerializeField, Range(0f, 360f)]
  float _turnAngle = 360.0f;

  [SerializeField, Range(0f, 30f)]
  float _turnSpeed = 20.0f;

  bool _isTurn = false;
  bool _isReturn = false;
  bool _isWinner = false;
  bool _isRotationEnd = false;
  bool _isReturnNenu = false;
  float _time;

  public bool getIsRotationEnd { get { return _isRotationEnd; } }

  void Start() {
    _time = _delayTime;
    _audioPlayer.Play(3, 0.2f, true);
  }

  void Update() {
    Turn();
    ReturnMenuUpdate();
    WaitTime();

    // この１文はタイムが０になったら起こるようにしてあるので
    // 必要なかったり、別のところで必要になったら消してください。
    if (_scoreCompare.getDisplayScore) { WinnerPlayer(); }
  }

  void Turn() {
    if (!_isTurn) { return; }

    _turnAngle -= _turnSpeed;

    if (_turnAngle <= 0.0f) {
      _isTurn = false;

      _isRotationEnd = true;
      _turnAngle = 720.0f;
      _audioPlayer.Play(19, false);
      _isReturnNenu = true;
    }
    transform.eulerAngles = new Vector3(0.0f, _turnAngle, 0.0f);
  }

  void WaitTime() {
    if (!_isReturnNenu) return;
    _time -= Time.deltaTime;
    if (_time <= 0) { ReturnOn(); }
  }

  void WinnerPlayer() {
    if (_isWinner == true) return;

    if (_scoreCompare.getWinPlayer == ScoreCompare.WinPlayer.Player1) {
      _isWinner = true;
      _isTurn = true;
      _board.text = "←";
    }
    else
    if (_scoreCompare.getWinPlayer == ScoreCompare.WinPlayer.Player2) {
      _isWinner = true;
      _isTurn = true;
      _board.text = "→";
    }

  }

  void ReturnMenuUpdate() {
    if (!_isReturn) { return; }
    if (_counter.time > 0) { return; }

    if (_counter != null) { Destroy(_counter); _board.text = "メニューに戻る"; }

    // クリックされてなければスキップ
    if (!TouchController.IsTouchBegan()) { return; }

    // レイを飛ばして、レフェリーじゃなかったらスキップ
    RaycastHit hit;
    if (!TouchController.IsRaycastHitWithLayer(out hit, _mask)) { return; }
    if (hit.transform != transform) { return; }

    _counter.time = _counter.timeLimit;
    ScreenSequencer.instance.SequenceStart(() => GameScene.Menu.ChangeScene(), new Fade(1f));
  }

  // これを呼べばメニューに戻る
  public void ReturnOn() {
    _isTurn = true;
    _isReturn = true;
  }
}
