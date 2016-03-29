using UnityEngine;
using UnityEngine.SceneManagement;
using Game.Utility;

public class ConfigPanel : MonoBehaviour {

    const string LOAD_SCENE_NAME = "Menu";

    [SerializeField]
    private GameObject _notPrinterConfigPanel = null;
    [SerializeField]
    private GameObject _confirmationPanel = null;
    [SerializeField]
    private GameObject _captureCanvas = null;
    [SerializeField]
    private GameObject _soundController = null;
    
    public void ClickButton()
    {
        _notPrinterConfigPanel.SetActive(false);
    }

    public void Return()
    {
        _soundController.GetComponent<PrintSceneSoundController>().isPlaySE = 7;
        var screenSequencer = ScreenSequencer.instance;
        if (screenSequencer.isEffectPlaying) return;

        screenSequencer.SequenceStart
            (
                () => { GameScene.Menu.ChangeScene(); },
                new Fade(1.0f)
            );
    }

    public void Complete()
    {
        _soundController.GetComponent<PrintSceneSoundController>().isPlaySE = 8;
        var screenSequencer = ScreenSequencer.instance;
        if (screenSequencer.isEffectPlaying) return;

        screenSequencer.SequenceStart
            (
                () => { GameScene.Menu.ChangeScene(); },
                new Fade(1.0f)
            );
    }


    public void ClickNoButton()
    {
        _confirmationPanel.SetActive(false);
        _captureCanvas.SetActive(true);
        //クリエイトの曲に戻す
        _soundController.GetComponent<PrintSceneSoundController>().isPlayBGM = 1;
        _soundController.GetComponent<PrintSceneSoundController>().isPlaySE = 6;
    }

    public void SelectSE()
    {
        _soundController.GetComponent<PrintSceneSoundController>().isPlaySE = 6;
    }

}
