using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public TimeController timeCnt; //TimeControllerスクリプト
    public TextMeshProUGUI timeText; //TimeTextオブジェクトについているTMPコンポーネントの情報

    public GameObject gameOverPanel; //ゲームオーバーUIを参照

    
    public TextMeshProUGUI pointText; //ポイントUIを取得

    int currentPoint; //UIが管理しているポイント

    public TextMeshProUGUI thisScoreText; //現在スコアのUI
    public TextMeshProUGUI highScoreText; //ハイスコアのUI
    public TextMeshProUGUI[] boxTexts; //ボックスの個別成績の文字列の配列




    //宅配BOXのUI関連
    public Image boxImage;
    public Sprite[] boxPics; //BOXの絵
    int currentBoxNum; //UIが把握している宅配BOX番号

    public string currentStageName; //ステージ名を入力

    public GameObject resultPanel;

    // Start is called before the first frame update
    void Start()
    {
        timeCnt = GetComponent<TimeController>();
        gameOverPanel.SetActive(false);
        resultPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //右クリックして選択したBOX番号がUIの把握しているBOX番号と違うなら
        if(currentBoxNum != Shooter.boxNum)
        {
            currentBoxNum = Shooter.boxNum;//最新のBOX番号に更新
            //UIが把握しているBOX番号に対応した絵を表示
            boxImage.sprite = boxPics[currentBoxNum];
        }

        if(GameController.gameState == GameState.playing)
        {
            //切り上げたdisplayTimeをStringに変換して差し替え
             timeText.text = Mathf.Ceil(timeCnt.displayTime).ToString();
            
            if (timeText.text == "0") 
            {
                //ゲームステータスをtimeoverに
                GameController.gameState = GameState.timeover;
                int highScore = PlayerPrefs.GetInt(currentStageName);
            
                if(GameController.stagePoints > highScore)
                {
                    highScore = GameController.stagePoints;
                    PlayerPrefs.SetInt(currentStageName, highScore);

                }

                //各スコアの表示
                thisScoreText.text = "This Time's Score : " + currentPoint.ToString();
                highScoreText.text = "High Score : " + highScore.ToString();
                
                //各ボックスの成功率表示
                for (int i = 0;i < boxTexts.Length; i++)
                {
                    float successRate;
                    if (Shooter.shootCounts[i] == 0) //分母が0の時は強制的に0
                    {
                        successRate = 0;
                    }
                    else
                    {
                        //success・shootともにintのため片方をfloatに変換しておく
                        successRate = ((float)Post.successCounts[i] / Shooter.shootCounts[i]) * 100f;
                    }

                    boxTexts[i].text = 
                        "Box" + (i+1) +":" 
                        + Post.successCounts[i] 
                        + "/" + Shooter.shootCounts[i] 
                        + "Success Rate" + successRate.ToString("F1")+"%";

                }

                resultPanel.SetActive(true);

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                GameController.gameState = GameState.end;

            }
        }

        

        if(GameController.gameState == GameState.gameover)
        {
            //ゲームオーバーパネルを出す
            gameOverPanel.SetActive(true);

            //カーソルの復活
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            //状態をendにする
            GameController.gameState = GameState.end;
        }

        






        //ステージポイントがUIの表示と違いが出たら
        if(currentPoint != GameController.stagePoints)
        {
            //UIの把握しているポイントに最新情報を反映
            currentPoint = GameController.stagePoints;
            pointText.text = currentPoint.ToString();
        }







    }
}
