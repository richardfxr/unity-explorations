using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRS_Demo : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        StartCoroutine(MoveObjectWithWait());
    }

    // Update is called once per frame
    void Update() {
        //transform.position = new Vector3(
        //    transform.position.x + 0.001f,
        //    transform.position.y,
        //    transform.position.z + 0.001f
        //);
        //transform.Rotate(
        //    90, 100, 70, Space.Self
        //);
    }
    IEnumerator MoveObjectWithWait() {
        transform.position += new Vector3(0.1f, 0, 0.1f);
        yield return new WaitForSeconds(1);
    }
}
