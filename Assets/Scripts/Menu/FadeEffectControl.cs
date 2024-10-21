using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeEffectControl : MonoBehaviour
{
    /*
    public float alphaFade;
    public bool increaseAlpha;
    public bool topAlpha;
    */
    // Start is called before the first frame update
    void Start()
    {
        /*
        alphaFade = 0.0f;
        increaseAlpha = false;
        topAlpha = false;
        */
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (increaseAlpha){
            alphaFade += 0.01f;
            alphaFade = Mathf.Clamp(alphaFade, 0.0f, 1.0f);
            if (alphaFade >= 1.0f)
            {
                alphaFade = 1.0f;
                increaseAlpha = false;
                topAlpha = true;
            }
        }
        else
        {
            alphaFade -= 0.01f;
            alphaFade = Mathf.Clamp(alphaFade, 0.0f, 1.0f);
        }

        GetComponent<RawImage>().color = new Color(0f, 0f, 0f, alphaFade);
        //Mathf.clamp
        */

    }

    /*

    public void settopAlpha(bool v)
    {
        topAlpha == v;
    }

    public bool gettopAlpha()
    {
        return topAlpha;
    }

    public void setincreaseAlpha(bool v)
    {
        increaseAlpha == v;
    }

    public bool getincreaseAlpha()
    {
        return increaseAlpha;
    }

    public void setalphaFade(float v)
    {
        alphaFade == v;
    }

    public bool getalphaFade()
    {
        return alphaFade;
    }
    */
}
