using UnityEngine;
using System;
using System.Collections;
using System.IO;

/// <summary>
/// キャラクターを回転する機能
/// </summary>
public class CharacterViewController : MonoBehaviour
{

    [Serializable]
    struct JsonData
    {
        public float SPEED_MAGNIFICATION;
        public Vector3 OFF_SET;
        public float NEAR;
        public float FAR;
        public float FIELD_OF_VIEW;
    }

    [SerializeField, TooltipAttribute("無視したくないLayerNameを設定")]
    string[] _layerNames = null;

    [SerializeField, TooltipAttribute("カメラで写したいターゲット")]
    Transform _target = null;

    [SerializeField, TooltipAttribute("カメラとのオフセット")]
    Vector3 OFF_SET = Vector3.back;

    [SerializeField, Range(0.0f, 10.0f), TooltipAttribute("止まる時間(秒)")]
    float ACCELERATION_TIME = 5.0f;

    [SerializeField, TooltipAttribute("回転速度の倍率")]
    float SPEED_MAGNIFICATION = 0.5f;

    [SerializeField, TooltipAttribute("スワイプの遊び")]
    float REACTION_VALUE = 3.0f;

    [SerializeField]
    bool DEBUG = false;

    Vector3 POINT = Vector3.zero;

    int LAYER_MASK = 0;

    Vector2 _deltaPosition = Vector2.zero;
    MouseUtility _mouseUtility = null;
    float _count = 0.0f;

    AudioPlayer _audioPlayer = null;

    Vector3 START_POSITON = Vector3.zero;

    public void ResetTransform()
    {
        transform.position = START_POSITON;
        transform.rotation = Quaternion.identity;
        _count = 0.0f;
    }

    void Awake()
    {
        var offSet = Vector3.zero;
        offSet.x = OFF_SET.x;
        START_POSITON = transform.position + offSet;
        _count = 0.0f;
    }

    void Start()
    {
        if (_layerNames.Length == 0) throw new IndexOutOfRangeException("index is empty");
        if (_target == null) throw new NullReferenceException("target is empty");

        _mouseUtility = FindObjectOfType<MouseUtility>();
        if (_mouseUtility == null) throw new NullReferenceException("MouseUtility is nothing");

        ReadJson();

        LAYER_MASK = LayerMask.GetMask(_layerNames);

        POINT = _target.position;

        var offSet = OFF_SET + POINT;

        gameObject.transform.position =
            new Vector3(offSet.x, offSet.y, offSet.z);

        StartCoroutine(Control());

        _audioPlayer = FindObjectOfType<AudioPlayer>();

        //gameObject.transform.LookAt(POINT);

    }

    void ReadJson()
    {
        if (DEBUG) return;
        var path = Application.dataPath + "/Json/ModelViewParameter.json";
        if (!File.Exists(path)) return;

        StreamReader sr = new StreamReader(path);
        var data = JsonUtility.FromJson<JsonData>(sr.ReadToEnd());
        sr.Close();

        var camera = GetComponent<Camera>();
        camera.farClipPlane = data.FAR;
        camera.nearClipPlane = data.NEAR;
        camera.fieldOfView = data.FIELD_OF_VIEW;

        OFF_SET = data.OFF_SET;
        SPEED_MAGNIFICATION = data.SPEED_MAGNIFICATION;
    }

    IEnumerator Control()
    {
        while (true)
        {
            Rotate();
            AccelerationRotate();
            StopRotate();
            yield return null;
        }
    }

    void Rotate()
    {
        if (TouchController.IsTouchBegan()) return;
        if (!TouchController.IsTouchMoved()) return;

        RaycastHit raycastHit;
        if (!TouchController.IsRaycastHitWithLayer(out raycastHit, LAYER_MASK)) return;

        if (!(-REACTION_VALUE > _mouseUtility.getDeltaPos.x ||
            REACTION_VALUE < _mouseUtility.getDeltaPos.x)) return;

        if (!_audioPlayer.IsPlaying())
        {
            _audioPlayer.Play(4);
        }

        _count = ACCELERATION_TIME;

        _deltaPosition = TouchController.IsSmartDevice ?
            Input.touches[0].deltaPosition :
            new Vector2(_mouseUtility.getDeltaPos.x, _mouseUtility.getDeltaPos.y);

        _deltaPosition *= SPEED_MAGNIFICATION;

        //gameObject.transform.LookAt(POINT);
        gameObject.transform.RotateAround(POINT, gameObject.transform.up, _deltaPosition.x);
        //gameObject.transform.RotateAround(POINT, -gameObject.transform.right, _deltaPosition.y);
    }

    void StopRotate()
    {
        if (!TouchController.IsTouchBegan()) return;

        RaycastHit raycastHit;
        if (!TouchController.IsRaycastHitWithLayer(out raycastHit, LAYER_MASK)) return;

        _deltaPosition = Vector2.zero;
    }

    void AccelerationRotate()
    {
        if (_count <= 0.0f) return;

        _count += -Time.deltaTime;
        var acceleration = _deltaPosition * _count / ACCELERATION_TIME;

        //gameObject.transform.LookAt(POINT);
        gameObject.transform.RotateAround(POINT, gameObject.transform.up, acceleration.x);
        //gameObject.transform.RotateAround(POINT, -gameObject.transform.right, acceleration.y);
    }

}
