using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private GameObject bottomPoint;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float dashForce;
    [SerializeField] private float dashReloadTime;

    private Rigidbody rb;
    private bool canJump = false;
    private bool canDash = false;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        MoveLogic();
        Jump();
    }
    private void Update()
    {
        CanJumpCheck();
    }
    private void MoveLogic()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        var moveStraight = transform.forward * vertical * moveSpeed * Time.fixedDeltaTime;
        var moveRight = transform.right * horizontal * moveSpeed * Time.fixedDeltaTime;
        Vector3 move = moveRight + moveStraight;
        rb.MovePosition(rb.position + move);
    }
    private void Jump()
    {
        if (Input.GetButton("Jump") && canJump)
        {
            Vector3 jumpVector = new Vector3(0, jumpForce, 0);
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y * 0.2f, rb.linearVelocity.z);
            rb.AddForce(jumpVector, ForceMode.Impulse);
            canJump = false;
        }
    }
    //private void /*OnCollisionStay*/(Collision collision)
    //{
    //    foreach (var contact in collision.contacts)
    //    {
    //        if (contact.normal.y > 0.7f)
    //        {
    //            canJump = true;
    //            return;
    //        }
    //    }
    //}
    private void CanJumpCheck()
    {
        Vector3 bottomPos = new Vector3(bottomPoint.transform.position.x, bottomPoint.transform.position.y, bottomPoint.transform.position.z);
        Ray raycastDown = new Ray(bottomPos, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(raycastDown, out hit, 0.1f))
        {
            canJump = true;
        }
    }
    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.Q) && canDash)
        {
            Vector3 dashVector = new Vector3(dashForce, 0, 0);
            rb.AddForce(dashVector, ForceMode.Impulse);
            canDash = false;
            StartCoroutine(ReloadingDash());
        }
    }
    private IEnumerator ReloadingDash()
    {
        yield return new WaitForSeconds(dashReloadTime);
        canDash = true;
    }
}