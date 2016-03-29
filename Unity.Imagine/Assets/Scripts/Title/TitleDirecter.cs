using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleDirecter : MonoBehaviour
{

    [SerializeField]
    AudioPlayer _player = null;

    public void PushStartButton()
    {
        _player.Stop();
        _player.Play(6, 1.0f, false);
        //StartButtonが押されたら
        var screenSequencer = ScreenSequencer.instance;

        screenSequencer.SequenceStart
            (
                () => { GameScene.Menu.ChangeScene(); },
                new Fade(1.0f)
            );
    }
}
