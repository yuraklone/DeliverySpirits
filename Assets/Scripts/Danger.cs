using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Danger : MonoBehaviour
{
    public float dangerSpeed = 10.0f;


    // Start is called before the first frame update
    void Start()
    {
        //10秒後に消滅
        Destroy(gameObject, 10.0f);



    }

    // Update is called once per frame
    void Update()
    {
        //もしもゲームステータスがtimeoverなら
        if (GameController.gameState == GameState.timeover)
        {
            //コライダーをオフ
            GetComponent<BoxCollider>().enabled = false;
            //即消滅
            Destroy(gameObject);

           


        }

        //自分の座標を前方向に対してdengerSpeedの係数をかけながら増加
        //transform.position += new Vector3(0, 0, -dangerSpeed);
        transform.position += transform.forward * dangerSpeed * Time.deltaTime;



    }
}
