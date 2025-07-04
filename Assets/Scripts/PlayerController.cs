using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    const int MinLane = -1;
    const int MaxLane = 1;
    const float LaneWidth= 3.0f;

    CharacterController controller;
    //Animator animator;

    Vector3 moveDirection = Vector3.zero;
    int targetLane;

    public float gravity;
    public float speedZ;
    public float speedX;
    public float speedJump;
    public float accelerationZ;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        //animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        //ゲームステータスがplayingの時のみ左右に動かせる
        if (GameController.gameState == GameState.playing)
        {
            if (Input.GetKeyDown(KeyCode.A)) MoveToLeft();
            if (Input.GetKeyDown(KeyCode.D)) MoveToRight();
        }

        //徐々に加速しZ方向に常に前進
        float acceleratedZ = moveDirection.z + (accelerationZ + Time.deltaTime);
        moveDirection.z = Mathf.Clamp(accelerationZ,0,speedZ);
        //Mathf.Clamp(値、最小、最大)

        //X方向は目標のポジションまでの差分の割合で速度を計算
        float ratioX = (targetLane * LaneWidth - transform.position.x) / LaneWidth;
        moveDirection.x = ratioX * speedX;

        //重力分の力をフレーム単位で追加
        moveDirection.y -= gravity * Time.deltaTime;

        //ここまで決めたxyzの値で移動実行
        Vector3 globalDirection = transform.TransformDirection(moveDirection);
        controller.Move(globalDirection * Time.deltaTime);　
        //前方向だけならcontroller.Move(globalDirection * Time.deltaTime);でもOK


        //地面についていたらY方向の速度はリセット
        if (controller.isGrounded) moveDirection.y = 0;

        //animator.SetBool("run", moveDirection.z > 0.0f);


    }

    public void MoveToLeft() 
    {
        if (controller.isGrounded && targetLane > MinLane) targetLane--;
    }
    public void MoveToRight() 
    {
        if (controller.isGrounded && targetLane < MaxLane) targetLane++;
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Danger"))
        {
            controller.Move(new Vector3(0, 5, 0));　//跳ね上げる
            controller.transform.Rotate(Random.Range(0,90), Random.Range(0,90), Random.Range(-90,90)); //回転させる
            GameController.gameState = GameState.gameover;
            Destroy(gameObject,3.0f);



        }    

    }











}
