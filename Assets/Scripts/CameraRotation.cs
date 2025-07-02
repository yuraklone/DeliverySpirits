using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float mouseSensitivity = 3.0f; //マウス感度

    //上下の角度上限
    public float minVerticalAngle = -60.0f;
    public float maxVerticalAngle = 60.0f;

    //左右の角度上限
    public float minHorizontalAngle = -90.0f;
    public float maxHorizontalAngle = 90.0f;

    //プレイ中のカメラの角度
    float verticalRotation = 0;
    float horizontalRotation = 0;

    //プレイ開始時のカメラの角度の基準
    float initialY = 0;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //画面中心にカーソルをロック
        Cursor.visible = false; //カーソルを非表示

        Vector3 angles = transform.eulerAngles; //プレイ開始時のカメラ角度
        initialY = angles.y;
        horizontalRotation = 0f; //明確に初期の角度の計算値も0
        verticalRotation = angles.x; //カメラの初期角度(上下)を入れておく
    }

    // Update is called once per frame
    void Update()
    {
        //プレイング状態でなければ動かせないようにしておく
        if (GameController.gameState != GameState.playing) return;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        horizontalRotation += mouseX; //その時のマウスの動きに応じた数値を積み重ね
        //最大・最小での絞り込み
        horizontalRotation = Mathf.Clamp(horizontalRotation, minHorizontalAngle, maxHorizontalAngle);

        verticalRotation -= mouseY; //上記と同じこと意をY軸でも
        verticalRotation = Mathf.Clamp(verticalRotation, minVerticalAngle, maxVerticalAngle);

        float yRotation = initialY + horizontalRotation;

        //カメラの角度を決定する
        transform.rotation = Quaternion.Euler(verticalRotation,yRotation, 0);
    }
}
