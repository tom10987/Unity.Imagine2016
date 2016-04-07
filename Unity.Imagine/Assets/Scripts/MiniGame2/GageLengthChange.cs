using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class GageLengthChange : MonoBehaviour {

    public float Parameter { get; set; }

    [SerializeField]
    Gage _gage;

    [SerializeField]
    float _lengthChange = 1;

    

	void Start ()
    {
	
	}
	
	void Update ()
    {
	
	}

   public void StatusGageLengthChange()
    {
        float changeGageLength = Parameter * _lengthChange;
        Vector2 size = new Vector2(changeGageLength,0);
        Image[] rangeGage = _gage.getRangeGageImage;
        foreach(var gage in rangeGage)
        {
            Vector3 GagePosition = gage.rectTransform.anchoredPosition;
            GagePosition.x -= size.x / 2;
            gage.rectTransform.anchoredPosition = GagePosition;
            gage.rectTransform.sizeDelta += size;
        }
        
    }

}
