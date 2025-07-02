using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum PostType
{
    box1,
    box2,
    box3
}

public class Post : MonoBehaviour
{

    public PostType type; //自作した列挙型を扱う変数、タイプを決める
    bool posted; //配達済みかどうかフラグ

    public int getPoint = 50; //ポイント設定(初期値はbox1用の50)

    public TextMeshProUGUI pointText;

    public PointUI pointUI; //ポイントを表示するUIクラス

    public static int[] successCounts = { 0, 0, 0 }; //成功数のカウント



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!posted)
        {
            switch (type)
            {
                case PostType.box1:
                    if (other.gameObject.CompareTag("Box1")) PostComp(0);
                    break ;

                case PostType.box2:
                    if (other.gameObject.CompareTag("Box2")) PostComp(1);
                    break;

                case PostType.box3:
                    if (other.gameObject.CompareTag("Box3")) PostComp(2);
                    break;
            }

        } 
    }

    void PostComp(int boxNum)
    {
        posted = true;
        successCounts[boxNum]++;

        //エフェクトより少し上の位置を指定
        Vector3 showPos = transform.position + (Vector3.up * 1.5f) ;

        //表示対象オブジェクトのテキスト内容を設定したスコアと同じになるように
        pointText.text = "+" + getPoint.ToString() + "pt";

        //ポイントのUIの表示メソッドを発動
        pointUI.Show(showPos);
        GameController.stagePoints += getPoint; //ステージポイント加算

        
        //あたり判定から親のPostプレハブを丸ごと消す
        Destroy(transform.parent.gameObject,1.0f);





    }



}
