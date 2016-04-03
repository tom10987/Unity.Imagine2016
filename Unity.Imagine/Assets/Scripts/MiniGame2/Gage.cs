using UnityEngine;
using UnityEngine.UI;

public class Gage : MonoBehaviour
{

    [SerializeField]
    private Image _backgroundGage;

    [SerializeField]
    private float _speed = 1;

    [SerializeField]
    private Image[] _rangeGageImage = null;

    private Vector2 _size;

    [SerializeField]
    private Image _selectGage = null;

    private Vector3 _selectGagePosition = new Vector3(0, 0, 0);

    float _oneRoundOnGageCount = 0;

    public float getOneRoundOnGageCount { get { return _oneRoundOnGageCount; } }

    //bool _isGage = false;

    //public bool setIsGage { set { _isGage = value; } }

    public int getRangeGageCount { get { return _rangeGageImage.Length; } }

    void Start()
    {
        _size = _backgroundGage.rectTransform.sizeDelta;
        _selectGagePosition = _backgroundGage.rectTransform.localPosition;
        _selectGagePosition.x -= _size.x / 2;
        _selectGage.rectTransform.localPosition = _selectGagePosition;
    }

    public void MoveSelectGage()
    {
        if (_selectGage.rectTransform.localPosition.x >=
            _backgroundGage.rectTransform.localPosition.x + _size.x / 2)
            return;
        _selectGagePosition.x += _speed;
        _selectGage.rectTransform.localPosition = _selectGagePosition;

    }

    void Update()
    {
       
    }

    public float RangeSelectNow()
    {
        if (_rangeGageImage.Length == 0) return 0;
        int score = 0;


        foreach (Image rangeGage in _rangeGageImage)
        {
            if (_selectGage.rectTransform.localPosition.x >=
                rangeGage.rectTransform.localPosition.x -
                rangeGage.rectTransform.sizeDelta.x / 2 &&
                _selectGage.rectTransform.localPosition.x <=
                rangeGage.rectTransform.localPosition.x +
                rangeGage.rectTransform.sizeDelta.x / 2)
            {
                score++;
            }
        }

        return _oneRoundOnGageCount = score;

    }

    public void InitGage()
    {
        _selectGagePosition = _backgroundGage.rectTransform.localPosition;
        _selectGagePosition.x -= _size.x / 2;
        _selectGage.rectTransform.localPosition = _selectGagePosition;
    }

}
