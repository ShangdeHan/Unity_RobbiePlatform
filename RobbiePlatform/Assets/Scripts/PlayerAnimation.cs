using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {
    Animator anim;
    PlayerMovement movement;
    Rigidbody2D rb;
    int groundID, haingingID, crouchID, speedID, fallID;

    void Start()
    {
        anim = GetComponent<Animator>();
        movement = GetComponentInParent<PlayerMovement>();
        rb = GetComponentInParent<Rigidbody2D>();
        groundID = Animator.StringToHash("isOnGround");
        haingingID = Animator.StringToHash("isHanging");
        crouchID = Animator.StringToHash("isCrouching");
        speedID = Animator.StringToHash("speed");
        fallID = Animator.StringToHash("verticalVelocity");

    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat(speedID, Mathf.Abs(movement.xVelocity));
        anim.SetBool(groundID, movement.isOnGround);
        anim.SetBool(haingingID, movement.isHanging);
        anim.SetBool(crouchID, movement.isCrouch);
        anim.SetFloat(fallID, rb.velocity.y);
    }

    public void StepAudio()
    {
        AudioManager.PlyerFootStepAudio();
    }
    public void CrouchStepAudio()
    {
        AudioManager.PlyerCrouchFootStepAudio();
    }
}
