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

    int result_state = 0;//0..フェードインする 1.プレイヤーを消す 2.フェードアウトする 3.. アニメーション開始 
    // Start is called before the first frame update
    void Start()
    {
        //fadeIn = true;
    }

    // Update is called once per frame



    void Update()
    {
        //phadeインへの遷移
        if(GameMgr.state == GameState.result)
        {
            switch(result_state){
                case 0:FadeIn();  break;
                case 1:ResultPreparation(); break;
                case 2:FadeOut();break;
                case 3:DoText();break;
            }

        }
        
    }

    private void ResultPreparation()
    {
        Destroy(player);
        fadeOut = true;
        result_state = 2;
    }

    private void FadeIn()
    {
        float a = fadeTex.color.a;
        a += 0.01f;
        var alfa = new Color(0, 0, 0, a);
        fadeTex.color = alfa;
        if (a>1.0f)
        {
            result_state = 1;
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
            result_state = 3 ;
        }
    }

    void DoText()
    {
        Debug.Log("Result Text is Apper");
    }
}
