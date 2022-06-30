using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Parameters")]
    [SerializeField] private float speed = 60f;
    [SerializeField] private float jumpPower = 17.5f;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;
    [Header("Coyote Time")]
    private Vector3 velocity = Vector3.zero;
    [Header("Multiple Jumps")]
    [SerializeField] private int maxJumps = 2;
    private int jumpCount;
    private bool m_FacingRight = true;
    [Header("Wall Jumping")]
    [SerializeField] private float wallJumpX; //Horizontal wall jump force
    [SerializeField] private float wallJumpY; //Vertical wall jump force
    
    [Header("Layers")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    [Header("Sounds")]
    [SerializeField] private AudioClip jumpSound;
    public float GC_width = 0.7f;
    public float GC_height = 0.3f;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalMove;
    public Transform groundCheck;
    private bool jump;
    private bool jumpstop;
    public float Vspeed = 100f;
    private float verticalInput;
    private float verticalMove;
    float timer = 0;

    public bool winBool = false;

    private void Awake()
    {
        //Grab references for rigidbody and animator from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (winBool)
        {
            StartCoroutine(winDelay());
        }

        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
        verticalInput = Input.GetAxisRaw("Vertical") * Vspeed;
        if(verticalInput < 0){
            verticalMove = verticalInput;
        }else{
            verticalMove = 0;
        }

        //Animator
        anim.SetBool("run", horizontalMove != 0);
        anim.SetBool("grounded", isGrounded());
        
        
        
        //Jump
        if (Input.GetKeyDown(KeyCode.Space)){
            jump = true;
        }

        //Adjustable jump height
        if (Input.GetKeyUp(KeyCode.Space) && body.velocity.y > 0){
            jumpstop = true;
        }

            
    }


    private void FixedUpdate(){
        //jump
        if(jump){
            Jump();
            jump = false;
        }
        //jump height
        if(jumpstop){
            JumpStop();
            jumpstop = false;
        }
        //move
        Vector3 targetSpeed = new Vector2(horizontalMove * Time.fixedDeltaTime * 10f, body.velocity.y + verticalMove);
        body.velocity = Vector3.SmoothDamp(body.velocity, targetSpeed, ref velocity ,m_MovementSmoothing);

        /*flip*/if (horizontalMove > 0 && !m_FacingRight)
			{
				//flip player
				Flip();
			}
			
			else if (horizontalMove < 0 && m_FacingRight)
			{
				//flip the player.
				Flip();
			}
        if (onWall())
        {
            jumpCount = 0; 
            if(Input.GetAxisRaw("Vertical") < 0){
                body.gravityScale = 15;
                //Debug.Log('s');
            }else{
            body.gravityScale = 0;
            }
            body.velocity = Vector2.zero;
        }else{
            body.gravityScale = 5;
        }

        if (isGrounded()){
            timer -= Time.fixedDeltaTime;
            if(timer <= 0){
                jumpCount = 0; //Reset jump counter
                //Debug.Log("RESET");
            }
        }
    }

    /*private void Jump()
    {
        if (coyoteCounter <= 0 && !onWall() && jumpCounter <= 0) return; 
        //If coyote counter is 0 or less and not on the wall and don't have any extra jumps don't do anything

        SoundManager.instance.PlaySound(jumpSound);

        if (onWall()){
            WallJump();
        }else{
            if (isGrounded())
                body.velocity = new Vector2(body.velocity.x, jumpPower);
            else
            {
                //If not on the ground and coyote counter bigger than 0 do a normal jump
                if (coyoteCounter > 0)
                    body.velocity = new Vector2(body.velocity.x, jumpPower);
                else
                {
                    if (jumpCounter > 0) //If we have extra jumps then jump and decrease the jump counter
                    {
                        body.velocity = new Vector2(body.velocity.x, jumpPower);
                        jumpCounter--;
                    }
                }
            }

            //Reset coyote counter to 0 to avoid double jumps
            coyoteCounter = 0;
        }
    }*/

    private void Jump(){
            if(onWall()){
                WallJump();
                jumpCount++;
            }else if(!(jumpCount >= maxJumps)){
                body.velocity = new Vector2(body.velocity.x,jumpPower);
                jumpCount++;
                //Debug.Log("JUMP");
                timer = 0.05f;
            }
        }

    private void Flip(){
        m_FacingRight = !m_FacingRight;

		
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
    }

    private void JumpStop(){
        body.velocity = new Vector2(body.velocity.x, body.velocity.y / 2);
    }

    private void WallJump()
    {
        body.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * wallJumpX, wallJumpY));
        wallJumpCooldown = 0;
    }


    private bool isGrounded(){
        RaycastHit2D raycastHit = Physics2D.BoxCast(groundCheck.position, new Vector3(GC_width, GC_height, 0), 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    void OnDrawGizmosSelected(){
        boxCollider = GetComponent<BoxCollider2D>();
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheck.position, new Vector3(GC_width, GC_height, 0));
    }

    private bool onWall()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }
    public bool canAttack()
    {
        return horizontalMove == 0 && isGrounded() && !onWall();
    }

    IEnumerator winDelay()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("win");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "POWERPAD")
        {
            speed = 45;
        }
    }
}