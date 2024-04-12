using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour{
    public static GameManager instance;

    public Transform hand;
    public Transform box;
    public GameObject overUi;


    public int score;
    public int record;
    public float bashDir;
    public float time;
    public float speed;

    public Text nowScore;
    public Text recordScore;
    public Text playTime;
    public Text version;
    public Slider speedSlider;


    public int nextCube;


    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }

        instance = this;


        time = 0;
        score = 0;

        if (!PlayerPrefs.HasKey("Record")) {
            PlayerPrefs.SetInt("Record", 0);
        }

        recordScore.text = string.Format("{0:#}", PlayerPrefs.GetInt("Record"));
        version.text =  "ver. " + Application.version;
    }

    private void Update() {

        speed = speedSlider.value * 10;
        time += Time.deltaTime;
        int min = Mathf.FloorToInt(time / 60);
        int sec = Mathf.FloorToInt(time % 60);
        playTime.text = string.Format("{0:D2}:{1:D2}", min, sec);

        nowScore.text = string.Format("{0:#}", score);


        if (bashDir > 1)
            bashDir = 1;
        if (bashDir < -1)
            bashDir = -1;

    }

    public void sceneReload() {
        if (PlayerPrefs.GetInt("Record") < score) {
            PlayerPrefs.SetInt("Record", score);
        }
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void gameOver() {
        StartCoroutine(timeStop());
    }

    IEnumerator timeStop() {

        overUi.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        Time.timeScale = 0;
    }

    public void gameExit() {
        Application.Quit();
    }
}
