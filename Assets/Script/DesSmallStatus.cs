using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DesSmallStatus : MonoBehaviour
{
    public int unitNo;
    public int statusNo;

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

        if (theDB.myUnit[unitNo].unitStatus.Count < statusNo + 1)
        {
            desPanel.SetActive(false);
            desName.text = "";
            des.text = "";

        }
        else
        {
            desPanel.SetActive(true);
            desName.text = theDB.myUnit[unitNo].unitStatus[statusNo].statusName;
            des.text = theDB.myUnit[unitNo].unitStatus[statusNo].statusDes + "\nStack : " + theDB.myUnit[unitNo].statusStack[statusNo];

        }

        if (unitNo == 0)
        {
            Vector3 mousePos;
            mousePos = new Vector3(Input.mousePosition.x + 180, Input.mousePosition.y + desPanel.GetComponent<RectTransform>().sizeDelta.y / 2 + 20, 0);
            desPanel.GetComponent<RectTransform>().anchoredPosition = mousePos;
        }
        else
        {
            Vector3 mousePos;
            mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y + desPanel.GetComponent<RectTransform>().sizeDelta.y / 2 + 20, 0);
            desPanel.GetComponent<RectTransform>().anchoredPosition = mousePos;
        }

    }

    public void DrawBigDes()
    {
        unitNo = theUNIT.selectedUnit;
        if (theDB.myUnit[unitNo].unitStatus.Count < statusNo + 1)
        {
            desPanel.SetActive(false);
            desName.text = "";
            des.text = "";

        }
        else
        {
            desPanel.SetActive(true);
            desName.text = theDB.myUnit[unitNo].unitStatus[statusNo].statusName;
            des.text = theDB.myUnit[unitNo].unitStatus[statusNo].statusDes + "\nStack : " + theDB.myUnit[unitNo].statusStack[statusNo];

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
