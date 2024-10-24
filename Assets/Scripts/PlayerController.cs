using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Tilemaps;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private Transform tr;
     
    [Header("Move Info")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float runSpeed;
    [SerializeField] private float activeSpeed;
    [SerializeField] private bool canDoubleJump;

    [Header("Groud Info")]    
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] float groundCheckRadius;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private bool isGounded;    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        isGounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGround);
                
        var xInput = Input.GetAxisRaw("Horizontal");

        activeSpeed = moveSpeed;

        if (Input.GetKey(KeyCode.LeftControl))
            activeSpeed = runSpeed;

        rb.velocity = new Vector2(xInput * activeSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            if (isGounded)
            {
                Jump();
                canDoubleJump = true;
                //anim.SetBool("isDoubleJump", false);
            }
            else
            {
                if (canDoubleJump)
                {
                    Jump();
                    canDoubleJump = false;
                    //anim.SetBool("isDoubleJump", true);
                    anim.SetTrigger("isDoubleJump");
                }
            }            
        }

        if (isGounded)
        {
            canDoubleJump = true;
            anim.SetBool("isDoubleJump", false);
        }

        //Configurando direções
        if (rb.velocity.x > 0)
            transform.localScale = new Vector3(1f, transform.localScale.y, transform.localScale.z);
        if (rb.velocity.x < 0)
            transform.localScale = new Vector3(-1f, transform.localScale.y, transform.localScale.z);

        //Chamando animações
        anim.SetFloat("speed", Mathf.Abs(rb.velocity.x)); 
        anim.SetBool("isGrounded", isGounded);
        anim.SetFloat("ySpeed", rb.velocity.y);        

    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
      

}
