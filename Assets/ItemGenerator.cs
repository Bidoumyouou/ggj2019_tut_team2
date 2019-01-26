using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    public GameMgr gameMgr;
    public GameObject itemCorePivot;

    public List<GameObject> itemPrefabList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Generate();
        }
    }

    void Generate()
    {
        //generatepivotとアイテムプレハブを生成して接着する
        GameObject pivot = GameObject.Instantiate(itemCorePivot);
        //アイテムをランダムに生成して接着する
        int m = Random.Range(0, itemPrefabList.Count);


        GameObject item = GameObject.Instantiate(itemPrefabList[m]);

        item.transform.parent = pivot.transform;
        pivot.transform.parent = transform;
        //pivotの中心は惑星の中心

        pivot.transform.localPosition = Vector3.zero;
    }

}
