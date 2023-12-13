using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class PlayerController : NetworkBehaviour
{
    [Header("Movement")]
    public float speed = 5f;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float groundDrag;

    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private float airMultiplyier;
    private bool readyToJump = true;
    

    [Header("Ground Check")]
    [SerializeField] private float playerHeight;
    [SerializeField] private LayerMask whatIsGround;
    private bool grounded;




    [SerializeField] private Animator animator;

    private Rigidbody rb;
    private Vector3 dir;

    NetworkVariable<int> randomNumber = new NetworkVariable<int>(1);

    // Start is called before the first frame update
    void Start()
    {
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!IsOwner) return;

        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight ,whatIsGround);

        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
        
        GetInput();
        SpeedControl();

    }
    private void FixedUpdate()
    {
        if(!IsOwner) return;
        Move();
    }


    private void GetInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        dir = transform.forward * vertical + transform.right * horizontal;

        if (dir != Vector3.zero)
        {
            animator.SetBool("IsMoving", true);
            animator.SetBool("IsDancing", false);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = sprintSpeed;
            animator.SetBool("IsRunning", true);
        }
        else
        {
            speed = walkSpeed;
            animator.SetBool("IsRunning", false);
        }  


        if (Input.GetKeyDown(KeyCode.Space) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }



        if (Input.GetKeyDown(KeyCode.Z))
        {
            animator.SetBool("IsDancing", true);
        }

    }

    void Move()
    {
        if(grounded)
            rb.AddForce(dir.normalized * speed * Time.deltaTime, ForceMode.Force);

        if (!grounded)
            rb.AddForce(dir.normalized * speed * Time.deltaTime * airMultiplyier, ForceMode.Force);
    }

    void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x,0,rb.velocity.z);

        if(flatVel.magnitude > speed)
        {
            Vector3 limitedVel = flatVel.normalized * speed;
            rb.velocity = new Vector3(limitedVel.x,rb.velocity.y,limitedVel.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }
}
