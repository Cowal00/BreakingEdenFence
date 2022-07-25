using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DesEquip : MonoBehaviour
{
    public int EquipNo;

    public GameObject desPanel;
    public Text desName;
    public Text des;

    private DataBaseManager theDB;
    private UnitManager theUNIT;

    // Start is called before the first frame update
    void Start()
    {
        theDB = FindObjectOfType<DataBaseManager>();
        theUNIT = FindObjectOfType<UnitManager>();
    }

    public void DrawDes()
    {

        if (theDB.myUnit[theUNIT.selectedUnit].unitEquip[EquipNo] == 0)
        {
            desPanel.SetActive(false);
            desName.text = "";
            des.text = "";

        }
        else
        {
            desPanel.SetActive(true);
            desName.text = theDB.equipList[theDB.myUnit[theUNIT.selectedUnit].unitEquip[EquipNo]].equipName;
            des.text = theDB.equipList[theDB.myUnit[theUNIT.selectedUnit].unitEquip[EquipNo]].equipDes;

        }

        Vector3 mousePos;
        mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y + desPanel.GetComponent<RectTransform>().sizeDelta.y / 2 + 20, 0);
        desPanel.GetComponent<RectTransform>().anchoredPosition = mousePos;
    }

    public void ClearDes()
    {

        desPanel.SetActive(false);
    }

}
