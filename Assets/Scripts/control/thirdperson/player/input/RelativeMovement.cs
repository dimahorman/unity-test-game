using System;
using JetBrains.Annotations;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class RelativeMovement : MonoBehaviour {
    [SerializeField] private Transform target;

    public float rotSpeed = 15.0f;

    public float moveSpeed = 6f;
    
    public float jumpSpeed = 15.0f;
    public float gravity = -9.8f;
    public float terminalVelocity = -10.0f;
    public float minFall = -1.5f;
    
    public float pushForce = 3.0f;
    
    private CharacterController _charController;
    
    private ControllerColliderHit _contact;
    private Animator _animator;
    
    private float _vertSpeed;

    private void Start() {
        _charController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _vertSpeed = minFall;
    }
    
    private void OnControllerColliderHit(ControllerColliderHit hit) {
        _contact = hit;

        Rigidbody body = hit.collider.attachedRigidbody;
        if (body != null && !body.isKinematic) {
            body.velocity = hit.moveDirection * pushForce;
        }
    }

    private void Update() {
        Vector3 movement = Vector3.zero;

        float horInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");
        
        if (horInput != 0 || vertInput != 0) {
            movement = DoHorizontalMovement(movement, horInput, vertInput);
            movement = DoHandleRotationMovement(movement);
            
            _animator.SetFloat("Speed", movement.sqrMagnitude);
            
            movement = DoVerticalMovement(movement);
            
            movement *= Time.deltaTime;
            _charController.Move(movement);
        }
    }

    /// <summary>
    ///   <para>Handle rotation of the character relatively to the camera.</para>
    /// </summary>
    private Vector3 DoHandleRotationMovement(Vector3 movement) {
        Quaternion tmp = target.rotation;
        target.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);
        movement = target.TransformDirection(movement);
        target.rotation = tmp;

        Quaternion direction = Quaternion.LookRotation(movement);
        transform.rotation = Quaternion.Lerp(transform.rotation, direction, rotSpeed * Time.deltaTime);
        return movement;
    }

    private Vector3 DoHorizontalMovement(Vector3 movement, float horInput, float vertInput) {
        movement.x = horInput * moveSpeed;
        movement.z = vertInput * moveSpeed;

        movement = Vector3.ClampMagnitude(movement, moveSpeed);
        return movement;
    }

    /// <summary>
    ///   <para>Handle jumps, gravity and falling.</para>
    /// </summary>
    private Vector3 DoVerticalMovement(Vector3 movement) {
        bool hitGround = false;
        RaycastHit hit;
        if (_vertSpeed < 0 && Physics.Raycast(transform.position, Vector3.down, out hit)) {
            float check = (_charController.height + _charController.radius) / 1.9f;
            hitGround = hit.distance <= check;
        }
      

        if (hitGround) {
            if (Input.GetButtonDown("Jump")) {
                _vertSpeed = jumpSpeed;
            } else {
                _vertSpeed = minFall;
                _animator.SetBool("Jumping", false);
            }
        } else { 
            _vertSpeed += gravity * 5 * Time.deltaTime;
            if (_vertSpeed < terminalVelocity) {
                _vertSpeed = terminalVelocity;
            }

            if (_contact != null) {
                _animator.SetBool("Jumping", true);
            }

            if (_charController.isGrounded) {
                if (Vector3.Dot(movement, _contact.normal) < 0) {
                    movement = _contact.normal * moveSpeed;
                    // Debug.Log("lesser:" + _contact.normal);
                } else {
                    movement += _contact.normal * moveSpeed;
                    // Debug.Log("more:" + _contact.normal);
                }
            }
        }

        movement.y = _vertSpeed;
        return movement;
    }
}