using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene3Animation : MonoBehaviour {
    // === PUBLIC VARIABLES ====================
    public Camera orthgraphicCamera;
    public GameObject blueSphere;
    public GameObject pinkSphere;
    public GameObject purpleSphere;

    // === VALUES ==============================
    private Vector3 cameraPosition = new Vector3(-1.29f, 2.21f, 5); // x, y, orthographicSize
    private Vector3 blueSpherePosition = new Vector3(4.51f, 8.92f, 25); // x, y, scale
    private Vector3 pinkSpherePosition = new Vector3(4.51f, 8.92f, 25); // x, y, scale
    private Vector3 purpleSpherePosition = new Vector3(4.51f, 8.92f, 25); // x, y, scale

    // === MOVE OUT ============================
    // move camera left
    private const float MO_cameraX = -2.6f;
    private const float MO_cameraExponent = 4;
    // overall
    private const float MO_duration = 1.4f;
    private const float MO_endX = -3.5f;
    private const float MO_exponent = 4;
    private const float MO_durationTotal = 1.28f;

    // === ZOOM IN =============================
    private const float ZI_duration = 0.65f;
    private const float ZI_cameraDelay = 0.1f;
    private const float ZI_exponent = 4;
    private const float ZI_durationTotal = ZI_duration + ZI_cameraDelay;

    // === SCENE SWITCH ========================
    private const float sceneLoadDelay = 0.2f;

    // === COROUTINES ==========================
    Coroutine animate;

    void Start() {
        if (animate != null) {
            StopCoroutine(animate);
        }
        animate = StartCoroutine(Animate());
    }

    IEnumerator Animate() {
        // === MOVE OUT =======================
        float elapsedTime = 0;
        while (elapsedTime < MO_durationTotal) {
            // update elapsedTime
            elapsedTime += Time.deltaTime;

            // move camera left
            orthgraphicCamera.transform.position = new Vector3(
                Easing.EaseOutExp(
                    MO_duration,
                    elapsedTime,
                    cameraPosition.x,
                    cameraPosition.x + MO_cameraX,
                    MO_cameraExponent
                ),
                cameraPosition.y,
                orthgraphicCamera.transform.position.z
            );

            // move pinkSphere left
            pinkSphere.transform.position = new Vector3(
                Easing.EaseOutExp(
                    MO_duration,
                    elapsedTime,
                    pinkSpherePosition.x,
                    pinkSpherePosition.x + MO_endX,
                    MO_exponent
                ),
                pinkSpherePosition.y,
                pinkSphere.transform.position.z
            );

            // move purpleSphere left
            purpleSphere.transform.position = new Vector3(
                Easing.EaseOutExp(
                    MO_duration,
                    elapsedTime,
                    purpleSpherePosition.x,
                    purpleSpherePosition.x + MO_endX * 2,
                    MO_exponent
                ),
                purpleSpherePosition.y,
                purpleSphere.transform.position.z
            );

            yield return null;
        }
        // update values
        cameraPosition.x = cameraPosition.x + MO_cameraX;
        pinkSpherePosition.x = pinkSpherePosition.x + MO_endX;
        purpleSpherePosition.x = purpleSpherePosition.x + MO_endX * 2;

        // === ZOOM IN ========================
        elapsedTime = 0;
        while ( elapsedTime < ZI_durationTotal) {
            // update elapsedTime
            elapsedTime += Time.deltaTime;

            // move pinkSphere right
            pinkSphere.transform.position = new Vector3(
                Easing.EaseInExp(
                    ZI_duration,
                    elapsedTime,
                    pinkSpherePosition.x,
                    pinkSpherePosition.x - MO_endX,
                    ZI_exponent
                ),
                pinkSpherePosition.y,
                pinkSphere.transform.position.z
            );

            // move purpleSphere right
            purpleSphere.transform.position = new Vector3(
                Easing.EaseInExp(
                    ZI_duration,
                    elapsedTime,
                    purpleSpherePosition.x,
                    purpleSpherePosition.x - MO_endX * 2,
                    ZI_exponent
                ),
                purpleSpherePosition.y,
                purpleSphere.transform.position.z
            );

            if (elapsedTime > ZI_cameraDelay) {
                // move camera right
                orthgraphicCamera.transform.position = new Vector3(
                    Easing.EaseInExp(
                        ZI_duration,
                        elapsedTime - ZI_cameraDelay,
                        cameraPosition.x,
                        blueSpherePosition.x,
                        MO_cameraExponent
                    ),
                    Easing.EaseInExp(
                        ZI_duration,
                        elapsedTime - ZI_cameraDelay,
                        cameraPosition.y,
                        blueSpherePosition.y,
                        MO_cameraExponent
                    ),
                    orthgraphicCamera.transform.position.z
                );
            }

            yield return null;
        }

        yield return new WaitForSeconds(sceneLoadDelay);
        // load Scene 0
        SceneManager.LoadScene(0);
    }
}
