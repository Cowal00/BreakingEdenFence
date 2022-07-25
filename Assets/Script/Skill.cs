using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skill
{
    // Start is called before the first frame update
    
    public int skillID;
    public int skillCost;
    public string skillName;
    public string skillDes;
    public Sprite skillImage;

    public SkillType skillType;

    public enum SkillType{
        attackOne,
        attackAll,
        healOne,
        healAll,
        auto
    }



    void Start()
    {
        
    }

}
