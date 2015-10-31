using UnityEngine;
using System.Collections;

public class ObstacleSmall : MonoBehaviour {
    public float speed = 0;

    public float switchTime = 2;
	// Use this for initialization
	void Start () {
       GetComponent<Rigidbody2D>().velocity = Vector2.left * speed;

       InvokeRepeating("Switch", 0, switchTime);
	}

   void Switch()
   {
       GetComponent<Rigidbody2D>().velocity *= -1;
   }
	// Update is called once per frame
	void Update () {
	
	}
}
