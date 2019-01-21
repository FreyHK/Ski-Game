using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAnimatorState : MonoBehaviour
{
    public string StateName = "Hidden";

    void Awake()
    {
        GetComponent<Animator>().Play(StateName, 0, 1f);
    }
}
