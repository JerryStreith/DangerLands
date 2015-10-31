using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {

void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag=="Block")
        {
            Debug.Log("I see you!");
        }
    }
}
