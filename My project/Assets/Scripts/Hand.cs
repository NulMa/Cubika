using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hand : MonoBehaviour{

    public Vector2 inputVec;
    public float speed;
    public GameObject inHand;
    public GameObject cubePref;
    public GameObject nextCubeGen;
    public bool isHold;
    public float rotSpeed;

    Rigidbody2D rigid;
    

    private void Awake() {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        speed = GameManager.instance.speed;

        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        float rotAmount = inputVec.y * rotSpeed * Time.fixedDeltaTime;

        if(inHand != null)
            inHand.transform.Rotate(Vector3.forward, rotAmount);



        if (isHold) {
            if (inHand == null)
                return;

            inHand.transform.localPosition = new Vector3(0, -0.5f, 0);
            inHand.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
        }
        else {
            StartCoroutine(cubeRegen());
        }


    }

    IEnumerator cubeRegen() {
        
        inHand.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        inHand.transform.SetParent(null);
        inHand = null;
        isHold = true;
        yield return new WaitForSeconds(0.5f);
        GameObject cube = Instantiate(cubePref, transform.position + new Vector3(0, -0.5f, 0), Quaternion.identity);
        cube.GetComponent<Cube>().spriteType = nextCubeGen.GetComponent<NextCubeCtrl>().img2Int;
        cube.GetComponent<Cube>().cubeShape();
        cube.transform.SetParent(transform);
        inHand = cube;
        nextCubeGen.GetComponent<NextCubeCtrl>().nextGen();
    }

    void OnMove(InputValue val) {
        inputVec = val.Get<Vector2>();
    }

    void OnFire(InputValue val) {
        if (!isHold)
            return;
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Drop, 0);
        isHold = false;
    }

    void OnBash(InputValue val) {
        GameManager.instance.bashDir += val.Get<float>();
    }

    void OnReset(InputValue val) {
        inHand.transform.eulerAngles = Vector3.zero;
    }
}