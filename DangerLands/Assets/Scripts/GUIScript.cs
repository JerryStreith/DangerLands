using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour
{
    //public Texture[] textures;
    public GameObject eventT;
    public GameObject cam;
    public Texture bonus;
    public Texture pause;
    public Font font;
    public Texture curTexture;
    int width = 90, height = 30;
    int w = Screen.width, h = Screen.height;

    public static GUIScript instance;
    int bonusValue = 0;
    const int plusBonus = 5;

    int xoffset = 5, yoffset = 5, lives = 3;
    int offsetBetweenHearts;

    void Start()
    {
        //cam.SetActive(false);
        //w = Screen.width; h = Screen.height;
        height = Mathf.RoundToInt(h / 100 * 8);
        offsetBetweenHearts = Mathf.RoundToInt(((float)w / 100.0f) / 2.0f);
        width = height;
        //width = 3 * height;
        instance = this;
        //curTexture = textures[2];
    }
    
    public void ChangeBonus()
    {
        bonusValue += plusBonus;
    }

    public void ChangeLife()
    {
        lives--;
        //width -= height;
        if (lives == 0)
        {
            //Time.timeScale = 0;
            Application.LoadLevel(1);
        }
        //else
        //    curTexture = textures[(width / height) - 1];
    }

    void OnGUI()
    {
        //life

        float x = xoffset, y = yoffset;
        for (int i = 0; i < lives; i++)
        {
            Rect rect = new Rect(x, y, width, height);
            GUI.DrawTexture(rect, curTexture, ScaleMode.ScaleToFit, true);
            x += width + offsetBetweenHearts; //y += height;
        }
        float padding = 1.2f;
        //cherepok
        Rect bonusRect = new Rect(w - (height + 5 + padding * height + 5), 5, height - 5, height - 5);
        GUI.DrawTexture(bonusRect, bonus, ScaleMode.ScaleToFit, true);

        //text
        string text = bonusValue.ToString();


        GUIStyle style = new GUIStyle();

        Rect fontRect = new Rect(w - (padding * height + 5), -4, height, height);
        style.fontSize = height;
        style.font = font;

        GUI.Label(fontRect, text, style);

        GUIStyle gSt = new GUIStyle(style);
        style.fontSize = height - 5;
        gSt.normal.textColor = Color.white;
        Rect sfontRect = new Rect(w - (padding * height + 4), -3, height - 1, height - 1);
        GUI.Label(sfontRect, text, gSt);

        Rect pauseRect = new Rect((float)w / 2.0f, 5, height + 5, height + 5);
        //GUI.DrawTexture(pauseRect, pause, ScaleMode.ScaleToFit, true);

        if (GUI.Button(pauseRect, pause))
        {
            Time.timeScale = 0;
            cam.SetActive(true);
            eventT.SetActive(false);
        }
    }
}
