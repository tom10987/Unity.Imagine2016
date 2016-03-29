using UnityEngine;
using System.Collections;

public class ParameterBar : MonoBehaviour
{
    [SerializeField]
    RectTransform _speedGauge = null;

    [SerializeField]
    RectTransform _attackGauge = null;

    [SerializeField]
    RectTransform _defenceGauge = null;

    CharacterParameterInfo _characterParameterInfo = null;

    void Start()
    {
        _characterParameterInfo = FindObjectOfType<CharacterParameterInfo>();
        ChangeParameterGauge();
    }

    public ParameterBar ChangeParameterGauge()
    {
        StartCoroutine(ChangeParameterGaugeCorutine());
        return this;
    }

    IEnumerator ChangeParameterGaugeCorutine()
    {
        yield return new WaitForSeconds(0.1f);

        var characterParameter = _characterParameterInfo.getCharacterParameter;
        _speedGauge.localScale = new Vector3(characterParameter.speed * 0.2f, 1, 1);
        _attackGauge.localScale = new Vector3(characterParameter.attack * 0.2f, 1, 1);
        _defenceGauge.localScale = new Vector3(characterParameter.defense * 0.2f, 1, 1);

        yield return null;
    }
}