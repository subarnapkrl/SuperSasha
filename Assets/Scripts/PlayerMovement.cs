using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D rb2d;
    Animator animator;
    SpriteRenderer sprite;

    BoxCollider2D coll;

    [SerializeField] private LayerMask jumpableGround;

    float dirX=0f;
    float moveSpeed=7f;
    float jumpForce=14f;

    private enum MovementState{idle,running,jumping,falling};

    [SerializeField] private AudioSource jumpSoundEffect;

    
    // Start is called before the first frame update
    void Start()
    
    {
        rb2d=GetComponent<Rigidbody2D>();
        animator=GetComponent<Animator>();
        sprite=GetComponent<SpriteRenderer>();
        coll=GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
         dirX=Input.GetAxisRaw("Horizontal");
        rb2d.velocity=new Vector2(dirX * moveSpeed,rb2d.velocity.y);

        if(Input.GetButtonDown("Jump") && IsGroundCheck())
        {
            rb2d.velocity=new Vector2(rb2d.velocity.x,jumpForce);
            jumpSoundEffect.Play();
        }

       AnimationUpdate();

    }

    void AnimationUpdate()
    {
        MovementState states;
         if(dirX>0f)
        {
            states=MovementState.running;
            sprite.flipX=false;
        }
        else if(dirX<0f)
        {
            states=MovementState.running;
            sprite.flipX=true;
        }else{
            states=MovementState.idle;
        }

        if(rb2d.velocity.y>0.1f)
        {
            states=MovementState.jumping;
        }
        else if(rb2d.velocity.y<-0.1f)
        {
            states=MovementState.falling;
        }

        animator.SetInteger("state",(int)states);
    }


    private bool IsGroundCheck()
    {
       return Physics2D.BoxCast(coll.bounds.center,coll.bounds.size,0f,Vector2.down,0.1f,jumpableGround);
    }
}
