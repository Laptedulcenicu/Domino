using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public GameObject ButtonRestart;
    public float waitTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (waitTime <= 0)
        {
            ButtonRestart.SetActive ( true );
        }
        waitTime -= Time.deltaTime;
    }
}
