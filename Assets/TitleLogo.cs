using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class TitleLogo : MonoBehaviour
{
    Image image;
    public float phade_speed;
    // Use this for initialization
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        //フェードアウトする
        if(GameMgr.state != GameState.title)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - phade_speed);
        }
    }
}
