using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{

    public static bool isSound = true;
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        if (PlayerPrefs.HasKey("Sound"))
        {
            isSound = PlayerPrefs.GetInt("Sound") == 1 ? true : false;
        }
    }

    void OnApplicationQuit()
    {
        int value = isSound ? 1 : 0;
        PlayerPrefs.SetInt("Sound", value);
    }
}
