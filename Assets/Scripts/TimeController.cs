using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public float startTime = 30.0f; //カウントダウンの基準

    public float displayTime; //UIと連動する予定の残時間

    float pastTime; //経過時間
    bool isTimeOver; //カウントが0になったかどうか

    // Start is called before the first frame update
    void Start()
    {
        //まずは基準時間をセット
        displayTime = startTime;
    }

    // Update is called once per frame
    void Update()
    {
        //カウントがゼロなら何もしない　※ゲームクリア状態にある
        if (isTimeOver == true) return;
        

        //経過時間の算出
        pastTime += Time.deltaTime;
        //残時間の算出
        displayTime = startTime - pastTime;

        //残時間が0になったら何をするか
        if (displayTime <= 0)
        {
            displayTime = 0;
            //GameController.gameState = GameState.timeover;
            isTimeOver = true;



        }


    }
}
