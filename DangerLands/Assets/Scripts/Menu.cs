using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour
{
    bool isON = true;
    GameObject on, off;
    public bool menu = false;
    public GameObject sound;
    public GameObject cam;
    public GameObject eventT;
    public UIButton musicButton;
    public GameObject root;
    public UILabel percentLabel;
    void Start()
    {
        if(menu)
        {
            on = GameObject.Find("Music on");//.GetComponent<UILabel>();
            off = GameObject.Find("Music off");//.GetComponent<UILabel>();
            if (on != null)
                on.SetActive(GameController.isSound);
            if(off != null)
                off.SetActive(!GameController.isSound);
        }

        sound.SetActive(GameController.isSound);

    }

    public void Reload()
    {
        Application.LoadLevel(0);
    }

    public void LoadLevel()
    {
        Application.LoadLevel(Application.loadedLevel + 1);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        Application.LoadLevel(0);
    }

    public void LoadWood()
    {
        if (PlayerPrefs.HasKey("WoodMiddle"))
        {
            //Application.LoadLevel("Wood");
            StartCoroutine(_Start("Wood"));
        }

    }

    public void LoadCave()
    {
        if (PlayerPrefs.HasKey("CaveMiddle"))
        {
            //Application.LoadLevel("Cave");
            StartCoroutine(_Start("Cave"));
        }

    }

    public void LoadWoodTutorial()
    {

        //Application.LoadLevel("WoodTutorial");
        StartCoroutine(_Start("WoodTutorial"));
    }

    public void LoadCaveTutorial()
    {
        StartCoroutine(_Start("CaveTutorial"));
        //Application.LoadLevel("CaveTutorial");
    }

    public void LoadWoodMiddle()
    {
        if (PlayerPrefs.HasKey("WoodTutorial"))
        {
            //Application.LoadLevel("WoodMiddle");
            StartCoroutine(_Start("WoodMiddle"));
        }

    }

    public void LoadCaveMiddle()
    {
        if (PlayerPrefs.HasKey("CaveTutorial"))
        {
            //Application.LoadLevel("CaveMiddle");
            StartCoroutine(_Start("CaveMiddle"));
        }
           

    }


    public void AudioChange()
    {
        GameController.isSound = !GameController.isSound;
        if (on != null && off != null)
        {
            on.SetActive(GameController.isSound);
            off.SetActive(!GameController.isSound);
            if (GameController.isSound)
            {
                musicButton.tweenTarget = on;
            }
            else
            {
                musicButton.tweenTarget = off;

            }

        }
        sound.SetActive(GameController.isSound);

    }

    public void Resume()
    {
        Time.timeScale = 1;
        cam.SetActive(false);
        eventT.SetActive(true);
    }


    private IEnumerator _Start(string levelName)
    {
        cam.SetActive(false);
        root.SetActive(true);
        AsyncOperation async = Application.LoadLevelAsync(levelName);
        while (!async.isDone)
        {
            percentLabel.text = string.Format("Loading {0}%", (int)Mathf.Clamp(async.progress * 100, 0f, 100f));
            yield return null;
        }

        yield return async;
    }
}
