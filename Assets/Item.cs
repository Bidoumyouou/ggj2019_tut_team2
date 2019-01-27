using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Item : MonoBehaviour
{
    [Tooltip("アイテム固有のGポイント倍率")] public float itemGP = 2;//アイテム固有のGpoint倍率

    public Vector3 initialPos = new Vector3(0,8,0);

    public bool isStartItem = false;

	public Sprite afterImage;

    public int pointLife;
    public int pointHappiness;
    public int pointHome;

    public float firstspeed = 2f;
    public float force_amount = 2000;

	public SoundManager.FallSEType fallSE;

	[Tooltip("回転角/1フレーム")]public float rad = -0.2f;

    public GameMgr gameMgr;

    public GameObject star;

    Rigidbody2D rb;

    public bool isTouched = false;

    bool isFalling = false;

    public int consumeGPoint;//このアイテムが現在消費させるGポイント算出値
    bool falledFlag = false;//すでに落下処理が完了しているかどうか

    Transform pivotTransform;

    float basePosY;
    [Tooltip("ゆらゆらの周波数")]public float frequency = 10;
    [Tooltip("ゆらゆらの振幅")] public float amplitude = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameMgr = GameObject.Find("GameMgr").GetComponent<GameMgr>();
        star = GameObject.Find("Planet");

        //ピボットのトランスフォームを取得する
        if (transform.parent)
        {
            pivotTransform = transform.parent.transform;
        }

        if (!isStartItem)
        {
            //座標系のinit

            transform.position = initialPos;
            //初期回転角

            pivotTransform.Rotate(0, 0, Random.Range(gameMgr.startDegreeMin, gameMgr.startDegreeMax));
            //transform.rotation = new Quaternion(0, 0, Random.Range(gameMgr.startDegreeMin, gameMgr.startDegreeMax), transform.rotation.w);


            basePosY = transform.position.y;
        }
        else
        {
            //座標系のinit

            transform.position = initialPos;
            //初期回転角

            pivotTransform.Rotate(0, 0, 0);
            //transform.rotation = new Quaternion(0, 0, Random.Range(gameMgr.startDegreeMin, gameMgr.startDegreeMax), transform.rotation.w);


            basePosY = transform.position.y;

        }
    }

    // Update is called once per frame
    void Update()
    {

        CalcConsumeGPoint();
		
        DeleteSequence();


    }

    private void FixedUpdate()
    {
        if (!isStartItem)
        {
            //スイングバイ
            RotatePivot();
            //ゆらゆら
            Waveing();
        }
        else
        {
            StartItemBehave();
        }
    }

    void StartItemBehave()
    {
        //
        if (!isFalling && !falledFlag)
        {
            float y = transform.localPosition.y;

            y += Mathf.Cos(Time.time * frequency) * amplitude;

            transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
        }
    }

    void CalcConsumeGPoint()
    {
        //距離の算出
        float distanceFromCore; // アイテムの物理距離()コアとの
        float distanceFromSurface; //表面との物理距離
        float coreRedious = star.GetComponent<Collider2D>().bounds.size.x / 2;  //惑星の半径


        float distance_param; //補正値込みのスコア算出素点

        distanceFromCore = Vector2.Distance(star.transform.position, transform.position);
        distanceFromSurface = distanceFromCore - coreRedious;

        distance_param = distanceFromSurface + gameMgr.correctionDistance;

        //計算式: kyori素点 * 50 * ItemGP
        consumeGPoint = (int)Mathf.Floor(distance_param * gameMgr.GPointRate * itemGP);
    }

    void DeleteSequence()
    {
        if(transform.position.x > 10)
        {
			GetComponentInParent<ItemGenerator>().FloatingItemList.Remove(this);
            GameObject.Destroy(transform.parent.gameObject);
            GameObject.Destroy(this.gameObject);
        }
    }

    void RotatePivot()
    {
        if (!isFalling && !falledFlag)
        {
            if (pivotTransform)
            {
                pivotTransform.Rotate(new Vector3(0, 0, rad));
            }
        }
    }

    void Waveing() {
        if (!isFalling && !falledFlag)
        {
            //transform.position = new Vector3(transform.position.x, basePosY + Mathf.Sin(t) , transform.position.z);
            //transform.Translate(0, 2 * Mathf.Acos(t), 0);
            //pos.x += Mathf.Sin(Time.time * speed) * 4f;
            float x = transform.localPosition.x;

            x += Mathf.Cos(Time.time * frequency) * amplitude;

            transform.localPosition = new Vector3(x, transform.localPosition.y, transform.localPosition.z);
        }
    }

    public bool IsOnMouse()
    {
        Vector3 aTapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Collider2D aCollider2d = Physics2D.OverlapPoint(aTapPoint);

        if (aCollider2d)
        {
            GameObject obj = aCollider2d.transform.gameObject;
            //マウスポインタがこのオブジェクトに触れていたら
            if (obj == this.gameObject)
            {
                return true;
            }
        }
        return false;
    }
    //Gポイントが要件を満たしているかを判定する
    bool CheckGPoint()
    {
        if(gameMgr.gPoint >= consumeGPoint)
        {
            return true;
        }
        return false;
    }


    public void Fall()
	{
        //
        //Gポイントを消費する
        gameMgr.gPoint -= consumeGPoint;
        //もしスタートアイテムだったらゲームを開始する
        if (isStartItem)
        {
            gameMgr.ChangeGameMode(GameState.maingame);
        }


        //Vector3 diff = (this.star.transform.position - this.transform.position).normalized;
        //this.transform.rotation = Quaternion.FromToRotation(Vector3.up, diff);



        //初期速度の設定(差をとって正規化)
        Vector2 vec_sub = (Vector2)(transform.position - star.transform.position);
        vec_sub.Normalize();

        //落とす
        rb.velocity = -vec_sub * firstspeed;
        //
        rb.AddForce( -vec_sub * force_amount);

		GetComponentInParent<ItemGenerator>().FloatingItemList.Remove(this);

		isFalling = true;
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Planet")
        {
            //アイテムの移動を禁止する
            rb.constraints =  RigidbodyConstraints2D.FreezeAll;
            //fallflag on
            falledFlag = true;

			GameContext.SoundManager.PlayFall(this);
			gameMgr.OnItemFall(this);

			Vector2 vec_sub = (Vector2)(transform.position - star.transform.position);
			vec_sub.Normalize();
			float power = 0.0001f * consumeGPoint;
			AnimManager.AddShakeAnim(GameContext.MainCamera, vec_sub, power * 6, 20 * power, 0.05f, ParamType.Position);

			if( afterImage != null )
			{
				GetComponent<SpriteRenderer>().sprite = afterImage;
			}
		}
    }

}
