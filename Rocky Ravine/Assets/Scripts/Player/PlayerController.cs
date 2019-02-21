using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

    Rigidbody2D body;
    Animator anim;

    bool isJumping = false;
    bool isGrounded = false;

    public LayerMask groundMask;
    float groundCheckDistance = .95f;

    //Effects
    [SerializeField] ParticleSystem trailParticles;
    [SerializeField] TrailRenderer trackTrailR;
    [SerializeField] TrailRenderer trackTrailL;

    //Audio
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource groundedAudioSource;
    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip landSound;

    //Public fields used by scripts
    public float SpeedScale = 1f;

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

    bool isMoving = false;

    /// <summary>
    /// Called by GameManager
    /// </summary>
    public void StartMoving ()
    {
        isMoving = true;

        groundedAudioSource.Play();
    }

    public void StopMoving()
    {
        isMoving = false;

        groundedAudioSource.Stop();
    }

    public float Speed = 6f;
    public float JumpForce = 14f;

    void Update () {
        //Visuals
        DoGroundCheck();
        if (anim != null)
            anim.SetBool("IsGrounded", isGrounded);

        if (!isMoving) {
            //Don't move
            if (isGrounded)
                body.velocity = Vector2.zero;

            return;
        }

        //Clamp movement
        if (isGrounded && !isJumping) {
            body.velocity = transform.right * Speed * SpeedScale;
        }

        //Jump
        if (CanJump()) {
            isJumping = true;
            body.AddForce((Vector2)transform.up * JumpForce, ForceMode2D.Impulse);
            //body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            if (anim != null)
                anim.SetTrigger("Jump");
        }
    }

    bool CanJump ()
    {
        return isGrounded && !isJumping && Input.GetMouseButtonDown(0);
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
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, Time.deltaTime * 200f * SpeedScale);

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

        if (groundedAudioSource != null)
        {
            audioSource.PlayOneShot(jumpSound);
            groundedAudioSource.Stop();
        }
    }

    //We are grounded again
    void OnHitGround() {
        isGrounded = true;

        isJumping = false;

        trackTrailR.emitting = true;
        trackTrailL.emitting = true;
        trailParticles.Play();
        SpawnImpactEffect();

        if (groundedAudioSource != null)
        {
            audioSource.PlayOneShot(landSound);

            if (GameManager.State == GameState.InGame)
                groundedAudioSource.Play();
        }
    }

    public GameObject impactParticles;

    void SpawnImpactEffect() {
        GameObject gm = Instantiate(impactParticles, transform.position, transform.rotation);
        Destroy(gm, 2f);
    }
}