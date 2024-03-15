using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MarbleMovement : MonoBehaviour {
    public GameObject cameraPivot;
    public float maxVelocity;
    public float forceMultiplier;
    private Rigidbody _rigidbody;
    private Vector2 _moveVector;

    void Start() {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update() {
        // calculate addedForce based on y-axis rotation of camera
        Quaternion cameraRotation = Quaternion.Euler(
            0,
            cameraPivot.transform.eulerAngles.y,
            0
        );
        Vector3 addedForce = Vector3.Normalize(
            cameraRotation * Vector3.right * _moveVector.x +
            cameraRotation * Vector3.forward * _moveVector.y
        );
        Vector3 velocity = _rigidbody.velocity;
        Vector3 contribution = Vector3.Project(addedForce, velocity);

        // calculate coefficient based on velocity and contribution of addedForce
        // stops adding force if velocity is equal to or greater than maxVelocity
        float coefficient;
        if (Vector3.Normalize(velocity) == Vector3.Normalize(contribution)) {
            coefficient = Mathf.Sqrt(
                Mathf.Max(
                    Mathf.Pow(maxVelocity, 2) - (velocity + contribution).sqrMagnitude,
                    0
                )
            ) * forceMultiplier;
        } else {
            coefficient = maxVelocity * forceMultiplier;
        }

        //Debug.DrawRay(transform.position, addedForce * coefficient, Color.green);

        _rigidbody.AddForce(coefficient * addedForce);
    }

    void OnMove(InputValue inputValue) {
        _moveVector = inputValue.Get<Vector2>();
    }
}
