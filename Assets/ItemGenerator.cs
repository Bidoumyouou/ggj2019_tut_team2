﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global;

public class ItemGenerator : MonoBehaviour
{
    public GameMgr gameMgr;
    public GameObject itemCorePivot;

    public List<GameObject> itemPrefabList;

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
            if (gameMgr.item_num < maxItemNum)
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
        int m = Random.Range(0, itemPrefabList.Count);


        GameObject item = GameObject.Instantiate(itemPrefabList[m]);
        gameMgr.item_num++;


        item.transform.parent = pivot.transform;
        pivot.transform.parent = transform;
        //pivotの中心は惑星の中心

        pivot.transform.localPosition = Vector3.zero;
    }

}
