using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour {

    public RuntimeAnimatorController[] animCon;
    public CubeData[] datas;
    public int spriteType;
    public bool inBox;
    public bool sfxPlayed;
    public GameObject bashLeft;
    public GameObject bashRight;

    CubeData data;

    Rigidbody2D rigid;
    BoxCollider2D coll;
    Animator anim;

    private void Awake() {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        tag = "Cube";

    }


    private void Update() {
        if (inBox)
            return;

        if (this.transform.parent == null) {
            StartCoroutine(limitCheck());
        }

        switch (GameManager.instance.bashDir) {
            case -1:
                setBash(1, true);
                setBash(2, false);
                break;

            case 0:
                setBash(1, false);
                setBash(2, false);
                break;

            case 1:
                setBash(1, false);
                setBash(2, true);
                break;

        }
    }

    public void setBash(int Dir, bool isOn) {
        bashLeft.transform.localScale = new Vector3(coll.size.x, coll.size.x, coll.size.x);
        bashLeft.transform.localPosition = new Vector3(0, -(coll.size.x / 2), 0);
        //bashLeft.GetComponent<BoxCollider2D>().offset = new Vector2(0, coll.size.y / 4);
        bashLeft.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0.5f);
        //bashLeft.GetComponent<BoxCollider2D>().size = new Vector2(0.9f , coll.size.x / 2);
        bashLeft.GetComponent<BoxCollider2D>().size = new Vector2(1.1f, 1.1f);


        bashRight.transform.localScale = new Vector3(coll.size.x, coll.size.x, coll.size.x);
        bashRight.transform.localPosition = new Vector3(0, -(coll.size.x / 2), 0);
        //bashRight.GetComponent<BoxCollider2D>().offset = new Vector2(0, coll.size.y / 4);
        bashRight.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0.5f);
        //bashRight.GetComponent<BoxCollider2D>().size = new Vector2(0.9f, coll.size.x / 2);
        bashRight.GetComponent<BoxCollider2D>().size = new Vector2(1.1f, 1.1f);



        if (Dir == 1) {
            bashLeft.gameObject.SetActive(isOn);
        }

        if (Dir == 2) {
            bashRight.gameObject.SetActive(isOn);
        }

    }

    IEnumerator limitCheck() {
        yield return new WaitForSeconds(1f);
        inBox = true;
    }


    private void OnCollisionStay2D(Collision2D collision) {

        if (collision.collider.tag != "Cube" || collision.gameObject.GetComponent<Cube>().inBox == false)
            return;


        if(spriteType == collision.gameObject.GetComponent<Cube>().spriteType && transform.position.y < collision.transform.position.y) {
       
            gradeUP();

            Destroy(collision.gameObject);
        }
        else {
            playLandSfx();
        }
    }

    public void playLandSfx() {
        if (!sfxPlayed) {
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Land, 0);
            sfxPlayed = true;
        }
    }

    public void gradeUP() {
        spriteType++;
        GameManager.instance.score += datas[spriteType].mergedScore;
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Merged, 0);
        cubeShape();
        
    }

    public void cubeShape() {
        anim.runtimeAnimatorController = animCon[spriteType];
        coll.size = datas[spriteType].size;
        coll.offset = Vector2.zero;
    }   
}
