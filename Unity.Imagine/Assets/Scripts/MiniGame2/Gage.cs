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

    float _oneRoundScore = 0;

    public float getOneRoundScore { get { return _oneRoundScore; } }

    bool _isGage = false;

    public bool getIsGage { set { _isGage = value; } }

    [SerializeField]
    float _plusScore = 1;

    void Start()
    {
        _size = _backgroundGage.rectTransform.sizeDelta;
        _selectGagePosition = _backgroundGage.rectTransform.anchoredPosition;
        _selectGagePosition.x -= _size.x / 2;
        _selectGage.rectTransform.anchoredPosition = _selectGagePosition;
    }

    public void MoveSelectGage()
    {
        if (_selectGage.rectTransform.anchoredPosition.x >=
            _backgroundGage.rectTransform.anchoredPosition.x + _size.x / 2) return;
        _selectGagePosition.x += _speed;
        _selectGage.rectTransform.anchoredPosition = _selectGagePosition;

    }

    void Update()
    {
        //_totalScore += RangeSelectNow(); 
    }

    public float  RangeSelectNow()
    {
        //int score = 0;
        if (_rangeGageImage.Length == 0) return 0;
        //if (!_isGage) return 0;
        float score = 0;
        int crossScore = 0;

        foreach (Image selectRange in _rangeGageImage)
        {
            if (_selectGage.rectTransform.anchoredPosition.x >=
                selectRange.rectTransform.anchoredPosition.x -
                selectRange.rectTransform.sizeDelta.x / 2 &&
                _selectGage.rectTransform.anchoredPosition.x <=
                selectRange.rectTransform.anchoredPosition.x +
                selectRange.rectTransform.sizeDelta.x / 2)
            {
                crossScore++;
            }
        }

        _oneRoundScore = crossScore;
        score = crossScore * _plusScore;
        return score;

    }

    public void InitGage()
    {
        _selectGagePosition = _backgroundGage.rectTransform.anchoredPosition;
        _selectGagePosition.x -= _size.x / 2;
        _selectGage.rectTransform.anchoredPosition = _selectGagePosition;
    }

}
