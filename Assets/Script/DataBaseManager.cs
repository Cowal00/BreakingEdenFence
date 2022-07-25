using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBaseManager : MonoBehaviour
{

    static public DataBaseManager instance;

    public List<Unit> unitList = new List<Unit>();
    public List<Unit> myUnit = new List<Unit>();
    public List<Equip> equipList = new List<Equip>();
    public List<Skill> skillList = new List<Skill>();
    public List<Status> statusList = new List<Status>();

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }

        myUnit[0] = unitList[1];

        myUnit[1] = unitList[2];
        myUnit[2] = unitList[3];
        myUnit[3] = unitList[4];
    }


    // Start is called before the first frame update
    void Start()
    {

    }


}
