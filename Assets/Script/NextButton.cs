using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextButton : MonoBehaviour
{
    public UnitManager theUNIT;


    // Start is called before the first frame update
    void Start()
    {
        theUNIT = FindObjectOfType<UnitManager>();
       
    }

    public void Next()
    {
        theUNIT.Nextturn();
    }

}
