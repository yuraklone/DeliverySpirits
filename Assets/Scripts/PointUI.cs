using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointUI : MonoBehaviour
{
    Camera cam; //カメラ情報を取得して連動
    public RectTransform uiTransform; //対象UIのRectTransform

    public Vector3 worldTarget; //出現させたいワールド座標

    public float displaytime = 1.0f; //出現時間
    public float floatUPSpeed = 0.5f; //上昇スピード

    float timer; //時間計測用
    bool isShowing; //表示中かどうかのフラグ


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main; //メインカメラのCameraコンポーネントを取得
    }

    // Update is called once per frame
    void Update()
    {
        if (isShowing)
        {
            //Y方向に少しずつ上昇
            worldTarget += Vector3.up * floatUPSpeed * Time.deltaTime;

            //ワールド座標→スクリーンUI座標に変換
            Vector3 screenPos = cam.WorldToScreenPoint(worldTarget);
            uiTransform.position = screenPos;

            timer -= Time.deltaTime;
            if(timer <= 0f)
            {
                uiTransform.gameObject.SetActive(false);
                isShowing = false;
            }


        }
    }

    public void Show(Vector3 worldPosition)
    {
        worldTarget = worldPosition; //Showメソッドを発動するエフェクトの座標
        timer = displaytime; //何秒出すかの値をリセット
        isShowing = true; //表示の始まりのフラグ
        uiTransform.gameObject.SetActive (true);　//対象UIを表示に
    }



}
