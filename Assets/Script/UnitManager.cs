using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitManager : MonoBehaviour
{
    private DataBaseManager theDB;
    private SkillManager theSKILL;
    private EnemyManager theEnemy;

    public Image[] sprites;

    public GameObject[] go_HUD;

    public Image[] hpBars;
    public Image[] apBars1;
    public Image[] apBars2;

    public int selectedUnit;

    public Image selectedUnitHP;
    public Image selectedUnitAP;
    public Text selectedUnitName;
    public Text selectedUnitATK;
    public Text selectedUnitDEF;
    public Text selectedUnitHPAmount;
    public Text selectedUnitAPAmount;
    public Image[] selectedUnitItem;
    public Image selectedUnitFace;

    public Image[] select;

    public Image[] skillImages;
    public Text[] skillNames;
    public Text[] skillDess;
    public Text[] skillCosts;

    public Text[] guardInt;
    public GameObject[] guard;

    public GameObject[] character;

    public GameObject hud;
    public GameObject falsehud;
    public GameObject nextButton;
    public GameObject wait;
    public GameObject falseProfile;

    public Image[] unit0_Status;
    public Image[] unit1_Status;
    public Image[] unit2_Status;
    public Image[] unit3_Status;
    public Image[] unit4_Status;

    public Image[] statusImages;
    public Text[] statusStack;

    // Start is called before the first frame update
    void Start()
    {
        theDB = FindObjectOfType<DataBaseManager>();
        theSKILL = FindObjectOfType<SkillManager>();
        theEnemy = FindObjectOfType<EnemyManager>();
        DrawingUnit();
        StartMyturn();
        DrawingStatus();
    }

    public void DrawingUnit()
    {
        for(int i = 0; i < 5; i++)
        {
            if (theDB.myUnit[i].unitID == 0)
            {
                character[i].SetActive(false);
            }
            else
            {
                
                character[i].SetActive(true);
                GameObject chara;
                chara = (GameObject)Instantiate(theDB.myUnit[i].motionPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                chara.transform.parent = character[i].transform;
                chara.transform.localPosition = new Vector3(0, -135, 0);
            }
        }
    }

    public void DrawingMiniHUD()
    {
        for (int i = 0; i < 5; i++)
        {
            if (theDB.myUnit[i].unitID == 0)
            {
                go_HUD[i].SetActive(false);
            }
            else
            {
                go_HUD[i].SetActive(true);
                hpBars[i].fillAmount = (float)theDB.myUnit[i].unitHP / (float)theDB.myUnit[i].maxHP;
                if (theDB.myUnit[i].unitAP > 5)
                {
                    apBars1[i].fillAmount = 1;
                    float a = (float)theDB.myUnit[i].unitAP;
                    a -= 5;
                    apBars2[i].fillAmount = a * 2 / 10f;
                }
                else
                {
                    apBars1[i].fillAmount = (float)theDB.myUnit[i].unitAP * 2f / 10f; ;
                    apBars2[i].fillAmount = 0;
                }
            }
        }
    }

    public void StartMyturn()
    {
        int a = 0;
        for (int i = 0; i < 5; i++)
        {
            if (theDB.myUnit[i].unitID != 0)
            {
                falseProfile.SetActive(false);
                wait.SetActive(false);
                hud.SetActive(true);
                falsehud.SetActive(false);
                nextButton.SetActive(true);
                selectedUnit = i;
                DrawingSkill();
                DrawingSelect();
                break;
            }
            else
            {
                a += 1;
            }
        }
        if (a >= 5)
        {
            Debug.Log("게임오버");
        }

    }

    public void Nextturn()
    {
        for (int i = selectedUnit + 1; i < 6; i++)
        {
            if (i == 5)
            {
                falseProfile.SetActive(true);
                wait.SetActive(true);
                hud.SetActive(false);
                falsehud.SetActive(true);
                nextButton.SetActive(false);
                ClearSkill();
                theEnemy.EnemyTurnStart();
                break;
            }
            if (theDB.myUnit[i].unitID != 0)
            {
                selectedUnit = i;
                DrawingSkill();
                DrawingSelect();
                break;
            }

            
        }

    }

    public void DrawingSkill()
    {
        for (int i = 0; i < 6; i++)
        {
            skillImages[i].sprite = theDB.skillList[theDB.myUnit[selectedUnit].unitSkill[i]].skillImage;
            skillNames[i].text = theDB.skillList[theDB.myUnit[selectedUnit].unitSkill[i]].skillName;
            skillDess[i].text = theSKILL.skillDes[theDB.myUnit[selectedUnit].unitSkill[i]];
            skillCosts[i].text = theDB.skillList[theDB.myUnit[selectedUnit].unitSkill[i]].skillCost.ToString();
        }
    }

    public void ClearSkill()
    {
        for (int i = 0; i < 6; i++)
        {
            skillImages[i].sprite = theDB.skillList[0].skillImage;
            skillNames[i].text = "";
            skillDess[i].text = "";
            skillCosts[i].text = "";
        }
    }

    public void DrawingHUD()
    {

        selectedUnitHP.fillAmount = (float)theDB.myUnit[selectedUnit].unitHP / (float)theDB.myUnit[selectedUnit].maxHP;
        selectedUnitAP.fillAmount = (float)theDB.myUnit[selectedUnit].unitAP / 10f;
        selectedUnitName.text = theDB.myUnit[selectedUnit].unitName;
        selectedUnitATK.text = theDB.myUnit[selectedUnit].currentATK.ToString();
        selectedUnitDEF.text = theDB.myUnit[selectedUnit].currentDEF.ToString();
        selectedUnitHPAmount.text = theDB.myUnit[selectedUnit].unitHP.ToString() + "/" + theDB.myUnit[selectedUnit].maxHP.ToString();
        selectedUnitAPAmount.text = theDB.myUnit[selectedUnit].unitAP.ToString() + "/10";
        selectedUnitFace.sprite = theDB.myUnit[selectedUnit].unitFace;
        selectedUnitItem[0].sprite = theDB.equipList[theDB.myUnit[selectedUnit].unitEquip[0]].equipImage;
        selectedUnitItem[1].sprite = theDB.equipList[theDB.myUnit[selectedUnit].unitEquip[1]].equipImage;
        selectedUnitItem[2].sprite = theDB.equipList[theDB.myUnit[selectedUnit].unitEquip[2]].equipImage;
    }

    public void DrawingSelect()
    {

        for (int i = 0; i < 5; i++)
        {
            Color color2 = select[i].color;
            color2.a = 0;
            select[i].color = color2;
        }

        Color color = select[selectedUnit].color;
        color.a = 1;
        select[selectedUnit].color = color;
    }

    public void DrawingGuard()
    {
        for (int i = 0; i < 5; i++)
        {
            if(theDB.myUnit[i].unitID == 0 || theDB.myUnit[i].guard == 0)
            {
                guard[i].SetActive(false);
            }
            else
            {
                guard[i].SetActive(true);
                guardInt[i].text = theDB.myUnit[i].guard.ToString();
            }
        }
    }

    public void DrawingStatus()
    {
        for (int i = 0; i < 9; i++)
        {
            if (theDB.myUnit[0].unitStatus.Count < i + 1)
            {
                Color color = unit0_Status[i].color;
                color.a = 0;
                unit0_Status[i].color = color;
            }
            else
            {
                unit0_Status[i].sprite = theDB.myUnit[0].unitStatus[i].statusImage;

                Color color = unit0_Status[0].color;
                color.a = 1;
                unit0_Status[i].color = color;
            }
        }

        for (int i = 0; i < 9; i++)
        {
            if (theDB.myUnit[1].unitStatus.Count < i + 1)
            {
                Color color = unit1_Status[i].color;
                color.a = 0;
                unit1_Status[i].color = color;
            }
            else
            {
                unit1_Status[i].sprite = theDB.myUnit[1].unitStatus[i].statusImage;

                Color color = unit1_Status[0].color;
                color.a = 1;
                unit1_Status[i].color = color;
            }
        }

        for (int i = 0; i < 9; i++)
        {
            if (theDB.myUnit[2].unitStatus.Count < i + 1)
            {
                Color color = unit2_Status[i].color;
                color.a = 0;
                unit2_Status[i].color = color;
            }
            else
            {
                unit2_Status[i].sprite = theDB.myUnit[2].unitStatus[i].statusImage;

                Color color = unit2_Status[0].color;
                color.a = 1;
                unit2_Status[i].color = color;
            }
        }

        for (int i = 0; i < 9; i++)
        {
            if (theDB.myUnit[3].unitStatus.Count < i + 1)
            {
                Color color = unit3_Status[i].color;
                color.a = 0;
                unit3_Status[i].color = color;
            }
            else
            {
                unit3_Status[i].sprite = theDB.myUnit[3].unitStatus[i].statusImage;

                Color color = unit3_Status[0].color;
                color.a = 1;
                unit3_Status[i].color = color;
            }
        }

        for (int i = 0; i < 9; i++)
        {
            if (theDB.myUnit[4].unitStatus.Count < i + 1)
            {
                Color color = unit4_Status[i].color;
                color.a = 0;
                unit4_Status[i].color = color;
            }
            else
            {
                unit4_Status[i].sprite = theDB.myUnit[4].unitStatus[i].statusImage;

                Color color = unit4_Status[0].color;
                color.a = 1;
                unit4_Status[i].color = color;
            }
        }


        for (int i = 0; i < 10; i++)
        {
            if (i < theDB.myUnit[selectedUnit].unitStatus.Count)
            {
                Color color = statusImages[i].color;
                color.a = 1;
                statusImages[i].color = color;

                statusImages[i].sprite = theDB.myUnit[selectedUnit].unitStatus[i].statusImage;
                statusStack[i].text = theDB.myUnit[selectedUnit].statusStack[i].ToString();
            }
            else
            {
                Color color = statusImages[i].color;
                color.a = 0;
                statusImages[i].color = color;
                statusStack[i].text = "";
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        DrawingGuard();
        DrawingMiniHUD();
        DrawingHUD();
        DrawingSkill();
    }

}
