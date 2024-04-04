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


    // Start is called before the first frame update
    void Start(){
        img1Int = Random.Range(0, 3);
        img1.sprite = sprites[img1Int];
        img2Int = Random.Range(0, 2);
        img2.sprite = sprites[img2Int];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
