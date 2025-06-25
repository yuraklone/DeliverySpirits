using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    Vector3 diff; //ターゲットとの距離の差
    GameObject player; //プレイヤー情報

    public float followSpeed; //カメラの補間スピード

    //カメラの初期位置
    public Vector3 defaultPos = new Vector3(0, 5, -8);
    public Vector3 defaultRotate = new Vector3(12, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        //カメラを変数で決めた初期位置・角度にする
        transform.position = defaultPos;
        transform.rotation = Quaternion.Euler(defaultRotate);
        //Quaternion型へ変換しなければならない

        //プレイヤー情報の取得
        player = GameObject.FindGameObjectWithTag("Player");

        //プレイヤーとカメラの距離感を記憶しておく
        diff = player.transform.position - transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        if(player != null)
        {
            //線形補間を使って、カメラを目的の場所に動かす
            //Lerp(今の位置、ゴールとすべき位置、割合)
            transform.position = 
                Vector3.Lerp(transform.position,player.transform.position - diff,followSpeed * Time.deltaTime);
        }
    }
}
