using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BashColliderCheck : MonoBehaviour{
    public int Dir;
    public bool isActivated;

    void Update(){
        if (isActivated || !GetComponentInParent<Cube>().inBox)
            return;

        StartCoroutine(Bash());

        
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject != GetComponentInParent<Transform>().gameObject) {
            Debug.Log(collision.transform.tag);
            isActivated = false;
            if(GetComponentInParent<Cube>().inBox)
                Destroy(this.gameObject, 0.1f);
        } 
    }

    IEnumerator Bash() {
        Vector3 forceDirection;

        switch (Dir) {
            case 1:
                forceDirection = GetComponentInParent<Transform>().TransformDirection(new Vector3(1, 0, 0));
                break;
            case 2:
                forceDirection = GetComponentInParent<Transform>().TransformDirection(new Vector3(-1, 0, 0));
                break;
            default:
                forceDirection = Vector3.zero;
                break;
        }

        GetComponentInParent<Rigidbody2D>().AddForce(forceDirection * 250, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.01f);
        isActivated = true;
    }
}
