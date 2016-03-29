using UnityEngine;
using UnityEngine.UI;
using Game.Utility;
using System;

public class PrinterConfig : MonoBehaviour
{
    const int PrinterName = 0;
    const int PrinterColor = 1;

    private Dropdown _data;
    private Dropdown.OptionData _item;

    public static bool _printColor = false;

    [SerializeField, Tooltip("0:プリンター設定, 1:カラー設定")]
    private int _type = PrinterName;

    [SerializeField]
    private GameObject _offScreenCamera = null;

    void Start()
    {
        _data = GetComponent<Dropdown>();
        if (_type == PrinterName)
        {
            if (!PrintDevice.isValid)
            {
                throw new NotSupportedException("認識できるプリンターがありません");
            }
            var printer = PrintDevice.GetPrinterNames().GetEnumerator();
            while (printer.MoveNext())
            {
                _item = new Dropdown.OptionData();
                _item.text = printer.Current;
                _data.options.Add(_item);
            }
            _data.captionText.text = _data.options[0].text;

        }
        else if (_type == PrinterColor)
        {
            var color = _offScreenCamera.GetComponent<Grayscale>();
            if (_data.value == 0)
            {
                _printColor = PrintDevice.GetPrinterColorConfig(true);
                color.enabled = false;
            }
            else if (_data.value == 1)
            {
                _printColor = PrintDevice.GetPrinterColorConfig(false);
                color.enabled = true;
            }
        }
    }

    /// <summary>
    /// カラーの設定
    /// モノクロならfalse,カラーならtrueにする
    /// </summary>
    public void ColorConfig()
    {
        if (_data.value == 0)
        {
            var _printColor = PrintDevice.GetPrinterColorConfig(true);
            Debug.Log("_printColor = " + _printColor);
            _offScreenCamera.GetComponent<Grayscale>().enabled = false;
        }
        else if (_data.value == 1)
        {
            _printColor = PrintDevice.GetPrinterColorConfig(false);
            Debug.Log("_printColor = " + _printColor);
            _offScreenCamera.GetComponent<Grayscale>().enabled = true;
        }
        else
        {
            throw new IndexOutOfRangeException("Out of Range");
        }
    }
}