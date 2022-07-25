using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy
{
    public string name;
    public int hP;
    public int maxHP;
    public int aTK;
    public int currentATK;
    public int dEF;
    public int currentDEF;
    public bool Null;
    public Sprite sprite;

    public int guard;

    public List<Status> statusList = new List<Status>();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
