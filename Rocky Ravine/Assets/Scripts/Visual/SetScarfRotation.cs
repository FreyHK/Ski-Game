using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetScarfRotation : MonoBehaviour
{

    //Degrees
    float offsetRange = 3.5f;

    void Update()
    {
        //Calculate offset
        float offset = Mathf.Sin(Time.timeSinceLevelLoad * 15f) * offsetRange;

        //Apply offset
        float z = transform.parent.rotation.eulerAngles.z + offset;

        transform.rotation = Quaternion.Euler(0f, 0f, z);
    }
}
