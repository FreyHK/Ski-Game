using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOptionsPanel : MonoBehaviour
{
    public Animator anim;

    bool isOpen = false;

    float cooldown;

    private void Update() {
        if (cooldown > 0)
            cooldown -= Time.deltaTime;
    }

    public void Toggle() {
        //Make sure player can't spam this
        if (cooldown > 0)
            return;

        isOpen = !isOpen;
        anim.SetBool("IsOpen", isOpen);
        //Set cooldown
        cooldown = 1f;
    }
}
