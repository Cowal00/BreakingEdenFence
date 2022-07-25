using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float ShakeAmount;
    float ShakeTime;
    Vector3 initialPosition;
    public void VibrateForTime(float time)
    {
        ShakeTime = time;
        StartCoroutine(ShakeCoroutine());
    }

    IEnumerator ShakeCoroutine()
    {
        initialPosition = GameObject.FindWithTag("MainCamera").transform.position;
        while (ShakeTime > 0)
        {
            transform.position = Random.insideUnitSphere * ShakeAmount + initialPosition;
            yield return new WaitForSeconds(0.001f);
            ShakeTime -= Time.deltaTime;
        }
        ShakeTime = 0.0f;
        transform.position = initialPosition;
    }


    private void Update()
    {
        
    }
}
