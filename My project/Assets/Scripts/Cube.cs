using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour{

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

        if(this.transform.parent == null) {
            StartCoroutine(limitCheck());
        }


    }

    IEnumerator limitCheck() {
        yield return new WaitForSeconds(0.75f);
        inBox = true;
    }


    private void OnCollisionStay2D(Collision2D collision) {
        
        if (collision.collider.tag != "Cube")
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
