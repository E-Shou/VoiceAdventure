using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelectManager : MonoBehaviour {

    public GameObject[] stageButtons;   //ステージ選択ボタン配列

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    //ステージ選択ボタンを押した
    public void PushStageSelectButton(int stageNo)
    {
        SceneManager.LoadScene("GameScene" + stageNo);  //ゲームシーンへ
    }
}
