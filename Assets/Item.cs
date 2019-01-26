using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Item : MonoBehaviour
{
    [Tooltip("アイテム固有のGポイント倍率")]public float itemGP = 2;//アイテム固有のGpoint倍率

    int pointLife;
    int pointHappiness;
    int pointHome;

    public float firstspeed = 2f;
    public float force_amount = 2000;

    public GameMgr gameMgr;

    public GameObject star;

    Rigidbody2D rb;

    public bool isClicked= false;

    public int consumeGPoint;//このアイテムが現在消費させるGポイント算出値
    bool falledFlag = false;//すでに落下処理が完了しているかどうか

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameMgr = GameObject.Find("GameMgr").GetComponent<GameMgr>();
        star = GameObject.Find("Planet");
        //if (gameMgr == null) { gameMgr = GameObject.Find("GameMgr").GetComponent<GameMgr>(); }
        //if (star_transform == null) { gameMgr = GameObject.Find("Planet").GetComponent<GameMgr>(); }
    }

    // Update is called once per frame
    void Update()
    {
        CalcConsumeGPoint();

        FallSequence();



        //マウスカーソルが当たっていたら
        if (Input.GetKeyDown(KeyCode.Z)){
            Fall();

        }
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
        //マウスが押されていたら
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 aTapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Collider2D aCollider2d = Physics2D.OverlapPoint(aTapPoint);

            if (aCollider2d)
            {
                GameObject obj = aCollider2d.transform.gameObject;
                //マウスポインタがこのオブジェクトに触れていたら
                if (obj == this.gameObject)
                {
                    //Gポイントが消費量より上かつfallflagがfalse
                    if (CheckGPoint() && !falledFlag)
                    {
                        //落下を成立させる
                        Fall();
                    }
                }
            }
        }

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
