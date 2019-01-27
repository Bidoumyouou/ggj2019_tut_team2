using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState{
    title = 0,
    maingame = 1,
    result = 2
}


public class GameMgr : MonoBehaviour
{
    public int scoreLife;
    public int scoreHappiness;
    public int scoreHome;
	
	public GravityGauge gravityGauge;

    public static GameState state = GameState.title;

    [Tooltip("初期生成角度最小値")] public float startDegreeMin = 50;
    [Tooltip("初期生成角度最大値")] public float startDegreeMax = 60;
    [Tooltip("初期生成角度最大値")] public float endDegree = -50;


    public void ChangeGameMode(GameState _state)
    {
        state = _state;
    }

    //グラビティポイント
    [HideInInspector]
	public int gPoint
	{
		get { return gravityPoint_; }
		set
		{
			gravityPoint_ = value;
			if( gravityPoint_ <= 0 )
			{
				gravityPoint_ = 0;
				// todo last shot
			}
			if( gravityGauge != null )
			{
				gravityGauge.SetGravityPoint(gravityPoint_);
			}
		}
	}
	int gravityPoint_;

	public int startGPoint = 10000;
    [Tooltip("距離計算のための距離補正値")]public float correctionDistance;//距離計算のための距離補正値
    [Tooltip("Gポイント倍率")] public float GPointRate  = 50; //

    // Start is called before the first frame update
    void Start()
    {
        gPoint = startGPoint;
    }
    //gogogogo

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CountScore(GameObject _itemobj)
    {
        Item item = _itemobj.GetComponent<Item>();

        scoreLife += item.pointLife;
        scoreHappiness += item.pointHappiness;
        scoreHome += item.pointHome;
        //
    }
}
