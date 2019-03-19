using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using EZCameraShake;

public class PlayerCollision : MonoBehaviour
{
    public static Action OnHitObstacle;

    bool hasBeenHit = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Obstacle" && !hasBeenHit)
        {
            hasBeenHit = true;

            AudioManager.Instance.Play("Hit");

            CameraShaker.Instance.ShakeOnce(10f, 10f, .1f, .1f);

            //Debug.Log("Player hit: " + collision.collider.name);
            if (OnHitObstacle != null)
                OnHitObstacle();
        }
    }
}
