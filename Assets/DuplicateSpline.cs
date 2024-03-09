using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class DuplicateSpline : MonoBehaviour {
    public GameObject splineContainer; // AllTheSplines GameObject
    public int numSplines; // 75
    public float offsetY; // 6
    public float offsetYIncrement; // 0.0015

    public GameObject splineA;
    public Vector3 offsetA; // 0.017, 0.11, 0.0

    public GameObject splineB;
    public Vector3 offsetB; // -0.017, 0.11, 0.0

    // Start is called before the first frame update
    void Start() {
        CloneSplines(splineA, offsetA);
        CloneSplines(splineB, offsetB);
    }

    // Creates a specified number of spline clones
    void CloneSplines(GameObject spline, Vector3 offset) {
        for (int i = 0; i < numSplines; i++) {
            // Copy spline
            GameObject splineClone = Instantiate(
                spline,
                new Vector3(
                    offset.x * i + spline.transform.position.x,
                    offset.y * i - offsetY + (offsetYIncrement * i * i),
                    spline.transform.position.z
                ),
                Quaternion.identity
            );

            // Move and rename spline
            splineClone.transform.parent = splineContainer.transform;
            splineClone.name = spline.name + "-" + (i + 1);
        }
    }
}
