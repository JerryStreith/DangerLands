using UnityEngine;
using System.Collections;

public class GeneratorScript : MonoBehaviour
{
    public float x = 0;
    public GameObject Background;

    void Start()
    {

    }

    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("background").Length < 10)
        {
            GameObject game_object = (GameObject)Instantiate(Background, new Vector3(x, 0, 0), Quaternion.identity);
            game_object.tag = "background";
            x += 19.704f;
        }
    }
    //public float x = 10;

    //public GameObject Background;
    //public float plusValue;

    //void Start()
    //{
    //    float value = Background.transform.position.x;
    //    while (value < x)
    //    {
    //        value += plusValue;
    //        GameObject go = (GameObject)Instantiate(Background, new Vector3(value, 1, 5), Quaternion.identity);
    //        go.transform.parent = transform;  
    //    }
    //}
}