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
    float groundCheckDistance = .9f;

    //Effects
    [SerializeField] ParticleSystem trailParticles;
    [SerializeField] TrailRenderer trackTrailR;
    [SerializeField] TrailRenderer trackTrailL;

    //Public fields used by scripts
    public float SpeedScale = 1f;
    public bool IsFrozen = false;

    void Start () {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        trailParticles.Stop();
        trackTrailR.emitting = false;
        trackTrailL.emitting = false;

        //Place ourselves on ground
        RaycastHit2D hit = Physics2D.Raycast(body.position, -Vector2.up, 999f, groundMask);

        if (hit.collider != null) {
            transform.position = hit.point + Vector2.up * .6f;
            //Align with ground
            Quaternion rot = Quaternion.LookRotation(Vector3.forward, (Vector3)hit.normal);
            transform.rotation = rot;
        }
        //Enable trails (should be disabled in scene to avoid visual glitch)
        trackTrailR.enabled = true;
        trackTrailL.enabled = true;
    }

    float speed = 6f;
    float jumpForce = 14f;

    void Update () {
        if (IsFrozen) {
            //Don't move
            body.velocity = Vector2.zero;
            return;
        }

        DoGroundCheck();
        if (anim != null)
            anim.SetBool("IsGrounded", isGrounded);

        //Clamp movement
        if (isGrounded && !isJumping) {
            body.velocity = transform.right * speed * SpeedScale;
        }

        //Jump
        if (isGrounded && !isJumping && Input.GetMouseButtonDown(0)) {
            isJumping = true;
            body.AddForce((Vector2)transform.up * jumpForce, ForceMode2D.Impulse);
            //body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

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
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, Time.deltaTime * 80f * SpeedScale);

        } else {
            if (isGrounded) {
                OnLeaveGround();
            }
        }
    }
    //We are no longer grounded
    void OnLeaveGround() {
        isGrounded = false;

        trackTrailR.emitting = false;
        trackTrailL.emitting = false;
        trailParticles.Stop();
        SpawnImpactEffect();
    }

    //We are grounded again
    void OnHitGround() {
        isGrounded = true;

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