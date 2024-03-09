using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene1Animation : MonoBehaviour {
    public GameObject redQuad;
    public GameObject tealQuad;
    public GameObject purpleSphere;
    public GameObject orangeQuadPivot;

    // === ANIMATION CONSTS ====================
    // initial positions
    private Vector3 redQuadInit = new Vector3(0, -6.5f, 13); // x, y, scale
    private Vector3 tealQuadInit = new Vector3(0, -8.5f, 9); // x, y, scale
    private Vector3 purpleSphereInit = new Vector3(0, -10.5f, 9); // x, y, scale
    private Vector3 orangeQuadInit = new Vector3(0, -10, 20); // x, y, scale
    // move up
    private const float MU_duration1 = 0.6f;
    private const float MU_duration2 = 0.72f;
    private const float MU_duration3 = 0.92f;
    private const float MU_delay4 = 0.6f;
    private const float MU_duration4 = 0.9f;
    private const float MU_endY = -2.5f;
    private const float MU_exponent = 4;
    private const float MU_durationTotal = MU_delay4 + MU_duration4;
    // rotate, scale, and move
    private const float RT_duration = 1.0f;
    private const float RT_delay = 0.2f;
    private const float RT_yAngle = 90.0f;
    private const float RT_exponent = 4;
    private const float SC_duration1 = 0.6f;
    private const float SC_delay1 = 0.3f;
    private const float SC_duration2 = 0.6f;
    private const float SC_delay2 = 0.4f;
    private const float SC_duration3 = 0.6f;
    private const float SC_delay3 = 0.5f;
    private const float SC_scalePercent = 0.65f;
    private const float SC_exponent = 4;
    private const float MV_duration = 1.12f;
    private const float MV_delay = 0.43f;
    private const float MV_endY = -0.925f;
    private const float MV_exponent = 5;
    private const float RT_durationTotal = MV_delay + MV_duration;
    // move left
    private const float ML_duration1 = 0.8f;
    private const float ML_duration2 = 0.8f;
    private const float ML_delay2 = 0.05f;
    private const float ML_duration3 = 0.8f;
    private const float ML_delay3 = 0.07f;
    private const float ML_duration4 = 0.8f;
    private const float ML_delay4 = 0.09f;
    private const float ML_endX = -17;
    private const float ML_exponent = 4;
    private const float ML_durationTotal = ML_delay4 + ML_duration4;
    // scene transition
    private const float final_delay = 0.2f;

    // === COROUTINES ==========================
    Coroutine animate;

    void Start() {
        if (animate != null) {
            StopCoroutine(animate);
        }
        animate = StartCoroutine(Animate());
    }

    IEnumerator Animate() {
        // === MOVE UP ========================
        float elapsedTime = 0;
        while (elapsedTime < MU_durationTotal) {
            // update elapsedTime
            elapsedTime += Time.deltaTime;

            // move redQuad up
            redQuad.transform.position = new Vector3(
                redQuad.transform.position.x,
                Easing.EaseOutExp(
                    MU_duration1,
                    elapsedTime,
                    redQuadInit.y,
                    MU_endY,
                    MU_exponent
                ),
                redQuad.transform.position.z
            );

            // move tealQuad up
            tealQuad.transform.position = new Vector3(
                tealQuad.transform.position.x,
                Easing.EaseOutExp(
                    MU_duration2,
                    elapsedTime,
                    tealQuadInit.y,
                    MU_endY,
                    MU_exponent
                ),
                tealQuad.transform.position.z
            );

            // move purpleSphere up
            purpleSphere.transform.position = new Vector3(
                purpleSphere.transform.position.x,
                Easing.EaseOutExp(
                    MU_duration3,
                    elapsedTime,
                    purpleSphereInit.y,
                    MU_endY,
                    MU_exponent
                ),
                purpleSphere.transform.position.z
            );

            if (elapsedTime >= MU_delay4) {
                // move orangeQuad up
                orangeQuadPivot.transform.position = new Vector3(
                    orangeQuadPivot.transform.position.x,
                    Easing.EaseOutExp(
                        MU_duration4,
                        elapsedTime - MU_delay4,
                        orangeQuadInit.y,
                        MU_endY,
                        MU_exponent
                    ),
                    orangeQuadPivot.transform.position.z
                );
            }

            yield return null;
        }

        // === ROTATE, SCALE, & MOVE ==========
        elapsedTime = 0;
        yield return new WaitForSeconds(RT_delay);
        while (elapsedTime < RT_durationTotal) {
            // update elapsedTime
            elapsedTime += Time.deltaTime;

            orangeQuadPivot.transform.eulerAngles = new Vector3(
                orangeQuadPivot.transform.localRotation.eulerAngles.x,
                orangeQuadPivot.transform.localRotation.eulerAngles.y,
                Easing.EaseInOutExp(
                    RT_duration,
                    elapsedTime,
                    0,
                    RT_yAngle,
                    RT_exponent
                )
            );

            if (elapsedTime >= SC_delay1) {
                float easedScale = Easing.EaseInOutExp(
                    SC_duration1,
                    elapsedTime - SC_delay1,
                    purpleSphereInit.z,
                    SC_scalePercent * purpleSphereInit.z,
                    SC_exponent
                );
                purpleSphere.transform.localScale = new Vector3(easedScale, easedScale, easedScale);
            }

            if (elapsedTime >= SC_delay2) {
                float easedScale = Easing.EaseInOutExp(
                    SC_duration2,
                    elapsedTime - SC_delay2,
                    tealQuadInit.z,
                    SC_scalePercent * tealQuadInit.z,
                    SC_exponent
                );
                tealQuad.transform.localScale = new Vector3(
                    easedScale,
                    tealQuad.transform.localScale.y,
                    tealQuad.transform.localScale.z
                );
            }

            if (elapsedTime >= SC_delay3) {
                float easedScale = Easing.EaseInOutExp(
                    SC_duration3,
                    elapsedTime - SC_delay3,
                    redQuadInit.z,
                    SC_scalePercent * redQuadInit.z,
                    SC_exponent
                );
                redQuad.transform.localScale = new Vector3(
                    easedScale,
                    redQuad.transform.localScale.y,
                    redQuad.transform.localScale.z
                );
            }

            if (elapsedTime >= MV_delay) {
                purpleSphere.transform.position = new Vector3(
                    purpleSphere.transform.position.x,
                    Easing.EaseInOutExp(
                        MV_duration,
                        elapsedTime - MV_delay,
                        MU_endY,
                        MV_endY,
                        MV_exponent
                    ),
                    purpleSphere.transform.position.z
                );
            }

            yield return null;
        }

        // === MOVE LEFT ======================
        elapsedTime = 0;
        while (elapsedTime < ML_durationTotal) {
            // update elapsedTime
            elapsedTime += Time.deltaTime;

            orangeQuadPivot.transform.position = new Vector3(
                Easing.EaseInExp(
                    ML_duration1,
                    elapsedTime,
                    orangeQuadInit.x,
                    ML_endX,
                    ML_exponent
                ),
                orangeQuadPivot.transform.position.y,
                orangeQuadPivot.transform.position.z
            );

            

            if (elapsedTime >= ML_delay4) {
                redQuad.transform.position = new Vector3(
                    Easing.EaseInExp(
                        ML_duration4,
                        elapsedTime - ML_delay4,
                        redQuadInit.x,
                        ML_endX,
                        ML_exponent
                    ),
                    redQuad.transform.position.y,
                    redQuad.transform.position.z
                );
            }

            if (elapsedTime >= ML_delay2) {
                purpleSphere.transform.position = new Vector3(
                    Easing.EaseInExp(
                        ML_duration2,
                        elapsedTime - ML_delay2,
                        purpleSphereInit.x,
                        ML_endX,
                        ML_exponent
                    ),
                    purpleSphere.transform.position.y,
                    purpleSphere.transform.position.z
                );
            }


            if (elapsedTime >= ML_delay3) {
                tealQuad.transform.position = new Vector3(
                    Easing.EaseInExp(
                        ML_duration3,
                        elapsedTime - ML_delay3,
                        tealQuadInit.x,
                        ML_endX,
                        ML_exponent
                    ),
                    tealQuad.transform.position.y,
                    tealQuad.transform.position.z
                );
            }





            yield return null;
        }

        // load Scene 0
        yield return new WaitForSeconds(final_delay);
        SceneManager.LoadScene(0);
    }
}
