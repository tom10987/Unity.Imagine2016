using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    //[SerializeField]
    GameObject[] _player = null;
    Shield[] _targetShield = null;
    public Shield[] targetShield { get { return _targetShield; } }

    float power { get; set; }
    public int speedLevel { get; set; }

    [SerializeField]
    float _maxPower = 50.0f;

    [SerializeField]
    float _initPower = 1.0f;

    [SerializeField]
    float _addPowerValue = 1.0f;

    Rigidbody _rigidbody;

    bool _stopFlag = false;
    public bool stopFlag { get { return _stopFlag; } }

    bool _moveFlag = false;
    public bool moveFlag
    {
        get { return _moveFlag; }
    }

    bool startFlag = false;

    ARDeviceManager _manager = null;
    public ARDeviceManager manager
    {
        set { _manager = value; }
    }

    public GameController gameController
    {
        get; set;
    }

    enum Target{
        Player1,
        Player2,
    }
    Target target = Target.Player1;

    Vector3 _playerPosOffset = new Vector3(0.0f, 0.0f, 0.0f);

    [SerializeField]
    GameObject[] _hitParticle = null;

    public GameManager gameManager { get; set; }

    // Use this for initialization
    void Start () {
        _rigidbody = GetComponent<Rigidbody>();
        power = _initPower;

        target = (Target)Random.Range(0, 2);
        //Vector3 offset = new Vector3(0.0f, 1.0f, 0.0f);
        //transform.position += (offset);
    }
	
	// Update is called once per frame
	void Update () {
        if (!startFlag)
        {
            //var key1P = gameController.player1.GetEnumerator();
            //var key2P = gameController.player2.GetEnumerator();
            //key1P.MoveNext();
            //key2P.MoveNext();
            if (GameController.instance.IsGameStart())
            {
                startFlag = true;
            }
            return;
        }

        if (_stopFlag || !FindPlayer())
        {
            _moveFlag = false;
            _rigidbody.velocity = Vector3.zero;
            return;
        }
        else
        {
            _moveFlag = true;
        }
        ThrowBall();
        RefereeTextUpdate();
    }

    void RefereeTextUpdate()
    {
        gameManager.referee.textBox.text = "スピードレベル\n" + speedLevel;
    }

    bool FindPlayer()
    {
        // モデルがなかったら抜ける
        int playerNum = 0;
        var models = _manager.models.GetEnumerator();
        while (models.MoveNext())
        {
            ++playerNum;
        }
        if(playerNum != 2 ||
           _manager.player1 == null ||
           _manager.player2 == null ||
           !_manager.player1.isVisible ||
           !_manager.player2.isVisible)
        {
            return false;
        }
        _player = new GameObject[2];
        _player[0] = _manager.player1.gameObject;        
        _player[1] = _manager.player2.gameObject;

        // 狙う盾を入れる
        _targetShield = new Shield[2];
        _targetShield[0] = _player[0].transform.GetComponentInChildren<Shield>();
        _targetShield[1] = _player[1].transform.GetComponentInChildren<Shield>();

        // ヒットエフェクトを入れる
        _targetShield[0].hitParticle = _hitParticle[0];
        _targetShield[1].hitParticle = _hitParticle[1];

        //if (_player[0].transform.parent.GetComponentInChildren<Shield>().hitParticle == null)
        //{
        //    _player[0].transform.parent.GetComponentInChildren<Shield>().hitParticle = _hitParticle[0];
        //}
        //if (_player[1].transform.parent.GetComponentInChildren<Shield>().hitParticle == null)
        //{
        //    _player[1].transform.parent.GetComponentInChildren<Shield>().hitParticle = _hitParticle[1];
        //}
        //if(_player[0] == null || _player == null) { return false; }

        return true;
    }

    // ボールの飛ぶ挙動
    void ThrowBall()
    {
       // Debug.Log((int)target);

        //Vector3 vectorPower = _player[(int)target].transform.position + _playerPosOffset - transform.position;

        //float ballLength = Vector3.Distance(_player[(int)target].transform.position, transform.position);
        //float playerLength = Vector3.Distance(_player[0].transform.position, _player[1].transform.position);
        //float nomarize = ballLength / playerLength;
        //Vector3 sin = new Vector3(0.0f, -Mathf.Cos(nomarize * Mathf.PI), 0.0f);

        //float offsetY = 7.0f;
        //Vector3 offset = new Vector3(0.0f, offsetY / 2.5f, 0.0f);
        //_rigidbody.velocity = vectorPower.normalized * power + sin * offsetY + offset;
        //_rigidbody.AddForce(vectorPower.normalized);

        ///////////////////////////////////////////////////////////////////////////////////////

        //Vector3 pos = (_player[0].transform.position - _player[1].transform.position) * 0.5f;
        //Vector2 heightPlayer = new Vector2(pos.x, pos.z);
        //float lengthPlayer = Mathf.Sqrt(heightPlayer.x * heightPlayer.x + heightPlayer.y * heightPlayer.y);

        //Vector3 vectorPower = _player[(int)target].transform.position + _playerPosOffset - transform.position;
        //Vector2 heightBall = new Vector2(vectorPower.x, vectorPower.z);
        //float lengthBall = Mathf.Sqrt(heightBall.x * heightBall.x + heightBall.y * heightBall.y);
        //_rigidbody.velocity = vectorPower.normalized * power - new Vector3(0.0f, lengthPlayer - lengthBall * 2.0f, 0.0f);

        ///////////////////////////////////////////////////////////////////////////////////////

        Vector3 targetlenght = Vector3.zero;
        if(target == Target.Player1)
        {
           targetlenght = _targetShield[1].transform.position - _targetShield[0].transform.position;
        }
        else if(target == Target.Player2)
        {
            targetlenght = _targetShield[0].transform.position - _targetShield[1].transform.position;
        }
        else
        {
            // たぶん通ることはない
            _rigidbody.velocity = Vector3.zero;
            return;
        }
        Vector3 ballLength = _targetShield[(int)target].transform.position - transform.position;
        float value = (targetlenght - ballLength).sqrMagnitude / targetlenght.sqrMagnitude;

        Vector3 vector = _targetShield[(int)target].transform.position - transform.position;

        const float MODEL_SIZE = 50.0f;
        _rigidbody.velocity = ((vector.normalized) + (Vector3.up * Mathf.Sin(value / Mathf.PI))) * MODEL_SIZE;
        _rigidbody.velocity *= power;
    }

    // ボールを飛ばすターゲットを切り替える
    public void ChangeTarget()
    {
        target = target == Target.Player1 ? Target.Player2 : Target.Player1;
        AddPower();

        gameManager.audio.Play(ClipIndex.se_No31_Floating);
    }

    // ボールの速さを増やす
    void AddPower()
    {
        if (power >= _maxPower) { return; }
        power += power < _maxPower ? _addPowerValue : 0;
        ++speedLevel;
    }

    // ボールを止める(呼ぶともうボールは動かなくなります)
    public void StopBall()
    {
        _stopFlag = true;
        _rigidbody.velocity = Vector3.zero;
    }
  
    public void SetPlayers(GameObject[] players)
    {
        _player = players;

        //Vector3 pos = (_player[0].transform.position - _player[1].transform.position) * 0.5f;
        //transform.position = pos;
        //Vector2 height = new Vector2(pos.x, pos.z);
        //float length = Mathf.Sqrt(height.x * height.x + height.y * height.y);
        //transform.position += new Vector3(0.0f, length, 0.0f);
    }
}
