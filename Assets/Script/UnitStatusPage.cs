using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStatusPage : MonoBehaviour
{

    public GameObject[] firstPage;
    public GameObject[] secondPage;
    public GameObject[] thirdPage;

    private DataBaseManager theDB;

    // Start is called before the first frame update
    void Start()
    {
        theDB = FindObjectOfType<DataBaseManager>();
        StartCoroutine(NextPageCoroutine());
    }

    IEnumerator NextPageCoroutine()
    {
        while (true)
        {
            for (int i = 0; i < 5; i++)
            {
                firstPage[i].SetActive(true);
                secondPage[i].SetActive(false);
                thirdPage[i].SetActive(false);
            }

            yield return new WaitForSeconds(5f);

            for (int i = 0; i < 5; i++)
            {
                if (theDB.myUnit[i].unitStatus.Count > 3)
                {
                    firstPage[i].SetActive(false);
                    secondPage[i].SetActive(true);
                    thirdPage[i].SetActive(false);
                }

            }

            yield return new WaitForSeconds(5f);

            for (int i = 0; i < 5; i++)
            {
                if (theDB.myUnit[i].unitStatus.Count > 6)
                {
                    firstPage[i].SetActive(false);
                    secondPage[i].SetActive(false);
                    thirdPage[i].SetActive(true);
                }

            }

            yield return new WaitForSeconds(5f);
        }
    }

    // Update is called once per frame
    
}
