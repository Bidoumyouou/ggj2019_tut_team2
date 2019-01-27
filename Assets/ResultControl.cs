using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ResultWord {
    [Tooltip("この数値以上なら")]public int treshold;
    [Tooltip("この文字列になる")]public string str;
}


public class ResultControl : MonoBehaviour
{

    GameMgr gameMgr;
    public Image fadeTex;

    public GameObject player;

    public GameObject lifePoint;
    public GameObject happinesPoint;
    public GameObject homePoint;

    private bool fadeOut;
    private bool fadeIn;

    [SerializeField] public List<ResultWord> resultWordListLife;
    [SerializeField] public List<ResultWord> resultWordListHappiness;
    [SerializeField] public List<ResultWord> resultWordListHome;

    int result_state = 0;//0..フェードインする 1.プレイヤーを消す 2.フェードアウトする 3.. アニメーション開始 

    public string[] resultStr = new string[3];
    // Start is called before the first frame update
    void Start()
    {
        gameMgr = GameObject.Find("GameMgr").GetComponent<GameMgr>();
        //fadeIn = true;
    }

    // Update is called once per frame
    void CalcResultText()
    {
        foreach (ResultWord word in resultWordListLife)
        {
            if (word.treshold <= gameMgr.scoreLife)
            {
                resultStr[0] = word.str;
            }
        }
        foreach (ResultWord word in resultWordListHappiness)
        {
            if (word.treshold <= gameMgr.scoreHappiness)
            {
                resultStr[1] = word.str;
            }
        }
        foreach (ResultWord word in resultWordListHome)
        {
            if (word.treshold <= gameMgr.scoreHome)
            {
                resultStr[2] = word.str;
            }
        }
    }


    void Update()
    {
        //リザルトテキストの編集
        CalcResultText();

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
