using UnityEngine;
using System.Collections.Generic;

public class Barrage : ActionManager
{
    [SerializeField]
    ARDeviceManager _arDevMgr = null; 

    [SerializeField]
    AudioPlayer _audioPlayer;

    [SerializeField]
    StartCount _startCount;

    [SerializeField]
    GameObject[] _bulletObj = null;

    [SerializeField]
    GameObject _specialBulletObj = null;

    [SerializeField]
    TimeCount _timeCount = null;

    [SerializeField]
    int _probability = 0;

    [SerializeField]
    float _waitTime = 0.2f;

    List<ARModel> _player = new List<ARModel>();

    private enum SelectPlayer
    {
        Player1,
        Player2
    }

    private GameObject _enemy;

    private int _keyCount = 0;

    int _count = 0;

    RandomBullet _randomBullet;

    public int _getKeyCount { get { return _keyCount; } }

    CharacterData _characterData;

    public GameObject getCustomGameObject { get { return _characterData.gameObject; } }

    void Start()
    {
        //_player = _keyAction.GetPlayers();

        if (_audioPlayer == null)
        {
            _audioPlayer = GameObject.Find("AudioPlayer").GetComponent<AudioPlayer>();
        }

        if (_startCount == null)
        {
            _startCount = GameObject.Find("StartCount").GetComponent<StartCount>();
        }

        if (_timeCount == null)
        {
            _timeCount = GameObject.Find("Time").GetComponent<TimeCount>();
        }

        _randomBullet = GetComponent<RandomBullet>();

        _characterData = GetComponentInChildren<CharacterData>();

    }

    void Update(){
        bool isPlayer = (_arDevMgr.player1 == null || _arDevMgr.player2 == null);
        if (isPlayer)
        {
            _player.Clear();
            _player.Add(_arDevMgr.player1);
            _player.Add(_arDevMgr.player2);
            //_player = _keyAction.GetPlayers();
        }
    }

    int Barragebutton(ARModel model)
    {
        if (_timeCount.time <= 1) return 0;
        if (model.inputKey())
        {
            _audioPlayer.Play(20, false);
            StartCoroutine(BulletCreate(_waitTime));
            return 1;
        }

        return 0;
    }

    IEnumerator<WaitForSeconds> BulletCreate(float waitTime)
    {
        if (_timeCount.time <= 1) yield break;
        if (_player[0].gameObject == gameObject)
        {
            Bullet(_bulletObj[0]);
        }else
        if (_player[1].gameObject == gameObject)
        {
            Bullet(_bulletObj[1]);
        }
        _count++;
        if (_probability > _count) yield break;
        if (_randomBullet.StatusRandomBullet() == false) yield break;
        _count = 0;
        yield return new WaitForSeconds(waitTime);
        //Debug.Log("来てる");
        Bullet(_specialBulletObj);
        _keyCount++;
        yield return new WaitForSeconds(0);
    }


    public override void Action(ARModel model)
    {
        // もういらない
        //Vector3 myRotate = transform.eulerAngles;
        //transform.LookAt(Enemy.transform);
        //rotation = transform.eulerAngles;
        //transform.eulerAngles = myRotate;

        if (_startCount.countFinish)
        {
            _keyCount += Barragebutton(model);
        }
    }

    public void Bullet(GameObject bullet)
    {
        _enemy = Enemy.GetComponentInChildren<CharacterData>().gameObject;
        var obj = Instantiate(bullet);
        obj.name = bullet.name;
        obj.transform.position = transform.position;
        var value = _enemy.transform.position - transform.position;
        obj.GetComponent<BulletShot>()._vectorValue = value.normalized;
        obj.GetComponent<BulletShot>()._parent = _enemy;
    }

}
