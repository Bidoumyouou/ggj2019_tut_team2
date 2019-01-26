using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    [HideInInspector]public int gPoint;//グラビティポイント
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
}
