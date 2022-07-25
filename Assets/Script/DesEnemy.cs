using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DesEnemy : MonoBehaviour
{
    public int unitNo;

    public GameObject desPanel;
    public Text desName;
    public Text des;

    private DataBaseManager theDB;
    private EnemyManager theENEMY;

    // Start is called before the first frame update
    void Start()
    {
        theDB = FindObjectOfType<DataBaseManager>();
        theENEMY = FindObjectOfType<EnemyManager>();
    }

    public void DrawDes()
    {

        if (theENEMY.enemyList[unitNo].Null == true)
        {
            desPanel.SetActive(false);
            desName.text = "";
            des.text = "";

        }
        else
        {
            desPanel.SetActive(true);
            desName.text = theENEMY.enemyList[unitNo].name;
            des.text = "HP : " + theENEMY.enemyList[unitNo].hP + "/" + theENEMY.enemyList[unitNo].maxHP + "\nATK : " + theENEMY.enemyList[unitNo].currentATK + "\nDEF : " + theENEMY.enemyList[unitNo].currentDEF;

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
