using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalPass : MonoBehaviour
{
    private PlatformEffector2D effector;        // enables object
    public float waitTime;                      // time to pass platform

    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // pass platform conditions
        if (Input.GetKeyUp(KeyCode.S))
        {
            waitTime = 0.1f;
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (waitTime <= 0)
            {
                effector.rotationalOffset = 180f;
                waitTime = 0.5f;
            }

            else
            {
                waitTime -= Time.deltaTime;
            }
        }

        if (Input.GetKey(KeyCode.Space))
        {
            effector.rotationalOffset = 0;
        }
    }
}
