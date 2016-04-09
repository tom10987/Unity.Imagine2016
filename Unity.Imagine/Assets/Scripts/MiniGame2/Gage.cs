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

    public Image[] getRangeGageImage { get { return _rangeGageImage; } }

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
        _selectGagePosition = _backgroundGage.rectTransform.anchoredPosition3D;
        _selectGagePosition.x -= _size.x / 2 +5;
        _selectGagePosition.y = _selectGage.rectTransform.anchoredPosition3D.y;
        _selectGagePosition.z = _selectGage.rectTransform.anchoredPosition3D.z;
        _selectGage.rectTransform.anchoredPosition3D = _selectGagePosition;
    }

    public void MoveSelectGage()
    {
        if (_selectGage.rectTransform.anchoredPosition.x >=
            _backgroundGage.rectTransform.anchoredPosition.x + _size.x / 2)
            return;
        _selectGagePosition.x += _speed;
        _selectGage.rectTransform.anchoredPosition = _selectGagePosition;

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
            
            if (_selectGage.rectTransform.anchoredPosition.x >=
                (rangeGage.rectTransform.anchoredPosition.x -
                rangeGage.rectTransform.sizeDelta.x / 2) &&
                _selectGage.rectTransform.anchoredPosition.x <=
                (rangeGage.rectTransform.anchoredPosition.x +
                rangeGage.rectTransform.sizeDelta.x / 2))
            {
                
                score++;
            }
        }

        return _oneRoundOnGageCount = score;

    }

    public void InitGage()
    {
        _selectGagePosition = _backgroundGage.rectTransform.anchoredPosition;
        _selectGagePosition.x -= _size.x / 2 +5;
        _selectGagePosition.y = _selectGage.rectTransform.anchoredPosition.y;
        _selectGage.rectTransform.anchoredPosition = _selectGagePosition;
    }

}
