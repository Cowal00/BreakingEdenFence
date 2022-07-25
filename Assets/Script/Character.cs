using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public SpriteRenderer[] spriteList;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void Attacked()
    {
        StartCoroutine(AttackedCoroutine());
    }

    IEnumerator AttackedCoroutine()
    {
        spriteList = this.transform.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sprites in spriteList)
        {
            sprites.color = Color.red;
        }

        yield return new WaitForSeconds(0.1f);

        foreach (SpriteRenderer sprites in spriteList)
        {
            sprites.color = Color.white;
        }

        yield return new WaitForSeconds(0.1f);

        foreach (SpriteRenderer sprites in spriteList)
        {
            sprites.color = Color.red;
        }

        yield return new WaitForSeconds(0.1f);

        foreach (SpriteRenderer sprites in spriteList)
        {
            sprites.color = Color.white;
        }


    }

    public void Attack()
    {
        StartCoroutine(AttackCoroutine());
    }

    IEnumerator AttackCoroutine()
    {
        spriteList = this.transform.GetComponentsInChildren<SpriteRenderer>();

        for (int i = 0; i < spriteList.Length; i++)
        {
            Color color = spriteList[i].color;
            color.a = 0f;
            spriteList[i].color = color;

        }

        yield return new WaitForSeconds(0.4f);

        Color color2 = spriteList[spriteList.Length - 1].color;

        while (color2.a < 1)
        {
            for (int i = 0; i < spriteList.Length; i++)
            {
                Color color = spriteList[i].color;
                color.a += 0.03f;
                spriteList[i].color = color;

                color2 = spriteList[spriteList.Length - 1].color;

            }
            yield return new WaitForSeconds(0.01f);
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
