using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaybackControls : MonoBehaviour {
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (Time.timeScale == 0.0f) {
                Time.timeScale = 1.0f;
            } else {
                Time.timeScale = 0.0f;
            }
        }
    }
}
