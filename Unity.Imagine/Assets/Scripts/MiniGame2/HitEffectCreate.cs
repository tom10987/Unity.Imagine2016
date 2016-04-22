using UnityEngine;
using System.Collections;

public class HitEffectCreate : MonoBehaviour
{
    [SerializeField]
    GameObject[] _effectObject;

   

	GameManager _gameManager;

    bool _isFiring = false;

    Vector3 effctPosition = new Vector3(0, 0, 0);

    int _movedDistance = 1;

    [SerializeField]
    float wave = 50;


    [SerializeField]
    float _effectTime = 2;

    [SerializeField]
    float _speed = 5;

    float _time;

    static bool _playerHit = false;

	bool _isSE = false;

    enum Player
    {
        Player1,
        Player2
    }
    [SerializeField]
    Player _player = Player.Player1;

    void Start()
    {
        _time = _effectTime;
        if(_player == Player.Player2)
        {
            _movedDistance = -1;
        }
       
		_gameManager = FindObjectOfType<GameManager> ();   
    }

    void Update()
    {
        LaserJostle();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.GetComponent<HitEffectCreate>() == null) return;
		float distance = Vector3.Distance(_gameManager.player1.gameObject.GetComponent<ChargePlayer>().gameObject.transform.position, _gameManager.player2.gameObject.GetComponent<ChargePlayer>().gameObject.transform.position);
        

        foreach (GameObject effect in _effectObject)
        {
            effect.SetActive(true);
            effctPosition = effect.transform.localPosition;
            effctPosition.z = distance / 2;
            effect.transform.localPosition = effctPosition;
            _isFiring = true;
            effctPosition.z -= wave / 2;
        }
    }

    public void LaserJostle()
    {
        if (_isFiring == false) return;

        if (_time <= 0)
        {
            LaserPushBack();
            return;
        }

        _time -= Time.deltaTime;
            effctPosition.z  +=  Mathf.Sin(Time.frameCount* 50 * _movedDistance) * wave;
        
        foreach (GameObject effect in _effectObject)
        {
            effect.transform.localPosition = effctPosition;
        }
    }

    void LaserPushBack()
    {

		if( _gameManager.player1.gameObject.GetComponent<ChargePlayer>().getTotalScorePlayer1 > _gameManager.player2.gameObject.GetComponent<ChargePlayer>().getTotalScorePlayer2)
        {


            if (effctPosition.z <= 0)
            {
				if (!_isSE) {
					_gameManager.audio.Play (ClipIndex.se_No27_BeamClash,0.5f);
					_isSE = true;
				}
                _playerHit = true;
            }
            if (_playerHit) return;

            effctPosition.z += _speed * _movedDistance;
            foreach (GameObject effect in _effectObject)
            {
                effect.transform.localPosition = effctPosition;
            }
        }
        else
			if (_gameManager.player1.gameObject.GetComponent<ChargePlayer>().getTotalScorePlayer1 < _gameManager.player2.gameObject.GetComponent<ChargePlayer>().getTotalScorePlayer2)
        {

            if(effctPosition.z <= 0)
            {
					if (!_isSE) {
						_gameManager.audio.Play (ClipIndex.se_No27_BeamClash,0.5f);
						_isSE = true;
					}
                _playerHit = true;
            }
            if (_playerHit) return;
            effctPosition.z -= _speed * _movedDistance;
            foreach (GameObject effect in _effectObject)
            {
                effect.transform.localPosition = effctPosition;
            }
        }
    }


}
