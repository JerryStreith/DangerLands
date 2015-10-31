using System.Collections;
using UnityEngine;

public class characterController : MonoBehaviour
{
    public float maxSpeed = 6.5f;
    public float jumpForce = 500f;
    bool facingRight = true;
    bool grounded;
    public Transform groundCheck;
    public int directionInput;
    public float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    Vector3 respawnPoint;
    public float move;

    private Animation anim;
    string[] animations;

    public AudioSource audioCoin;

    bool ifJump = false;
    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        //move = Input.GetAxis("Horizontal");
        if (!grounded)
            return;        
    }

    public void Move(float InputAxis)
    {
        move = InputAxis;
    }
    public void Jump(int InputAxis)
    {
        ifJump = InputAxis == 0 ? false : true;
    }

    //public void Move(int InputAxis)
    //{
    //    directionInput = InputAxis;
    //    GetComponent<Rigidbody2D>().velocity = new Vector2(InputAxis * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
    //}

    //public void Jump(bool IsJump)
    //{
    //    if (!grounded)
    //        return;
    //    if (grounded)
    //    {
    //        GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
    //    }
    //}

    void Awake()
    {
        anim = GetComponent<Animation>();
        respawnPoint = transform.position;

        animations = new string[anim.GetClipCount()];
        int i = 0;
        foreach (AnimationState a in anim)
        {
            animations[i] = a.name;
            i++;
        }
    }

    void Update()
    {
        if (grounded && ifJump)//(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            ifJump = false;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
        }
        GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

        if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.z *= -1f;
        transform.localScale = theScale;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Bonus")
        {
            audioCoin.Play();
            Destroy(col.gameObject);
            GUIScript.instance.ChangeBonus();            
        }

        if (col.gameObject.tag == "Respawn")
        {
            respawnPoint = col.gameObject.transform.position;
        }

        if (col.gameObject.tag == "Lava")
        {
            Application.LoadLevel(1);
        }

        if (col.gameObject.name.Contains("dieCollider") || col.gameObject.tag == "DeadZone")
        {
            Respawn();           
        }

        if (col.gameObject.tag == "FinalBlock")
        {
            //anim.Play(animations[7]);
            if (!PlayerPrefs.HasKey(Application.loadedLevelName))
            {
                PlayerPrefs.SetInt(Application.loadedLevelName, 1);
            }
            Application.LoadLevel("ChooseLevel");
        }
    }

    public void Respawn()
    {
        transform.position = new Vector3(respawnPoint.x, -3.69f, 0);
        GUIScript.instance.ChangeLife();
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "FinalBlock")
        {
            //anim.Play(animations[7]);
            if (!PlayerPrefs.HasKey(Application.loadedLevelName))
            {
                PlayerPrefs.SetInt(Application.loadedLevelName, 1);
            }
            Application.LoadLevel("ChooseLevel");
        }
    }
}