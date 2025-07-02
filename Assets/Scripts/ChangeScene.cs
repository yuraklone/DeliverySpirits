using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneName;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Load()
    {
        //ポイントリセット
        GameController.stagePoints = 0;
        for(int i = 0; i < Post.successCounts.Length; i++)
        {
            Post.successCounts[i] = 0;
            Shooter.shootCounts[i] = 0;
        }

        //シーンの切替
        SceneManager.LoadScene(sceneName);
               
    }

}
