using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour {

    public GameObject[] caveLocks;
    public GameObject[] woodLocks;

    public static LevelController instance;

	void Start ()
    {
        instance = this;

        if (PlayerPrefs.HasKey("WoodTutorial"))
        {
            woodLocks[0].SetActive(false);
        }
        if (PlayerPrefs.HasKey("CaveTutorial"))
        {
            caveLocks[0].SetActive(false);
        }
        if (PlayerPrefs.HasKey("WoodMiddle"))
        {
            woodLocks[1].SetActive(false);
        }
        if (PlayerPrefs.HasKey("CaveMiddle"))
        {
            caveLocks[1].SetActive(false);
        }
    }

}
