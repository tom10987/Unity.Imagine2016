using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Game.Utility;
using System.IO;

public class PrintStart : MonoBehaviour {

    //[SerializeField, Tooltip("画像の名前")]
    //private string _textureName;

    //[SerializeField]
    //private int width;
    //[SerializeField]
    //private int height;
    //[SerializeField]
    //private int x;
    //[SerializeField]
    //private int y;

    [SerializeField]
    private GameObject _notPrinterConfigPanel = null;
    [SerializeField]
    private GameObject _confirmationPanel = null;
    [SerializeField]
    private GameObject _captureCanvas = null;
    [SerializeField]
    private GameObject _soundController = null;

    //参考サイト：http://www.insatsuyasan.com/data/datasize_tool.html
    const int PrintSize = 620;

    
    void Start()
    {
    }

    /// <summary>
    /// ボタンを押したらプリントスタート
    /// </summary>
    public void PrintingStart()
    {
        if(!PrintDevice.isValid)
        {
            _notPrinterConfigPanel.SetActive(true);
        }
        else
        {
            StartCoroutine(ScreenShot());
            _confirmationPanel.SetActive(true);
            _captureCanvas.SetActive(false);
            _soundController.GetComponent<PrintSceneSoundController>().isPlayBGM = 2;
            _soundController.GetComponent<PrintSceneSoundController>().isPlaySE = 8;
        }
    }

    private IEnumerator ScreenShot()
    {
        yield return new WaitForEndOfFrame();

        //float x2 = (Screen.width / 1920.0f) * x;
        //float y2 = (Screen.height / 1080.0f) * y;
        //float w = (Screen.width / 1920.0f) * width;
        //float h = (Screen.height / 1080.0f) * height;

        //Texture2D tex = new Texture2D((int)w, (int)h, TextureFormat.RGB24, false);
        //tex.ReadPixels(new Rect(x2, y2, w, h), 0, 0);
        //tex.Apply();

        RenderTexture renderTexture = Resources.Load<RenderTexture>("PrinterScene/PrintTexture");
        RenderTexture.active = renderTexture;

        Texture2D tex = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
        tex.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        tex.Apply();

        var pngData = tex.EncodeToPNG();
        var screenShotPath = GetScreenShotPath();
        File.WriteAllBytes(screenShotPath, pngData);

        var printer = GameObject.Find("Printer").GetComponent<Dropdown>();
        Debug.Log(printer.value);

        var color = GameObject.Find("Color").GetComponent<Dropdown>();
        Debug.Log(color.value);

        //var path = Application.dataPath + "/Resources/" + _textureName + ".png";
        //Debug.Log(path);
        Debug.Log(screenShotPath);
        Debug.Log(printer.options[printer.value].text);

        PrintDevice.PrintRequest(screenShotPath,
            PrintDevice.DrawSize.one* PrintSize,
            printer.options[printer.value].text,
            PrinterConfig._printColor
            );
    }

    private string GetScreenShotPath()
    {
        string path = "";
        path = Application.dataPath + "/Craft.png";
        return path;
    }

}
