using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DesUnit : MonoBehaviour
{
    public int unitNo;

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

        if (theDB.myUnit[unitNo].unitID == 0)
        {
            desPanel.SetActive(false);
            desName.text = "";
            des.text = "";

        }
        else
        {
            desPanel.SetActive(true);
            desName.text = theDB.myUnit[unitNo].unitName;
            des.text = "HP : " + theDB.myUnit[unitNo].unitHP + "/" + theDB.myUnit[unitNo].maxHP + "\nAP : " + theDB.myUnit[unitNo].unitAP + "/10 \nATK : " + theDB.myUnit[unitNo].currentATK + "\nDEF : " + theDB.myUnit[unitNo].currentDEF;

        }

        if (unitNo == 0)
        {
            Vector3 mousePos;
            mousePos = new Vector3(Input.mousePosition.x + 140, Input.mousePosition.y + desPanel.GetComponent<RectTransform>().sizeDelta.y / 2 + 20, 0);
            desPanel.GetComponent<RectTransform>().anchoredPosition = mousePos;
        }
        else
        {
            Vector3 mousePos;
            mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y + desPanel.GetComponent<RectTransform>().sizeDelta.y / 2 + 20, 0);
            desPanel.GetComponent<RectTransform>().anchoredPosition = mousePos;
        }
        
    }

    public void ClearDes()
    {

        desPanel.SetActive(false);
    }
}
