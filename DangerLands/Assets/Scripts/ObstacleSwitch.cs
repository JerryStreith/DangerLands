using UnityEngine;
using System.Collections;

public class ObstacleSwitch : MonoBehaviour {

    public float speed = 0;

    public float switchTime = 2;
    // Use this for initialization
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;

        //switchTime = Random.Range(1f, 2.5f);
        InvokeRepeating("Switch", 0, switchTime);
    }

    void Switch()
    {
        GetComponent<Rigidbody2D>().velocity *= -1;
    }
}
