using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene0Animation : MonoBehaviour {
    // === PUBLIC VARIABLES ====================
    public GameObject orangeSphere;
    public GameObject pinkSpehre;
    public GameObject pinkSpehreDup;
    public GameObject blueSphere;
    public GameObject yellowSphere;

    // === INITAL POSITIONS ====================
    private Vector3 orangeSphereInit = new Vector3(0, 0, 21); // x, y, scale

    // === ZOOM OUT ============================
    private const float ZO_duration = 0.95f;
    private const float ZO_delay = 0.2f;
    private const float ZO_orangeSphereEndDiameter = 4;
    private const float ZO_distance = 11;
    private const float ZO_exponent = 5;

    // === MOVE AROUND =========================
    // move & scale pinkSphere, pinkSphereDup, and blueSphere to the side
    private const float MA_duration1 = 1.6f;
    private const float MA_distanceFromCenter = 8;
    private const float MA_exponent1 = 4;
    // move & scale orangeSphere up
    private const float MA_delay2 = 0.3f;
    private const float MA_duration2 = 1.25f;
    private const float MA_exponent2 = 4;
    // move & scale yellowSphere down
    private const float MA_delay3 = 0.65f;
    private const float MA_duration3 = 1.05f;
    private const float MA_exponent3 = 3;
    // overall
    private const float MA_durationTotal = MA_delay3 + MA_duration3;

    // === MOVE UP =============================
    // move orangeSphere up
    private const float MU_distance1 = 2;
    // move yellowSphere up
    private const float MU_distance2 = 3;
    // overall
    private const float MU_delay = 0.25f;
    private const float MU_duration = 0.4f;
    private const float MU_exponent = 4;

    // === COROUTINES ==========================
    Coroutine animate;

    void Start() {
        if (animate != null) {
            StopCoroutine(animate);
        }
        animate = StartCoroutine(Animate());
    }

    IEnumerator Animate() {
        // === ZOOM OUT =======================
        float elapsedTime = 0;
        yield return new WaitForSeconds(ZO_delay);
        while (elapsedTime < ZO_duration) {
            // update elapsedTime
            elapsedTime += Time.deltaTime;

            // scale orangeSphere down
            float orangeEasedVal = Easing.EaseOutExp(
                ZO_duration,
                elapsedTime,
                orangeSphereInit.z,
                ZO_orangeSphereEndDiameter,
                ZO_exponent
            );
            orangeSphere.transform.localScale = new Vector3(orangeEasedVal, orangeEasedVal, orangeEasedVal);
            
            // scale pinkSphere, pinkSphereDup, and blueSphere up
            float otherEasedVal = Easing.EaseOutExp(
                ZO_duration,
                elapsedTime,
                ZO_distance * 2 - orangeSphereInit.z,
                ZO_distance * 2 - ZO_orangeSphereEndDiameter,
                ZO_exponent
            );
            pinkSpehre.transform.localScale = new Vector3(otherEasedVal, otherEasedVal, otherEasedVal);
            pinkSpehreDup.transform.localScale = new Vector3(otherEasedVal, otherEasedVal, otherEasedVal);
            blueSphere.transform.localScale = new Vector3(otherEasedVal, otherEasedVal, otherEasedVal);

            yield return null;
        }

        // === MOVE AROUND ====================
        elapsedTime = 0;
        while (elapsedTime < MA_durationTotal) {
            // update elapsedTime
            elapsedTime += Time.deltaTime;

            if (elapsedTime <= MA_duration1) {
                // move pinkSphere, pinkSphereDup, and blueSphere to the side
                float easedX1 = Easing.EaseInOutExp(
                    MA_duration1,
                    elapsedTime,
                    0,
                    MA_distanceFromCenter,
                    MA_exponent1
                );
                float easedY1 = Easing.EaseInOutExp(
                    MA_duration1,
                    elapsedTime,
                    ZO_distance,
                    0,
                    MA_exponent1
                );
                pinkSpehre.transform.position = new Vector3(-1 * easedX1, easedY1, pinkSpehre.transform.position.z);
                pinkSpehreDup.transform.position = new Vector3(-1 * easedX1, easedY1, pinkSpehreDup.transform.position.z);
                blueSphere.transform.position = new Vector3(easedX1, -1 * easedY1, blueSphere.transform.position.z);

                // scale pinkSphere, pinkSphereDup, and blueSphere down
                float easedScale1 = Easing.EaseInOutExp(
                    MA_duration1,
                    elapsedTime,
                    ZO_distance * 2 - ZO_orangeSphereEndDiameter,
                    MA_distanceFromCenter * 2,
                    MA_exponent1
                );
                pinkSpehre.transform.localScale = new Vector3(easedScale1, easedScale1, easedScale1);
                pinkSpehreDup.transform.localScale = new Vector3(easedScale1, easedScale1, easedScale1);
                blueSphere.transform.localScale = new Vector3(easedScale1, easedScale1, easedScale1);
            }
            
            if (elapsedTime >= MA_delay2) {
                // move orangeSphere up
                orangeSphere.transform.position = new Vector3(
                    orangeSphere.transform.position.x,
                    Easing.EaseInOutExp(
                        MA_duration2,
                        elapsedTime - MA_delay2,
                        0,
                        MA_distanceFromCenter,
                        MA_exponent2
                    ),
                    orangeSphere.transform.position.z
                );

                // scale orangeSphere up
                float easedScale2 = Easing.EaseInOutExp(
                    MA_duration2,
                    elapsedTime - MA_delay2,
                    ZO_orangeSphereEndDiameter,
                    MA_distanceFromCenter * 2,
                    MA_exponent2
                );
                orangeSphere.transform.localScale = new Vector3(easedScale2, easedScale2, easedScale2);
            }

            if (elapsedTime >= MA_delay3) {
                // move yellowSphere down
                yellowSphere.transform.position = new Vector3(
                    yellowSphere.transform.position.x,
                    Easing.EaseInOutExp(
                        MA_duration3,
                        elapsedTime - MA_delay3,
                        8,
                        -1 * MA_distanceFromCenter,
                        MA_exponent3
                    ),
                    yellowSphere.transform.position.z
                );

                // scale yellowSphere up
                float easedScale3 = Easing.EaseInOutExp(
                    MA_duration3,
                    elapsedTime - MA_delay3,
                    4,
                    MA_distanceFromCenter * 2,
                    3
                );
                yellowSphere.transform.localScale = new Vector3(easedScale3, easedScale3, easedScale3);
            }

            yield return null;
        }

        // === MOVE UP ========================
        elapsedTime = 0;
        yield return new WaitForSeconds(MU_delay);
        while (elapsedTime < MU_duration) {
            // update elapsedTime
            elapsedTime += Time.deltaTime;

            // move orangeSphere up
            orangeSphere.transform.position = new Vector3(
                orangeSphere.transform.position.x,
                Easing.EaseInExp(
                    MU_duration,
                    elapsedTime,
                    MA_distanceFromCenter,
                    MA_distanceFromCenter + MU_distance1,
                    MU_exponent
                ),
                orangeSphere.transform.position.z
            );

            // move yellowSphere up
            yellowSphere.transform.position = new Vector3(
                yellowSphere.transform.position.x,
                Easing.EaseInExp(
                    MU_duration,
                    elapsedTime,
                    -1 * MA_distanceFromCenter,
                    -1 * MA_distanceFromCenter + MU_distance2,
                    MU_exponent
                ),
                yellowSphere.transform.position.z
            );

            yield return null;
        }

        // load Scene 1
        SceneManager.LoadScene(1);
    }
}