using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BackGroundCreater : MonoBehaviour
{

    private enum MoveDirection
    {
        PLUSX,
        PLUSY,
        MINUSX,
        MINUSY,
        PLUSXPLUSY,
        PLUSXMINUSY,
        MINUSXPLUSY,
        MINUSXMINUSY,
    }

    [SerializeField, Tooltip("動く方向を決める")]
    private MoveDirection _direction;

    [SerializeField, Tooltip("使うパネルを設定(標準で子に入ってるPanelを選択)")]
    private GameObject _backGroundPanel;
    [SerializeField, Tooltip("背景画像の選択")]
    private Sprite _backGroundTexture;

    [SerializeField, Tooltip("動くスピードを決める")]
    private float _moveSpeed = 0.1f;

    //パネルの枚数を決める(方向によって枚数は変わる)
    private int _panelNumber = 0;

    private List<GameObject> _panel = null;

    private Vector3 _startPos = Vector3.zero;

    void Start()
    {
        _panel = new List<GameObject>();
        if (_direction == MoveDirection.PLUSX || _direction == MoveDirection.MINUSX)
        {
            _panelNumber = 6;
            _startPos = new Vector3(-1500, -900, 0);
            for (int i = 0; i < _panelNumber; i++)
            {
                var panel = Instantiate(_backGroundPanel) as GameObject;
                _panel.Add(panel);
                _panel[i].SetActive(true);
                _panel[i].transform.SetParent(transform);
                _panel[i].GetComponent<Image>().sprite = _backGroundTexture;
                _panel[i].name = "Panel" + (i + 1);
                //子供にした後に座標を調整する必要がある。
                _panel[i].transform.localScale = Vector3.one;
                _panel[i].transform.localPosition = _startPos;
                _startPos.x += 1075;
                if(i == _panelNumber / 2 - 1)
                {
                    _startPos.x = -1500;
                    _startPos.y += 1080;
                }
            }

            if (_direction == MoveDirection.PLUSX)
            {
                StartCoroutine(PlusX());
            }
            else
            {
                StartCoroutine(MinusX());
            }
        }
        else if (_direction == MoveDirection.PLUSY || _direction == MoveDirection.MINUSY)
        {
            _panelNumber = 6;
            _startPos = new Vector3(-500, -1500, 0);
            for (int i = 0; i < _panelNumber; i++)
            {
                var panel = Instantiate(_backGroundPanel) as GameObject;
                _panel.Add(panel);
                _panel[i].SetActive(true);
                _panel[i].transform.SetParent(transform);
                _panel[i].GetComponent<Image>().sprite = _backGroundTexture;
                _panel[i].name = "Panel" + (i + 1);

                _panel[i].transform.localScale = Vector3.one;
                _panel[i].transform.localPosition = _startPos;
                _startPos.y += 1075;
                if (i == _panelNumber / 2 - 1)
                {
                    _startPos.x += 1080;
                    _startPos.y = -1500;
                }
            }

            if (_direction == MoveDirection.PLUSY)
            {
                StartCoroutine(PlusY());
            }
            else
            {
                StartCoroutine(MinusY());
            }
        }
        else if (_direction == MoveDirection.PLUSXPLUSY || _direction == MoveDirection.MINUSXMINUSY
            || _direction == MoveDirection.PLUSXMINUSY || _direction == MoveDirection.MINUSXPLUSY)
        {
            _panelNumber = 6;
            _startPos = new Vector3(-1500, -1080, 0);
            for (int i = 0; i < _panelNumber; i++)
            {
                var panel = Instantiate(_backGroundPanel) as GameObject;
                _panel.Add(panel);
                _panel[i].SetActive(true);
                _panel[i].transform.SetParent(transform);
                _panel[i].GetComponent<Image>().sprite = _backGroundTexture;
                _panel[i].name = "Panel" + (i + 1);

                _panel[i].transform.localScale = Vector3.one;
                _panel[i].transform.localPosition = _startPos;
                _startPos.x += 1075;
                if (i == _panelNumber / 2 - 1)
                {
                    _startPos.x = -1500;
                    _startPos.y += 1080;
                }
            }

            if (_direction == MoveDirection.PLUSXPLUSY)
            {
                StartCoroutine(PlusXPlusY());
            }
            else if (_direction == MoveDirection.MINUSXMINUSY)
            {
                StartCoroutine(MinusXMinusY());
            }
            else if (_direction == MoveDirection.PLUSXMINUSY)
            {
                StartCoroutine(PlusXMinusY());
            }
            else
            {
                StartCoroutine(MinusXPlusY());
            }
        }
    }

    IEnumerator PlusX()
    {
        while (true)
        {
            foreach (var panel in _panel)
            {
                panel.transform.Translate(_moveSpeed, 0.0f, 0.0f);
                if (panel.transform.localPosition.x > 1550)
                {
                    panel.transform.localPosition = new Vector3(-1670, panel.transform.localPosition.y, 0);
                }
            }
            yield return null;
        }
    }

    IEnumerator MinusX()
    {
        while (true)
        {
            foreach (var panel in _panel)
            {
                panel.transform.Translate(-_moveSpeed, 0.0f, 0.0f);
                if (panel.transform.localPosition.x < -1550)
                {
                    panel.transform.localPosition = new Vector3(1670, panel.transform.localPosition.y, 0);
                }
            }
            yield return null;
        }
    }

    IEnumerator PlusY()
    {
        while (true)
        {
            foreach (var panel in _panel)
            {
                panel.transform.Translate(0.0f, _moveSpeed, 0.0f);
                if (panel.transform.localPosition.y > 1550)
                {
                    panel.transform.localPosition = new Vector3(panel.transform.localPosition.x, -1670, 0);
                }
            }
            yield return null;
        }
    }

    IEnumerator MinusY()
    {
        while (true)
        {
            foreach (var panel in _panel)
            {
                panel.transform.Translate(0.0f, -_moveSpeed, 0.0f);
                if (panel.transform.localPosition.y < -1550)
                {
                    panel.transform.localPosition = new Vector3(panel.transform.localPosition.x, 1670, 0);
                }
            }
            yield return null;
        }
    }

    IEnumerator PlusXPlusY()
    {
        while (true)
        {
            foreach (var panel in _panel)
            {
                panel.transform.Translate(_moveSpeed, _moveSpeed, 0.0f);

                if (panel.transform.localPosition.x > 1550)
                {
                    panel.transform.localPosition = new Vector3(-1670, panel.transform.localPosition.y, 0);
                }
                if (panel.transform.localPosition.y > 1080)
                {
                    panel.transform.localPosition = new Vector3(panel.transform.localPosition.x, -1075, 0);
                }
            }
            yield return null;
        }
    }

    IEnumerator MinusXMinusY()
    {
        while (true)
        {
            foreach (var panel in _panel)
            {
                panel.transform.Translate(-_moveSpeed, -_moveSpeed, 0.0f);

                if (panel.transform.localPosition.x < -1550)
                {
                    panel.transform.localPosition = new Vector3(1670, panel.transform.localPosition.y, 0);
                }
                if (panel.transform.localPosition.y < -1080)
                {
                    panel.transform.localPosition = new Vector3(panel.transform.localPosition.x, 1075, 0);
                }
            }
            yield return null;
        }
    }

    IEnumerator PlusXMinusY()
    {
        while (true)
        {
            foreach (var panel in _panel)
            {
                panel.transform.Translate(_moveSpeed, -_moveSpeed, 0.0f);

                if (panel.transform.localPosition.x > 1550)
                {
                    panel.transform.localPosition = new Vector3(-1670, panel.transform.localPosition.y, 0);
                }
                if (panel.transform.localPosition.y < -1080)
                {
                    panel.transform.localPosition = new Vector3(panel.transform.localPosition.x, 1075, 0);
                }
            }
            yield return null;
        }
    }

    IEnumerator MinusXPlusY()
    {
        while (true)
        {
            foreach (var panel in _panel)
            {
                panel.transform.Translate(-_moveSpeed, _moveSpeed, 0.0f);

                if (panel.transform.localPosition.x < -1550)
                {
                    panel.transform.localPosition = new Vector3(1670, panel.transform.localPosition.y, 0);
                }
                if (panel.transform.localPosition.y > 1080)
                {
                    panel.transform.localPosition = new Vector3(panel.transform.localPosition.x, -1075, 0);
                }
            }
            yield return null;
        }
    }
}
