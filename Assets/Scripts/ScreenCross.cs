using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenCross : MonoBehaviour
{

    private static Vector3 jumpBack = new Vector3(-19f, 0);
    private static Vector3 forward = new Vector3(1f, 0);

    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition.x > 9.5f) {
            transform.localPosition += jumpBack;
        } else {
            transform.localPosition += forward * Time.deltaTime;
        }
    }
}
