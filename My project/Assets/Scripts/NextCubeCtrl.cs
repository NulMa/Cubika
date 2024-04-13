using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextCubeCtrl : MonoBehaviour{

    public Image img1;
    public Image img2;
    public int img1Int;
    public int img2Int;

    public Sprite[] sprites;

    private void Awake() {
        img1Int = Random.Range(0, 4);
        img1.sprite = sprites[img1Int];
        img2Int = Random.Range(0, 2);
        img2.sprite = sprites[img2Int];
    }


    private void Update() {
        img1.sprite = sprites[img1Int];
        img2.sprite = sprites[img2Int];
    }

    public void nextGen() {
        img2Int = img1Int;
        img1Int = Random.Range(0, 4);
    }

}
