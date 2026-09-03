using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class Movement1 : Entity, IJump
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject bottomPoint;
    [SerializeField] private float secondJumpCD;
    [SerializeField] private float dashCD;
    [SerializeField] private float dashDistance;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float moveSpeed;


    private static Action<bool> onDoubleJumped;

    private Rigidbody rb;
    private Vector3 lastPos;

    private bool posX = false;
    private bool negX = false;
    private bool posY = false;
    private bool negY = false;
    private float x = 0;
    private float y = 0;

    private bool canCheckMove = true;
    private bool canCheckJump = true;
    private bool canCheckDash = true;

    private bool canDoubleJump = false;
    private bool canJump = false;
    private bool canDash = false;
    private bool moved = false;
    private bool dashed = false;

    private bool jumpPressed;
    private bool dashPressed;

    public float JumpForce { get => jumpForce; set => jumpForce = value; }

    private void Start()
    {
        onDoubleJumped += RecoverDash;
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        if (canCheckMove)
        {
            Move();
            if (moved)
            {
                animator.SetFloat("speed", 1f, 0.1f, Time.deltaTime);
            }
            else
            {
                animator.SetFloat("speed", 0f, 0.1f, Time.deltaTime);
            }
        }
        CanJumpCheck();
        CanDashCheck();
        if (canCheckJump) JumpLogic();
        if(canCheckDash) Dash();
    }

    private void Update()
    {
        SetDirection();
        if (Input.GetButtonDown("Jump"))
            jumpPressed = true;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            dashPressed = true;
        }
    }
    private void SetDirection()
    {
        moved = false;
        if (posX || negX || posY || negY)
        {
            moved = true;
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            posY = false;
            y = 0;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            negY = false;
            y = 0;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            posX = false;
            x = 0;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            negX = false;
            x = 0;
        }

        if (Input.GetKey(KeyCode.W))
        {
            if (posX || negX || negY)
            {
                return;
            }
            posY = true;
            y = 1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (posX || negX || posY)
            {
                return;
            }
            negY = true;
            y = -1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (negX || posY || negY)
            {
                return;
            }
            posX = true;
            x = 1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            if (posX || posY || negY)
            {
                return;
            }
            negX = true;
            x = -1;
        }

        animator.SetFloat("x", x, 0.1f, Time.deltaTime);
        animator.SetFloat("y", y, 0.1f, Time.deltaTime);
        //if (y != 1 && y != -1)
        //{
        //    animator.SetFloat("y", 0);
        //    animator.SetFloat("x", x, 0.15f, Time.deltaTime);
        //}
        //else
        //{
        //    animator.SetFloat("x", 0);
        //    animator.SetFloat("x", x, 0.15f, Time.deltaTime);
        //}
    }

    protected override void Move()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        var moveStraight = transform.forward * vertical * moveSpeed * Time.fixedDeltaTime;
        var moveRight = transform.right * horizontal * moveSpeed * Time.fixedDeltaTime;
        Vector3 move = moveRight + moveStraight;
        rb.MovePosition(rb.position + move);
        //moved = false;
        Vector3 playerPosYNullized = transform.position;
        playerPosYNullized.y = 0;
        //if (Vector3.Distance(playerPosYNullized, lastPos) > 0.01f)
        //{
        //    moved = true;
        //    lastPos = playerPosYNullized;
            
        //}
    }
    private void JumpLogic()
    {
        if (jumpPressed && (canJump || canDoubleJump))
        {

            if (canJump)
            {
                canJump = false;
                canDoubleJump = false;
                StartCoroutine(SecondJumpCD(secondJumpCD));
                Jump();
            }
            else if (canDoubleJump)
            {
                canDoubleJump = false;
                Jump();
                Movement1.onDoubleJumped.Invoke(canDash);
            }
        }
        jumpPressed = false;
    }
    public void Jump()
    {
        Vector3 jumpVector = new Vector3(0, JumpForce, 0);
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y * 0.2f, rb.linearVelocity.z);
        rb.AddForce(jumpVector, ForceMode.Impulse);
        animator.SetTrigger("jump");
        animator.SetBool("onLand", false);
    }
    private void CanJumpCheck()
    {
        Vector3 bottomPos = new Vector3(bottomPoint.transform.position.x, bottomPoint.transform.position.y, bottomPoint.transform.position.z);
        Ray raycastDown = new Ray(bottomPos, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(raycastDown, out hit, 0.1f))
        {
            animator.SetBool("onLand", true);
            canJump = true;
            canDoubleJump = true;
        }
    }
    private IEnumerator SecondJumpCD(float coolDown)
    {
        yield return new WaitForSeconds(coolDown);
        canDoubleJump = true;
    }
   
    private void Dash()
    {
        if (dashPressed && !dashed)
        {
            canCheckMove = false;
            canCheckJump = false;
            canCheckDash = false;

            Vector3 dir = transform.forward;
            dir.y = 0f;
            dir.Normalize();

            Vector3 target = rb.position + dir * dashDistance;
            StartCoroutine(DashRoutine(target, dashSpeed));
            canDash = false;
            dashed = true;
        }
        dashPressed = false;
    }
    private void CanDashCheck()
    {
        if (!canDash)
        {
            Vector3 bottomPos = new Vector3(bottomPoint.transform.position.x, bottomPoint.transform.position.y, bottomPoint.transform.position.z);
            Ray raycastDown = new Ray(bottomPos, Vector3.down);
            RaycastHit hit;
            if (Physics.Raycast(raycastDown, out hit, 0.1f) && dashed)
            {
                StartCoroutine(DashCD(dashCD));
                canDash = true;
            }
        }
    }
    private void RecoverDash(bool toRecover)
    {
        toRecover = true;
        dashed = false;
    }
    private IEnumerator DashRoutine(Vector3 target, float speed)
    {
        Vector3 dir = transform.forward;
        dir.y = 0f;
        dir.Normalize();

        Vector3 start = rb.position;

        while (Vector3.Distance(rb.position, target) > 0.01f)
        {
            Vector3 newPos = Vector3.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
            rb.linearVelocity = Vector3.zero;

            yield return new WaitForFixedUpdate();
        }
        rb.MovePosition(target);

        canCheckMove = true;
        canCheckJump = true;
        canCheckDash = true;
    }
    public void LockOrNotMovement(bool condition)
    {
        canCheckDash = condition;
        canCheckJump = condition;
        canCheckMove = condition;
    }

    private IEnumerator DashCD(float coolDown)
    {
        yield return new WaitForSeconds(coolDown);
        dashed = false;
    }
}