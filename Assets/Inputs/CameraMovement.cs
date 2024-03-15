using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour {
    public float rotateSpeed;
    public Vector3 initialRotation;
    private Vector2 _moveVector;

    void Start() {
        transform.eulerAngles = initialRotation;
    }

    void Update() {
        transform.eulerAngles = new Vector3(
            transform.eulerAngles.x,
            transform.eulerAngles.y - _moveVector.x * rotateSpeed * Time.deltaTime,
            transform.eulerAngles.z
        );
    }

    void OnMove(InputValue inputValue) {
        _moveVector = inputValue.Get<Vector2>();
    }
}
