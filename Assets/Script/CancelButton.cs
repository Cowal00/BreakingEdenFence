using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelButton : MonoBehaviour
{
    private SkillManager theSKILL;

    // Start is called before the first frame update
    void Start()
    {
        theSKILL = FindObjectOfType<SkillManager>();
    }

    public void Canceling()
    {
        theSKILL.Canceling();
    }

}
