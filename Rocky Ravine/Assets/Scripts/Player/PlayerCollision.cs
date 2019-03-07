using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using EZCameraShake;

public class PlayerCollision : MonoBehaviour
{
    //Audio
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip hitSound;

    public static Action OnHitObstacle;

    bool hasBeenHit = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Obstacle" && !hasBeenHit)
        {
            hasBeenHit = true;

            if (audioSource != null)
                audioSource.PlayOneShot(hitSound, .15f);

            CameraShaker.Instance.ShakeOnce(10f, 10f, .1f, .1f);

            //Debug.Log("Player hit: " + collision.collider.name);
            if (OnHitObstacle != null)
                OnHitObstacle();
        }
    }
}
