
//------------------------------------------------------------
// TIPS:
// AudioPlayer.Play() で使用できる AudioClip のインデックス一覧です
//
// 対応関係、および再生タイミングは
// Assets/Resources/Audio/Data/readme.txt を参照してください
//
//------------------------------------------------------------

public enum ClipIndex {
  bgm_No01_TitleAndMenu,
  bgm_No02_CreateAndPrint,
  bgm_No03_Printing,
  bgm_No04_MiniGame,

  se_No05_CharacterRotation,
  se_No06_ChangeDesign,

  se_No07_Next,
  se_No08_Back,
  se_No09_OK,

  se_No10_TitleLogoSetup,
  se_No11_CharacterFallDown,
  se_No12_CharacterLanding,
  se_No13_ExpandBox,

  se_No14_Jump,

  se_No15_StartCountDown,
  se_No16_FinishCountDown,

  /// <summary> <see cref="se_No16_FinishCountDown"/> に含まれるため未使用 </summary>
  se_No17_Finish,

  se_No18_CostumeBreak_A,
  se_No19_CostumeBreak_B,
  se_No20_Result,

  se_No21_Shot,
  se_No22_Hit,

  se_No23_JustCharge,
  se_No24_MissCharge,
  se_No25_PowerCharge,
  se_No26_BeamFire,
  se_No27_BeamClash,

  se_No28_JustHit,
  se_No29_MissHit,
  se_No30_ShieldBreak,
  se_No31_Floating,

  se_No32_RandomSelect,
  se_No33_RandomFinish,

  Max, None = -1,
}

public static class ClipIndexExtension {

  /// <summary> 指定した ID の <see cref="AudioClip"/> を使って再生する </summary>
  /// <param name="volume"> 音量を指定 (0.0 ~ 1.0) </param>
  /// <param name="isLoop"> true = ループ再生を許可 </param>
  public static void Play(this AudioPlayer player,
                          ClipIndex index, float volume, bool isLoop) {
    player.Play(index.ToInt(), volume, isLoop);
  }

  public static void Play(this AudioPlayer player,
                          ClipIndex index, float volume) {
    player.Play(index.ToInt(), volume, false);
  }

  public static void Play(this AudioPlayer player,
                          ClipIndex index, bool isLoop) {
    player.Play(index.ToInt(), 1f, isLoop);
  }

  public static void Play(this AudioPlayer player, ClipIndex index) {
    player.Play(index.ToInt(), 1f, false);
  }

  public static int ToInt(this ClipIndex index) { return (int)index; }
}
