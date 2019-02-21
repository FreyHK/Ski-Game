using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerArea : MonoBehaviour
{
    public string ActivatorTag = "Player";
    public UnityEvent OnTrigger;
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(ActivatorTag)) {
            OnTrigger.Invoke();
        }
    }
}
