using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Shield : MonoBehaviour {

    [SerializeField]
    private int _initHp = 100;

    [SerializeField]
    private int _hp = 0;
    public int hp
    {
        get
        {
            return _hp;
        }

        set
        {
            _hp = value;
            if (_hp < 0) { _hp = 0; }        
        }
    }
    [SerializeField]
    private int _maxDamage = 20;
    [SerializeField]
    private int _maxArmor = 20;
    private int _armor = 0;

    //[SerializeField]
    //private string _ballName = "Ball";

    public int defenseParmater { get; set; }

    //Pendulum pendulum = null;

    private Vector3 _particleOffsetPos = new Vector3(0.0f, 0.5f, 0.0f);

    public GameObject hitParticle { get; set; }

    private GameObject _breakParticle = null;
    public GameObject breakParticle {
        get
        {
            if(_breakParticle == null)
            {
                _breakParticle = Resources.Load("MiniGame/Pendulum/Effect/yellow") as GameObject;
            }
            return _breakParticle;
        }
    }

    IEnumerator<float> _move = null;

    private float _hitDelayCount = 0;
    private const float _MAX_HIT_DELAY_COUNT = 30;

    // Use this for initialization
    void Start() {
        //pendulum = GetComponentInParent<Pendulum>();
        Reset();
    }

    // Update is called once per frame
    void Update() {
        /*pendulum == null || */
        if (hp == 0)
        {
            Destroy(gameObject);
        }

        if (_move != null && !_move.MoveNext())
        {
            _move = null;
        }

        if (_hitDelayCount != 0)
        {
            --_hitDelayCount;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        var ball = collision.gameObject.GetComponent<Ball>();
        if(ball == null) { return; }
        if (!ball.stopFlag)
        {
            if (_hitDelayCount == 0)
            {
                int damage = _maxDamage - _armor - defenseParmater;
                if(damage < 0) { damage = 0; }
                hp -= damage;
                if(hp == 0) {
                    collision.gameObject.GetComponent<Ball>().StopBall();
                    var particle = Instantiate(breakParticle);
                    particle.transform.position = transform.position + _particleOffsetPos;
                    particle.name = breakParticle.name;
                }
                else
                {
                    var particle = Instantiate(hitParticle);
                    particle.transform.position = transform.position + _particleOffsetPos;
                    particle.name = hitParticle.name;
                }
                _hitDelayCount = _MAX_HIT_DELAY_COUNT;
            }
            collision.gameObject.GetComponent<Ball>().ChangeTarget();
        }
    }

    public void PushOn()
    {
        if(_move != null) { return; }
        _move = PushMove();
    }

    IEnumerator<float> PushMove()
    {
        //float count = 0;
        const int PUSH_FREAM = 3;
        const int PULL_FREAM = 3;
        const float PUSH_SPEED = 40.0f;
        const float PULL_SPEED = 40.0f;
        const float PUSH_VALUE = 0.3f;
        const float PULL_VALUE = -0.3f;
        for (int i = 0; i < PUSH_FREAM; ++i)
        {
            _armor = _maxArmor - _maxArmor / PUSH_FREAM * i;
            transform.Translate(0.0f, 0.0f, PUSH_VALUE * PUSH_SPEED);
            yield return PUSH_VALUE * PUSH_SPEED;
        }
        for (int i = 0; i < PULL_FREAM; ++i)
        {
            _armor -= _maxArmor;
            transform.Translate(0.0f, 0.0f, PULL_VALUE * PULL_SPEED);
            yield return PULL_VALUE * PULL_SPEED;
        }
        _armor = 0;
    }

    //void OnTriggerEnter(Collider collider)
    //{
    //    if (collider.gameObject.name == _ballName)
    //    {
    //        collider.gameObject.GetComponent<Ball>().ChangeTarget();
    //    }
    //}

    public void Reset()
    {
        hp = _initHp;
    }
}