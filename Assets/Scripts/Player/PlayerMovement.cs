using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    public float moveSpeed;
    private Rigidbody2D myRigidbody;
    public Vector3 change;
    private Animator animator;

    public bool isMoving = false;
    private bool isSprinting = false;
    public bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        transform.position = new Vector3(2, 2, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canMove)
        {
            
            change = Vector3.zero;
            change.x = Input.GetAxisRaw("Horizontal");
            change.y = Input.GetAxisRaw("Vertical");
            UpdateAnimationAndMove();
        }
        else
        {
            change = Vector3.zero;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSprinting = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSprinting = false;
        }
    }

    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            isMoving = true;

            MoveCharacter();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("isMoving", true);
            
        }
        else
        {
            isMoving = false;
            animator.SetBool("isMoving", false);
        }
    }

    void MoveCharacter()
    {
        if (isSprinting)
        {
            myRigidbody.MovePosition(transform.position + change * (moveSpeed * 2) * Time.deltaTime);
        }
        else
        {
            
            myRigidbody.MovePosition(transform.position + change * moveSpeed * Time.deltaTime);
        }
    }
}
