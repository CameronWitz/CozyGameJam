using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // movement
    public float xForce;
    public float maxSpeed;
    public float maxAirSpeed;
    public float groundDrag;
    public float airModifier;
    

    // jumping
    public Transform groundLoc;
    public float jumpCheckRadius;
    public LayerMask groundLayer;
    public float jumpSpeed;
    public KeyCode jumpKeyCode;
    public float airDrag;
    
    // rigidbody
    private Rigidbody2D rb;
    private float xInput;
    private bool isGrounded;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       MyInput();
       // SpeedControl();
    }

    void MyInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        isGrounded = Physics2D.OverlapCircle(groundLoc.position, jumpCheckRadius, groundLayer);
        // check if currently jumping
        if (Input.GetKeyDown(jumpKeyCode) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            rb.drag = airDrag;
        }
        // otherwise if we are on the ground
        else if (isGrounded)
        {
            rb.drag = groundDrag;
        }
    }

    void FixedUpdate()
    {
        MovePlayer();
        SpeedControl();
    }

    void MovePlayer()
    {
        if (!isGrounded)
        {
            xInput *= airModifier;
        }
        rb.AddForce(new Vector2(xInput * xForce, 0f), ForceMode2D.Force);
    }

    void SpeedControl()
    {
        float speedCheck = maxSpeed;
        if (!isGrounded)
        {
            speedCheck = maxAirSpeed;
        }

        float dir = 1f;
        if (rb.velocity.x < 0f)
        {
            dir = -1;
        }
        if (rb.velocity.x*dir > speedCheck)
        {
            rb.velocity = new Vector2(speedCheck*dir, rb.velocity.y);
        }
    }
    
    
    
    
    
    
    
    
    
}
