using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

    Rigidbody2D body;
    Animator anim;

    bool isJumping = false;
    bool isGrounded = false;

    public LayerMask groundMask;
    float groundCheckDistance = .7f;

    //Effects
    public ParticleSystem trailParticles;
    public TrailRenderer trackTrailR;
    public TrailRenderer trackTrailL;
    
    void Start () {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        trailParticles.Stop();
        trackTrailR.emitting = false;
        trackTrailL.emitting = false;
    }

    float speed = 6f;
    float jumpForce = 12f;
    
    void Update () {
        DoGroundCheck();
        if (anim != null)
            anim.SetBool("IsGrounded", isGrounded);

        //Clamp movement
        if (isGrounded && !isJumping) {
            body.velocity = transform.right * speed;
            //new Vector2(speed, body.velocity.y);
        }

        //Jump
        if (isGrounded && !isJumping && Input.GetMouseButtonDown(0)) {
            isJumping = true;
            body.AddForce((Vector2)transform.up * jumpForce, ForceMode2D.Impulse);

            if (anim != null)
                anim.SetTrigger("Jump");
        }
    }


    void DoGroundCheck() {
        RaycastHit2D hit = Physics2D.Raycast(body.position, -Vector2.up, groundCheckDistance, groundMask);

        if (hit.collider != null) {
            Quaternion rot = Quaternion.LookRotation(Vector3.forward, (Vector3)hit.normal);
            if (!isGrounded) {
                OnHitGround();
                //Align with ground (instantly)
                transform.rotation = rot;
                return;
            }
            //Align with ground (slowly)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, Time.deltaTime * 80f);

        } else {
            if (isGrounded) {
                OnLeaveGround();
            }
        }
    }
    //We are no longer grounded
    void OnLeaveGround() {
        isGrounded = false;
        //print("OnLeaveGround");

        trackTrailR.emitting = false;
        trackTrailL.emitting = false;
        trailParticles.Stop();
        SpawnImpactEffect();
    }

    //We are grounded again
    void OnHitGround() {
        isGrounded = true;
        //print("OnHitGround");

        isJumping = false;
        trackTrailR.emitting = true;
        trackTrailL.emitting = true;
        trailParticles.Play();
        SpawnImpactEffect();
    }

    public GameObject impactParticles;

    void SpawnImpactEffect() {
        GameObject gm = Instantiate(impactParticles, transform.position, transform.rotation);
        Destroy(gm, 2f);
    }
}