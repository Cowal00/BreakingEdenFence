using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetUnitButton : MonoBehaviour
{
    public int buttonNo;

    private SkillManager theSKILL;

    // Start is called before the first frame update
    void Start()
    {
        theSKILL = FindObjectOfType<SkillManager>();
    }

    public void Select()
    {
        theSKILL.targetUnit = buttonNo;
        theSKILL.ClearButtons();
    }


    // Update is called once per frame

}
