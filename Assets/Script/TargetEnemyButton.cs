using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetEnemyButton : MonoBehaviour
{
    // Start is called before the first frame update
    public int buttonNo;

    private SkillManager theSKILL;

    // Start is called before the first frame update
    void Start()
    {
        theSKILL = FindObjectOfType<SkillManager>();
    }

    public void Select()
    {
        theSKILL.targetEnemy = buttonNo;
        theSKILL.ClearEnemyButtons();
    }
}
