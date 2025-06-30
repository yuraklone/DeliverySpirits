using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerGenerator : MonoBehaviour
{
    const float laneWidth = 3.0f; //レーン幅

    public GameObject dangerPrefab; //生成される対向車

    public bool isRandom; //インターバルをランダムにするかどうか
    public float intervalTime = 10.0f; //インターバルをランダムにしなかった場合
    public float minIntervalTime = 5.0f;
    public float maxIntervalTime = 15.0f;

    float timer; //時間経過を観測
    float posX; //対向車の出現X座標

    public GameObject dangerPanel; //DangerのUIをコントロールする用

    // Start is called before the first frame update
    void Start()
    {
        timer = intervalTime; //一定間隔の場合の時間の代入
        if (isRandom)
        {
            timer = Random.Range(minIntervalTime, maxIntervalTime + 1);
        }



    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.gameState != GameState.playing) return; //ステータスがplayingでなければ何もしない

        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            DangerCreated();

            timer = intervalTime;
            if (isRandom) 
            {
                timer = Random.Range(minIntervalTime, maxIntervalTime + 1);
            }

            
        }
    }

    void DangerCreated()
    {
        int rand = Random.Range(-1,2 );
        posX = rand * laneWidth; //レーン番号とレーン幅で座標を決める

        //プレハブ化した対向車をそもままのzの位置にむきも祖＾のままに生成する「
        Instantiate(dangerPrefab,new Vector3( posX,1,transform.position.z), dangerPrefab.transform.rotation);

        //コルーチンの発動
        StartCoroutine(AlertText()); //引数でコルーチンを指定



    }

    //コルーチンの作成
    IEnumerator AlertText()
    {
        float duration = 3.0f; //点滅持続時間
        float blinkInterval = 0.05f; //点滅時間
        float blinkTimer = 0f; //点滅時間のカウントダウン

        while (blinkTimer < duration)
        {
            dangerPanel.SetActive(!dangerPanel.activeSelf);
            yield return new WaitForSeconds(blinkInterval); //ウェイト処理
            blinkTimer += blinkInterval;
        }

        dangerPanel.SetActive(false);
        
    }



}
