using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideLine : MonoBehaviour{

    void Update(){
        transform.position = new Vector3(GetComponentInParent<Transform>().position.x, 1.1f, 0);

        if (GetComponentInParent<Hand>().inHand == null)
            return;

        switch (GetComponentInParent<Hand>().inHand.GetComponent<Cube>().spriteType) {
            case 0:
                transform.localScale = new Vector3(0.75f, 10, 1);
                break;
            case 1:
                transform.localScale = new Vector3(0.95f, 10, 1);
                break;
            case 2:
                transform.localScale = new Vector3(1.15f, 10, 1);
                break;
        }

        
    }
}
