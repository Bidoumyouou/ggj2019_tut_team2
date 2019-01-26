using UnityEngine;
using UnityEditor;

//特定のゲームシステムと関係のないクラスのみ許容する。特定のゲーム依存の者はGameManagerへどうぞ
namespace Global


    //タイマーを「登録」して複数管理する
{    public class AutoTimer {
        public float time;
        public void Update()
        {
            time += Time.deltaTime;
        }
        //特定の周期が来たらtimeを再び0にする
        public bool CheckFrequene(float _frequenxy)
        {
            if (time >= _frequenxy)
                return true;
            return false;
        }
        //周期的タイマーリセットを実行するメソッド
        public bool DoSequence(float _frequenxy)
        {
            if (CheckFrequene(_frequenxy))
            {
                time = 0;
                return true;
            }
            return false;

        }
    }



    public static class Common : System.Object
    {
        //public static Leader Player;//今このソフトが操作しているプレイヤー

        public static Vector2 cursol_pos;

        public static bool isHost = false;
    }


}