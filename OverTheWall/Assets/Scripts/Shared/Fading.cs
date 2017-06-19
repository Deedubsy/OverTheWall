using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fading : MonoBehaviour {

    public Texture2D fadeOutTexture;
    public float fadeSpeed = 1.0f;

    private int drawDepth = -1000;
    private float alpha = 1.0f;
    private int fadeDir = -1;

    private void OnGUI()
    {
        //fade out/in the alpha value using a direction, a speed and time.deltatime to convert the operation into seconds
        alpha += fadeDir * fadeSpeed * Time.deltaTime;

        //force (clamp) the number between  0 and 1
        alpha = Mathf.Clamp01(alpha);

        //Set color of out GUI
        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);

        //make sure black texture will render on top
        GUI.depth = drawDepth;

        //draw texture to fit on screen
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture);
    }

    public float BeginFade(int direction)
    {
        fadeDir = direction;

        return fadeSpeed;
    }

    private void OnLevelWasLoaded(int level)
    {
        BeginFade(-1);
    }
}
