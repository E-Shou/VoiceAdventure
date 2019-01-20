using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public GameObject textGameOver;
    public GameObject textClear;
    public GameObject player;

    public enum GAME_MODE
    {
        PLAY,
        CLEAR,
    };

    public GAME_MODE gameMode = GAME_MODE.PLAY;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GameNext2()
    {
        SceneManager.LoadScene("GameScene2");
    }

    public void GameNext3()
    {
        SceneManager.LoadScene("GameScene3");
    }

    public void GameClear()
    {
        gameMode = GAME_MODE.CLEAR;
        textClear.SetActive(true);
        Invoke("BackTitle", 2.0f);
    }

    public void GameOver()
    {
        textGameOver.SetActive(true);
        player.transform.position = (Vector2.zero);
        Invoke("BackTitle", 2.0f);
    }

    public void BackTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
