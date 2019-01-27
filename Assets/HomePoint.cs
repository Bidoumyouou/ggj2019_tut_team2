using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomePoint : MonoBehaviour
{
    public Text Text;

    private GameObject GameManager;

    private int TextCurrentGravity;

    // Start is called before the first frame update
    void Start()
    {
        Text = GetComponentInChildren<Text>();

        GameManager = GameObject.Find("GameMgr");
        TextCurrentGravity = GameManager.GetComponent<GameMgr>().scoreHome;
    }

    // Update is called once per frame
    void Update()
    {
        Text.text = TextCurrentGravity.ToString();

        int nowPoint = GameManager.GetComponent<GameMgr>().scoreHome;
        if (nowPoint==TextCurrentGravity)
        {
            return;
        }
        if (TextCurrentGravity < nowPoint)
        {
            TextCurrentGravity += 10;
            if (TextCurrentGravity > nowPoint)
            {
                TextCurrentGravity = nowPoint;
            }
        }
        else
        {
            TextCurrentGravity -= 10;
            if (TextCurrentGravity < nowPoint)
            {
                TextCurrentGravity = nowPoint;
            }
        }

    }
}
