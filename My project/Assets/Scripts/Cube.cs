using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour{

    public RuntimeAnimatorController[] animCon;
    public CubeData[] datas;
    public int spriteType;
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
        

    }


    public void Init() {

    }


    private void OnCollisionStay2D(Collision2D collision) {
        
        if (collision.collider.tag != "Cube")
            return;


        if(spriteType == collision.gameObject.GetComponent<Cube>().spriteType && transform.position.y < collision.transform.position.y) {

            // delete upper
            gradeUP();

            Destroy(collision.gameObject);
        }
    }

    public void gradeUP() {
        spriteType++;

        anim.runtimeAnimatorController = animCon[spriteType];
        coll.size = datas[spriteType].size;
        coll.offset = new Vector2(0,  datas[spriteType].size.x / 2);
        
    }
}
