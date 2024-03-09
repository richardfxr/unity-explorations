using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision_Demo : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    void OnCollisionEnter(Collision collision) {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.name == "Floor") {
            StartCoroutine(moveCube(collision));
        }
    }

    IEnumerator moveCube(Collision collision) {
        //Debug.Log(collision.gameObject.name);
        float elapsedTime = 0;
        while (elapsedTime < 5.0f) {
            elapsedTime += Time.deltaTime;

            collision.gameObject.transform.position = new Vector3(
                Easing.EaseInExp(
                    5.0f,
                    elapsedTime,
                    collision.gameObject.transform.position.x,
                    collision.gameObject.transform.position.x + 0.5f,
                    4
                ),
                collision.gameObject.transform.position.y,
                collision.gameObject.transform.position.z
            );

            yield return null;
        }
    }
}
