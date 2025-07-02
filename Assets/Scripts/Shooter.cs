using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject[] boxPrefabs; //宅配物のPrefabを格納
    Transform player; //プレイヤーの位置情報

    public static int boxNum; //配列の何番目のボックスが選択されているか
    public float shootSpeed = 100f; //投げた時の力
    public float upSpeed = 30f; //投げた時の上向きの力

    bool startShoot; //シュート可能かどうかのフラグ

    Camera cam; //カメラ情報

    public static int[] shootCounts = { 0, 0, 0 };


    // Start is called before the first frame update
    void Start()
    {
        Invoke("ShootEnabled", 0.5f);

        //プレイヤー情報の取得
        player = GameObject.FindGameObjectWithTag("Player").transform;

        //カメラ情報の取得
        cam = Camera.main; //MainCameraタグが付いているカメラ情報の取得
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.gameState != GameState.playing) return;

        if(Input.GetMouseButtonDown(0)) //0番指定で左クリック
        {
            if (startShoot) Shoot();
        }
        if (Input.GetMouseButtonDown(1)) //1番指定で右クリック 
        {
            boxNum++;
            if (boxPrefabs.Length == boxNum) boxNum = 0;
        }


    }

        void ShootEnabled()
    {
        startShoot = true; //シュート可能にする
    }

    void Shoot()
    {
        if(player != null)//プレイヤーが消滅していなければ
        {
            //配列の中からBOXを作成し、情報を取得
            GameObject box = Instantiate(boxPrefabs[boxNum], player.position,Quaternion.identity);

            //生成したBOXのRigidbodyを取得
            Rigidbody rbody = box.GetComponent<Rigidbody>();

            //生成したBOXをAddForceで射出
            rbody.AddForce(new Vector3
                            (
                             cam.transform.forward.x * shootSpeed, 
                             cam.transform.forward.y + upSpeed, 
                             cam.transform.forward.z * shootSpeed
                             ),ForceMode.Impulse);

            shootCounts[boxNum]++;




        }







    }




}
