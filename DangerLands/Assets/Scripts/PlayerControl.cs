using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
    [HideInInspector]
    public bool facingRight = true;         // For determining which way the player is currently facing.
    [HideInInspector]
    public bool jump = false;               // Condition for whether the player should jump.


    public float moveForce = 5;         // Amount of force added to move the player left and right.
    public float maxSpeed = 1f;             // The fastest the player can travel in the x axis.
    public AudioClip[] jumpClips;           // Array of clips for when the player jumps.
    public float jumpForce = 1000f;         // Amount of force added when the player jumps.
                                            //public AudioClip[] taunts;				// Array of clips for when the player taunts.
                                            //public float tauntProbability = 50f;	// Chance of a taunt happening.
                                            //public float tauntDelay = 1f;			// Delay for when the taunt should happen.


    //private int tauntIndex;					// The index of the taunts array indicating the most recent taunt.
    private Transform groundCheck;          // A position marking where to check if the player is grounded.
    private bool grounded = false;          // Whether or not the player is grounded.
    private Animation anim;					// Reference to the player's animator component.

    string[] animations;

    Vector3 respawnPoint;
    bool isDie = false, ifJump = false;
    public AudioSource audioCoin;
    float hValue = 0;
    void Awake()
    {

        // Setting up references.
        groundCheck = transform.Find("groundCheck");
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
        if (anim.IsPlaying(animations[6]))
            return;

        if (anim.IsPlaying(animations[4]))
            return;

        if (anim.IsPlaying(animations[7]))
            return;

        if (isDie == true)
        {
            Respawn();
            isDie = false;
        }




        // The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        // If the jump button is pressed and the player is grounded then the player should jump.
        if (ifJump && grounded)
        {
            //anim.Play(animations[9]);
            jump = true;
            ifJump = false;

        }
    }


    void FixedUpdate()
    {
        //anim.Play(animations[10]);

        if (anim.IsPlaying(animations[6]))
            return;

        if (anim.IsPlaying(animations[4]))
            return;

        if (anim.IsPlaying(animations[7]))
            return;

        //float h = Input.GetAxis("Horizontal");

        if (hValue * GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
        {
            anim.Play(animations[1]);
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * hValue * moveForce);
        }

        if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed)
        {
            anim.Play(animations[1]);
            GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }

        if (hValue > 0 && !facingRight)
        {
            Flip();
        }

        else if (hValue < 0 && facingRight)
        {
            Flip();
        }


        if (jump)
        {

            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }
    }


    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.z *= -1f;
        transform.localScale = theScale;
        //if (transform.rotation.eulerAngles != new Vector3(0.0f, 205.0f, 0.0f))
        //{
        //    Debug.Log(transform.rotation.eulerAngles);
        //    transform.rotation.eulerAngles = (new Vector3(0.0f, 205.0f, 0.0f));
        //}
        //else
        //{ Debug.Log(transform.rotation.eulerAngles);
        //    transform.rotation.(new Vector3(0.0f, 205.0f, 0.0f));
        //}
    }


    //public IEnumerator Taunt()
    //{
    //	// Check the random chance of taunting.
    //	float tauntChance = Random.Range(0f, 100f);
    //	if(tauntChance > tauntProbability)
    //	{
    //		// Wait for tauntDelay number of seconds.
    //		yield return new WaitForSeconds(tauntDelay);

    //		// If there is no clip currently playing.
    //		//if(!GetComponent<AudioSource>().isPlaying)
    //		//{
    //		//	// Choose a random, but different taunt.
    //		//	tauntIndex = TauntRandom();

    //		//	// Play the new taunt.
    //		//	GetComponent<AudioSource>().clip = taunts[tauntIndex];
    //		//	GetComponent<AudioSource>().Play();
    //		//}
    //	}
    //}


    //int TauntRandom()
    //{
    //	// Choose a random index of the taunts array.
    //	int i = Random.Range(0, taunts.Length);

    //	// If it's the same as the previous taunt...
    //	if(i == tauntIndex)
    //		// ... try another random taunt.
    //		return TauntRandom();
    //	else
    //		// Otherwise return this index.
    //		return i;
    //}


    public void Move(float InputAxis)
    {
        hValue = InputAxis;
    }

    public void Jump(int InputAxis)
    {
        ifJump = InputAxis == 0 ? false : true;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Bonus")
        {
            audioCoin.Play();
            Destroy(collider.gameObject);
            GUIScript.instance.ChangeBonus();
        }

        if (collider.gameObject.tag == "Respawn")
        {
            respawnPoint = collider.gameObject.transform.position;
        }

        if (collider.gameObject.tag == "DeadZone")
        {
            Respawn();
        }

        if (collider.gameObject.tag == "FinalBlock")
        {
            //anim.Play(animations[7]);
            if (!PlayerPrefs.HasKey(Application.loadedLevelName))
            {
                PlayerPrefs.SetInt(Application.loadedLevelName, 1);
            }
            Application.LoadLevel("ChooseLevel");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Saw")
        {
            isDie = true;
            anim.Play(animations[6]);
        }

        if (collision.gameObject.tag == "FinalBlock")
        {
            anim.Play(animations[7]);
            if (!PlayerPrefs.HasKey(Application.loadedLevelName))
            {
                PlayerPrefs.SetInt(Application.loadedLevelName, 1);
            }
            Application.LoadLevel("ChooseLevel");
        }

    }

    public void Respawn()
    {
        transform.position = respawnPoint;
        GUIScript.instance.ChangeLife();
        hValue = 0;
        //anim.Play(animations[4]);
    }
}
