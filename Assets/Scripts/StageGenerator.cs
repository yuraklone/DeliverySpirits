using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    const int StageChipSize = 50; //生成するチップの大きさの基準

    int currentChipIndex; //現在注目しているチップ

    Transform player; //プレイヤーのtransform情報
    public GameObject[] stageChips; //生成すべきオブジェクトを配列に格納
    public int startChipIndex; //チップ番号の開始
    public int preInstantiate; //余分に就く言っておくチップ数

    //現在生成したオブジェクトの管理用
    public List<GameObject> generatedStageList = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        //プレイヤーのtransform情報を取得
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentChipIndex = startChipIndex - 1;
        UpdateStage(preInstantiate);

    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            //キャラクターの位置から現在のステージチップの番号を割り出し
            int charaPositionIndex = (int)(player.position.z / StageChipSize);

            //次のステージチップに入ったらステージの更新処理を行う
            if (charaPositionIndex + preInstantiate > currentChipIndex)
            {
                UpdateStage(charaPositionIndex + preInstantiate);
            }
        }
    }

    //指定のIndexまでのステージチップを生成して、管理下に置く
    void UpdateStage(int toChipIndex)
    {
        if (toChipIndex <= currentChipIndex) return;

        //指定のステージチップまでを作成
        for (int i = currentChipIndex + 1; i<=toChipIndex; i++)
        {
            GameObject stageObject = GenerateStage(i);

            //生成したステージチップを管理リストに追加
            generatedStageList.Add(stageObject);
        }

        //ステージ保持上限内になるまで古いステージを削除
        while (generatedStageList.Count > preInstantiate + 2) DestroyOldestStage();

        currentChipIndex = toChipIndex;
    }

    //指定のインデックス位置にStageオブジェクトをランダムに生成
    GameObject GenerateStage(int chipIndex)
    {
        int nextStageChip = Random.Range(0, stageChips.Length);

        GameObject stageObject =
            Instantiate(stageChips[nextStageChip],
                        new Vector3(0, 0, chipIndex * StageChipSize),
                        Quaternion.identity );

        return stageObject;
    }

    void DestroyOldestStage()
    {
        GameObject oldStage = generatedStageList[0];
        generatedStageList.RemoveAt(0);
        Destroy(oldStage);


    }









}

