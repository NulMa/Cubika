using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BashColliderCheck : MonoBehaviour
{
    public int Dir;
    public bool isActivated;

    // Update is called once per frame

    void Update(){
        if (isActivated || !GetComponentInParent<Cube>().inBox)
            return;

        StartCoroutine(Bash());

        
    }


    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject != GetComponentInParent<Transform>().gameObject) {
            isActivated = false;
            if(GetComponentInParent<Cube>().inBox)
                Destroy(this.gameObject, 0.1f);
        } 
    }

    


    IEnumerator Bash() {

            switch (Dir) {
            case 1:
                GetComponentInParent<Rigidbody2D>().AddForce(new Vector3(250, 0, 0), ForceMode2D.Impulse);
                break;

            case 2:
                GetComponentInParent<Rigidbody2D>().AddForce(new Vector3(-250, 0, 0), ForceMode2D.Impulse);
                break;
        }
        yield return new WaitForSeconds(0.01f);
        isActivated = true;
    }

}
