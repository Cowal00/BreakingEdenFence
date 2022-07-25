using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour { 

    public int enemyNo;


    private EnemyManager theENEMY;
    private SkillManager theSKILL;

    public SpriteRenderer thisMotion;
    public SpriteRenderer attackSprite;

    public Sprite attackMotion;

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);

    private WaitForSeconds waitLong = new WaitForSeconds(3f);

    // Start is called before the first frame update
    void Start()
    {
        theENEMY = FindObjectOfType<EnemyManager>();
        theSKILL = FindObjectOfType<SkillManager>();
        StartCoroutine(TurnWaitCoroutine());
    }


    public void Pattern1()
    {
        Debug.Log("АјАн");
        theSKILL.Attacked(theENEMY.enemyList[enemyNo - 1].aTK, 3);
        StartCoroutine(AttackMotionCoroutine());
        StartCoroutine(TurnEndCoroutine());
    }

    IEnumerator TurnEndCoroutine()
    {
        yield return waitLong;
        theENEMY.EnemyTurnNext();
        StartCoroutine(TurnWaitCoroutine());
    }

    IEnumerator AttackMotionCoroutine()
    {
        theENEMY.enemyChara[enemyNo - 1].Attack();

        attackSprite.sprite = attackMotion;

        Color color2 = attackSprite.color;
        color2.a = 1;
        attackSprite.color = color2;
        yield return new WaitForSeconds(0.4f);
        while (color2.a > 0)
        {

            color2.a -= 0.03f;
            attackSprite.color = color2;

            yield return waitTime;
        }


    }
    IEnumerator TurnWaitCoroutine()
    {
        yield return new WaitUntil(() => theENEMY.turnEnemy == enemyNo);
        Pattern1();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
