using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{

    public List<Enemy> enemyList = new List<Enemy>();

    public SpriteRenderer[] sprites;
    public Character[] enemyChara;
    public GameObject[] enemy_go;

    public Sprite invisible;

    public GameObject[] hud;
    public Image[] hpBar;

    public GameObject[] guard;
    public Text[] guardInt;

    public UnitManager theUNIT;

    public int turnEnemy;

    // Start is called before the first frame update
    void Start()
    {
        theUNIT = FindObjectOfType<UnitManager>();
        DrawingEnemy();
    }

    public void DrawingEnemy()
    {
        for (int i = 0; i < 5; i++)
        {
            if (enemyList[i].Null == false)
            {
                enemy_go[i].SetActive(true);
            }
            else
            {
                enemy_go[i].SetActive(false);
            }
        }
    }

    public void DrawingHUD()
    {
        for (int i = 0; i < 5; i++)
        {
            if (enemyList[i].Null == false)
            {
                hpBar[i].fillAmount = (float)enemyList[i].hP / (float)enemyList[i].maxHP;
            }
            else
            {
                hud[i].SetActive(false);
            }
        }
    }

    public void DrawingGuard()
    {
        for (int i = 0; i < 5; i++)
        {
            if (enemyList[i].Null == true || enemyList[i].guard == 0)
            {
                guard[i].SetActive(false);
            }
            else
            {
                guard[i].SetActive(true);
                guardInt[i].text = enemyList[i].guard.ToString();
            }
        }
    }

    public void EnemyTurnStart()
    {
        for (int i = 1; i < 7; i++)
        {
            if (i == 6)
            {
                Debug.Log("½Â¸®");
                break;
            }
            if (enemyList[i - 1].Null == false)
            {
                turnEnemy = i;
                break;
            }
        }
    }

    public void EnemyTurnNext()
    {
        for (int i = turnEnemy + 1; i < 7; i++)
        {
            if (i == 6)
            {
                turnEnemy = 0;
                theUNIT.StartMyturn();
                break;
            }
            if (enemyList[i - 1].Null == false)
            {
                turnEnemy = i;
                break;
            }
        }
    }



    // Update is called once per frame
    void Update()
    {
        DrawingHUD();
        DrawingGuard();
    }
}
