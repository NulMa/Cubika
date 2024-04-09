using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideLine : MonoBehaviour{

    void Update(){
        transform.position = new Vector3(GetComponentInParent<Transform>().position.x, 1.3f, 0);
    }
}
