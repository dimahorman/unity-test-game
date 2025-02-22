using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class FPSInput : PausableBehavior {
    private float _speed;
    public float jumpForce = 20;
    public float gravity = -9.8f;

    public float baseSpeed = 10f;
    
    public float maxJump = 100f;
    public float minJump = -9.8f;
    private CharacterController _charController;
    private PlayerCharacter _playerCharacter;

    private bool isJumpAllowed = true;

    // Start is called before the first frame update
    void Start() {
        _charController = GetComponent<CharacterController>();
        _playerCharacter = GetComponent<PlayerCharacter>();
        
        _speed = baseSpeed * PlayerPrefs.GetFloat("speed", 1);
    }

    // Update is called once per frame
    protected override void PausableUpdate() {
        float deltaX = Input.GetAxis("Horizontal") * _speed;
        float deltaZ = Input.GetAxis("Vertical") * _speed;

        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, _speed);

        movement.y = Mathf.Clamp(gravity + Input.GetAxis("Jump") * jumpForce, minJump, maxJump);

        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);

        _charController.Move(movement);
    }

    public void Awake() {
        GameEvent.SpeedChangeEvent.AddListener(OnSpeedChange);
    }

    public void OnDestroy() {
        GameEvent.SpeedChangeEvent.RemoveListener(OnSpeedChange);
    }

    private void OnSpeedChange(float value) {
        _speed = baseSpeed * value;
    }
}