using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillButton : MonoBehaviour
{
    public int buttonNo;

    public int skillNo;

    private SkillManager theSKILL;
    private DataBaseManager theDB;
    private UnitManager theUNIT;

    // Start is called before the first frame update
    void Start()
    {
        theSKILL = FindObjectOfType<SkillManager>();
        theDB = FindObjectOfType<DataBaseManager>();
        theUNIT = FindObjectOfType<UnitManager>();
    }

    public void UsingSkill()
    {
        theSKILL.Using(skillNo);
    }

    // Update is called once per frame
    void Update()
    {
        skillNo = theDB.myUnit[theUNIT.selectedUnit].unitSkill[buttonNo];
    }
}
