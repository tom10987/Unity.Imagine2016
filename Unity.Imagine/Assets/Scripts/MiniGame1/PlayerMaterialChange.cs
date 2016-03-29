using UnityEngine;
using System.Collections.Generic;

public class PlayerMaterialChange : MonoBehaviour
{

    [SerializeField]
    Material[] _material;

    List<ARModel> _playerList = new List<ARModel>();

    [SerializeField]
    ARDeviceManager _arDevMgr = null;

    void Start()
    {
        _playerList.Add(_arDevMgr.player1);
        _playerList.Add(_arDevMgr.player2);
        bool isPlayer = (_arDevMgr.player1 == null || _arDevMgr.player2 == null);

        //Debug.Log(_playerList.Count);
        // _material = new Material[4];
        if (_arDevMgr == null)
        {
            _arDevMgr = FindObjectOfType<ARDeviceManager>();
        }
        //_material[0] = Resources.Load<Material>("Resources/MiniGame/Game1/Material/Clip");
        //_material[1] = Resources.Load<Material>("Resources/MiniGame/Game1/Material/RedCannonMaterial");
        //_material[2] = Resources.Load<Material>("Resources/MiniGame/Game1/Material/BlueClipMaterial");
        //_material[3] = Resources.Load<Material>("Resources/MiniGame/Game1/Material/BlueCannonMaterial");

    }

    void Update()
    {
        MaterialChange();
    }

    public void MaterialChange()
    {
        bool isPlayer = (_arDevMgr.player1 == null || _arDevMgr.player2 == null);
        if (isPlayer)
        {
            _playerList.Clear();
            _playerList.Add(_arDevMgr.player1);
            _playerList.Add(_arDevMgr.player2);          
            return;
        }



        if (gameObject.transform.parent.gameObject == _playerList[0].gameObject)
        {
            gameObject.GetComponent<Renderer>().material = _material[0];
            gameObject.transform.parent.gameObject.GetComponent<Renderer>().material = _material[1];
        }
        else
    if (gameObject.transform.parent.gameObject == _playerList[1].gameObject)
        {
            gameObject.GetComponent<Renderer>().material = _material[2];
            gameObject.transform.parent.gameObject.GetComponent<Renderer>().material = _material[3];
        }
    }

}
