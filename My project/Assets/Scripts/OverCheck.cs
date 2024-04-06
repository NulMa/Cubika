using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverCheck : MonoBehaviour{

    Collider2D coll;
    Rigidbody2D rigid;

    private void Awake() {
        coll = GetComponent<Collider2D>();
        rigid = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.tag == "Cube" && collision.GetComponent<Cube>().inBox) {
            GameManager.instance.gameOver();
        }
    }

}
