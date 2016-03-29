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

    public int _score = 0;

    public int _getChargeScore { get { return _score; } }


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

    public int RangeSelectNow()
    {
        //int score = 0;
        if (_rangeGageImage.Length == 0) return 0;

        foreach (Image selectRange in _rangeGageImage)
        {
            if (_selectGage.rectTransform.anchoredPosition.x >=
                selectRange.rectTransform.anchoredPosition.x -
                selectRange.rectTransform.sizeDelta.x / 2 &&
                _selectGage.rectTransform.anchoredPosition.x <=
                selectRange.rectTransform.anchoredPosition.x +
                selectRange.rectTransform.sizeDelta.x / 2)
            {
                _score++;
            }
        }

        return _score;

    }

    public void InitGage()
    {
        _selectGagePosition = _backgroundGage.rectTransform.anchoredPosition;
        _selectGagePosition.x -= _size.x / 2;
        _selectGage.rectTransform.anchoredPosition = _selectGagePosition;
    }

}
