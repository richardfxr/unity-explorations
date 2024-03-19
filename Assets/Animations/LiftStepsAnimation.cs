using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftStepsAnimation : MonoBehaviour {
    // === PUBLIC VARIABLES ====================
    public float startY;
    public float endY;
    public float period;
    public float exponent;

    // === COROUTINES ==========================
    Coroutine animate;

    void Start() {
        if (animate != null) {
            StopCoroutine(animate);
        }
        animate = StartCoroutine(Animate());
    }

    IEnumerator Animate() {
        float halfPeriod = period / 2;
        float elapsedTime = 0;

        while (true) {
            // reset elapsedTime for each period
            elapsedTime = 0;

            while (elapsedTime < halfPeriod) {
                // update elapsedTime
                elapsedTime += Time.deltaTime;

                transform.position = new Vector3(
                    transform.position.x,
                    Easing.EaseInOutExp(
                        halfPeriod,
                        elapsedTime,
                        startY,
                        endY,
                        exponent
                    ),
                    transform.position.z
                );

                yield return null;
            }

            while (
                elapsedTime >= halfPeriod &&
                elapsedTime < period
            ) {
                // update elapsedTime
                elapsedTime += Time.deltaTime;

                transform.position = new Vector3(
                    transform.position.x,
                    Easing.EaseInOutExp(
                        halfPeriod,
                        elapsedTime - halfPeriod,
                        endY,
                        startY,
                        exponent
                    ),
                    transform.position.z
                );

                yield return null;
            }

            yield return null;
        }
    }
}
