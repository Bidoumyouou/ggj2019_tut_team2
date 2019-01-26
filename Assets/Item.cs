using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Item : MonoBehaviour
{
    [Tooltip("アイテム固有のGポイント倍率")] public float itemGP = 2;//アイテム固有のGpoint倍率



    public int pointLife;
    public int pointHappiness;
    public int pointHome;

    public float firstspeed = 2f;
    public float force_amount = 2000;

    [Tooltip("回転角/1フレーム")] public float rad = 1;

    public GameMgr gameMgr;

    public GameObject star;

    Rigidbody2D rb;

    public bool isTouched = false;

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

        basePosY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {

        CalcConsumeGPoint();

        FallSequence();



        //マウスカーソルが当たっていたら
        if (Input.GetKeyDown(KeyCode.Z)) {
            Fall();

        }
    }

    private void FixedUpdate()
    {
        //スイングバイ
        RotatePivot();
        //ゆらゆら
        Waveing();
    }


    void CalcConsumeGPoint()
    {
        //距離の算出
        float distanceFromCore; // アイテムの物理距離()コアとの
        float distanceFromSurface; //表面との物理距離
        float coreRedious = star.GetComponent<SpriteRenderer>().bounds.size.x / 2;  //惑星の半径


        float distance_param; //補正値込みのスコア算出素点

        distanceFromCore = Vector2.Distance(star.transform.position, transform.position);
        distanceFromSurface = distanceFromCore - coreRedious;

        distance_param = distanceFromSurface + gameMgr.correctionDistance;

        //計算式: kyori素点 * 50 * ItemGP
        consumeGPoint = (int)Mathf.Floor(distance_param * gameMgr.GPointRate * itemGP);
    }

    void FallSequence()
    {
        //マウスのオーバーレイを検出する
        isTouched = MouseSequence();


        //マウスが押されていたら
        if (Input.GetMouseButtonDown(0))
        {
            //Gポイントが消費量より上かつfallflagがfalse
            if (isTouched && !falledFlag)
            {
                //落下を成立させる
                Fall();
            }


        }

    }

    void RotatePivot()
    {
        if (pivotTransform)
        {
            pivotTransform.Rotate(new Vector3(0, 0, rad));
        }
    }

    void Waveing() {

        //transform.position = new Vector3(transform.position.x, basePosY + Mathf.Sin(t) , transform.position.z);
        //transform.Translate(0, 2 * Mathf.Acos(t), 0);
        //pos.x += Mathf.Sin(Time.time * speed) * 4f;
        float y = transform.localPosition.y;

        y += Mathf.Cos(Time.time * frequency) * amplitude;

        transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
    }

    bool MouseSequence()
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


    void Fall(){
        //
        //Gポイントを消費する
        gameMgr.gPoint -= consumeGPoint;
        //デバッグ
        Debug.Log("I consumed " + consumeGPoint + " point and remain G is " + gameMgr.gPoint);


        Vector3 diff = (this.star.transform.position - this.transform.position).normalized;
        this.transform.rotation = Quaternion.FromToRotation(Vector3.up, diff);

        

        //初期速度の設定(差をとって正規化)
        Vector2 vec_sub = (Vector2)(transform.position - star.transform.position);
        vec_sub.Normalize();

        //落とす
        rb.velocity = -vec_sub * firstspeed;
        //
        rb.AddForce( -vec_sub * force_amount);
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Planet")
        {
            //アイテムの移動を禁止する
            rb.constraints =  RigidbodyConstraints2D.FreezeAll;
            //fallflag on
            falledFlag = true;
        }
    }

}
