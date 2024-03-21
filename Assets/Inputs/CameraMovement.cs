using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour {
    public GameObject marble;
    public float moveSmoothDampSpeed;
    public float cameraOffsetY;
    public float rotateSpeed;
    public float rotateSmoothDampSpeed;
    public Vector3 initialRotation;

    private Vector2 _moveVector;
    private float _currentRotateY;
    private float _targetRotateY;
    private float _rotateSmoothDampVelocity;
    private float _currentMoveY;
    private float _moveSmoothDampVelocity;

    void Start() {
        // set up initial rotation
        transform.eulerAngles = initialRotation;
        _currentRotateY = initialRotation.y;
        _targetRotateY = initialRotation.y;
    }

    void Update() {
        _targetRotateY -= _moveVector.x * rotateSpeed * Time.deltaTime;
        _currentRotateY = Mathf.SmoothDamp(_currentRotateY, _targetRotateY, ref _rotateSmoothDampVelocity, rotateSmoothDampSpeed);

        transform.eulerAngles = new Vector3(
            transform.eulerAngles.x,
            _currentRotateY,
            transform.eulerAngles.z
        );

        _currentMoveY = Mathf.SmoothDamp(_currentMoveY, marble.transform.position.y + cameraOffsetY, ref _moveSmoothDampVelocity, moveSmoothDampSpeed);

        transform.position = new Vector3(
            transform.position.x,
            _currentMoveY,
            transform.position.z
        );
    }

    void OnMove(InputValue inputValue) {
        _moveVector = inputValue.Get<Vector2>();
    }
}
