using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultControl : MonoBehaviour
{
    public Image fadeTex;

    public GameObject player;

    public GameObject lifePoint;
    public GameObject happinesPoint;
    public GameObject homePoint;

    private bool fadeOut;
    private bool fadeIn;


    // Start is called before the first frame update
    void Start()
    {
        fadeIn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeIn)
        {
            FadeIn();
            return;
        }
        else
        {
            ResultPreparation();
        }

        if (fadeOut)
        {
            FadeOut();
            return;
        }

    }

    private void ResultPreparation()
    {
        Destroy(player);

    }

    private void FadeIn()
    {
        float a = fadeTex.color.a;
        a += 0.01f;
        var alfa = new Color(0, 0, 0, a);
        fadeTex.color = alfa;
        if (a>1.0f)
        {
            fadeIn = false;
        }
    }

    private void FadeOut()
    {
        float a = fadeTex.color.a;
        a -= 0.01f;
        var alfa = new Color(0, 0, 0, a);
        fadeTex.color = alfa;
        if (a < 0.0f)
        {
            fadeOut = false;
        }
    }
}
