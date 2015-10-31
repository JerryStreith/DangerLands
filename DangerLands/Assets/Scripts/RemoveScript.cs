using UnityEngine;
using System.Collections;

public class RemoveScript : MonoBehaviour {

	// Update is called once per frame
	void Update() {
		
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		if (player != null)
			if (player.transform.position.x - gameObject.transform.position.x > 20)
				Destroy(gameObject);
	}
}