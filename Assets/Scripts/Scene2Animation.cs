using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene2Animation : MonoBehaviour {
    // === PUBLIC VARIABLES ====================
    public GameObject perspectiveCamera;
    public GameObject cube0Pivot;

    // === VALUES ==============================
    private Vector3 cameraRotation = new Vector3(0, -87.8f, 0);
    private Vector3 cube0Rotation = new Vector3(0, 0, 0);

    // === ROTATE ==============================
    // rotate camera
    private const float RC_duration1 = 4.2f;
    private const float RC_endY = 0;
    private const float RC_exponent1 = 2;
    // rotate cube0Pivot
    private const float RC_duration2 = 0.55f;
    private const float RC_endZ = 34;
    private const float RC_exponent2 = 2;
    // overall
    private const float RC_durationTotal = 1.65f;

    // === COROUTINES ==========================
    Coroutine animate;

    void Start() {
        if (animate != null) {
            StopCoroutine(animate);
        }
        animate = StartCoroutine(Animate());
    }

    IEnumerator Animate() {
        // === ROTATE CAMERA ==================
        float elapsedTime = 0;
        while (elapsedTime < RC_durationTotal) {
            // update elapsedTime
            elapsedTime += Time.deltaTime;

            // rotate camera
            perspectiveCamera.transform.eulerAngles = new Vector3 (
                cameraRotation.x,
                Easing.EaseOutExp(
                    RC_duration1,
                    elapsedTime,
                    cameraRotation.y,
                    RC_endY,
                    RC_exponent1
                ),
                cameraRotation.z
            );

            // rotate cube0Pivot
            cube0Pivot.transform.eulerAngles = new Vector3(
                cube0Rotation.x,
                cube0Rotation.z,
                Easing.EaseInExp(
                    RC_duration2,
                    elapsedTime,
                    cube0Rotation.y,
                    RC_endZ,
                    RC_exponent2
                )
            );

            yield return null;
        }

        // load Scene 3
        SceneManager.LoadScene(3);
    }
}
