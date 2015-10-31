using UnityEngine;
using System.Collections;

public class SpecialPlatform : MonoBehaviour
{

    bool can = false;
    Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if(can)
        {
            transform.Translate(Vector2.down * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {         
        if (collision.gameObject.tag == "Player")
        {
            can = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "DeadZone")
        {
            can = false;
            transform.position = startPos;
        }  
    }
}
