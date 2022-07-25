using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Unit
{

    // Start is called before the first frame update

    public string unitName;
    public string unitDes;
    public int unitID;
    public Sprite unitFace;
    public Sprite unittSprite;
    public Sprite unitAttack;
    public int[] unitSkill; 

    public int unitHP;
    public int maxHP;
    public int unitAP;
    public int maxAP;
    public int unitLevel;
    public int exp;
    public int Maxexp;
    public int unitATK;
    public int unitDEF;
    public int currentATK;
    public int currentDEF;

    public int[] unitEquip;
    public List<Status> unitStatus = new List<Status>();
    public int[] statusStack;

    public GameObject motionPrefab;

    public int guard;

    public string attackSound;

    void Start()
    {
        
    }

}
