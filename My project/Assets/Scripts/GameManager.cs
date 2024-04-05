using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour{
    public static GameManager instance;

    public Transform hand;
    public Transform box;


    public int score;
    public int record;
    public float time;
    public Text nowScore;
    public Text recordScore;
    public Text playTime;


    public int nextCube;


    private void Awake() {
        time = 0;
        score = 0;
        
    }

    private void Update() {

        time += Time.deltaTime;
        int min = Mathf.FloorToInt(time / 60);
        int sec = Mathf.FloorToInt(time % 60);
        playTime.text = string.Format("{0:D2}:{1:D2}", min, sec);

        nowScore.text = string.Format("{0:D9}", score);
    }

    public void sceneReload() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
