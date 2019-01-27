using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global;

public class ItemGenerator : MonoBehaviour
{
    public GameMgr gameMgr;
    public GameObject itemCorePivot;

    public List<GameObject> itemPrefabList;
    public GameObject startItemPrefab;

	public List<Item> FloatingItemList { get; private set; } = new List<Item>();

    float time;
    public float frequency;

    public int maxItemNum;

    // Start is called before the first frame update
    void Start()
    {
         //tiemr = new AutoTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Generate();
        }


        if(time > frequency)
        {
            time = 0;
            if (FloatingItemList.Count < maxItemNum)
            {
                Generate();

            }

        }

        time += Time.deltaTime;
    }

    

    void Generate()
    {
        //generatepivotとアイテムプレハブを生成して接着する
        GameObject pivot = GameObject.Instantiate(itemCorePivot);
        //アイテムをランダムに生成して接着する

        GameObject item;
        //スタートオブジェクトの設置
        if(GameMgr.state == GameState.title && FloatingItemList.Count == 0) {
            pivot.transform.eulerAngles = new Vector3(0, 0, 0);
            item = GameObject.Instantiate(startItemPrefab);
            FloatingItemList.Add(item.GetComponent<Item>());
            item.transform.parent = pivot.transform;

        }
        else if(GameMgr.state == GameState.maingame)
        //そうでないオブジェクトの設置
        {
            int m = Random.Range(0, itemPrefabList.Count);
            item = GameObject.Instantiate(itemPrefabList[m]);
            FloatingItemList.Add(item.GetComponent<Item>());
            item.transform.parent = pivot.transform;

        }




        pivot.transform.parent = transform;
        //pivotの中心は惑星の中心

        pivot.transform.localPosition = Vector3.zero;
    }

}
