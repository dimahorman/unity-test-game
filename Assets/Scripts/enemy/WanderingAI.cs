using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


public class WanderingAI : PausableBehavior {
    [SerializeField] private GameObject fireballPrefab;
    private GameObject _fireball;

    private float _speed = 3.0f;
    public float baseSpeed = 3.0f;
    public float obstacleRange = 5.0f;

    public bool alive;
    private Animator _animator;

    protected override void PausableUpdate() {
        var reactiveTarget = GetComponent<ReactiveTarget>();
        if (!alive) {
            return;
        }

        transform.Translate(0, 0, _speed * Time.deltaTime);
        
        _animator.SetFloat("Speed", _speed);
        
        Ray ray = new Ray(transform.position, transform.forward);

        RaycastHit hit;
        if (Physics.SphereCast(ray, 0.75f, out hit)) {
            var hitObject = hit.transform.gameObject;
            if (hitObject.GetComponent<PlayerCharacter>() != null) {
                _fireball = Instantiate(fireballPrefab);
                _fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);

                _fireball.transform.rotation = transform.rotation;
            }

            if (hit.distance < obstacleRange) {
                float angle = Random.Range(-110, 110);
                transform.Rotate(0, angle, 0);
            }
        }
    }

    private void OnPause() {
        _animator.StartPlayback();
    }
    
    private void OnResume() {
        _animator.StopPlayback();
    }

    void Start() {
        alive = true;
        _speed = PlayerPrefs.GetFloat("speed", 1) * baseSpeed;
        _animator = GetComponent<Animator>();
        _animator.SetFloat("Speed", _speed);
    }

    private void OnSpeedChange(float value) {
        Debug.Log("new multiplier for speed: " + value);
        _speed = value * baseSpeed;
    }

    public void setAlive(bool alive) {
        this.alive = alive;
    }

    public void Awake() {
        GameEvent.SpeedChangeEvent.AddListener(OnSpeedChange);
        GameEvent.ResumeGameEvent.AddListener(OnResume);
        GameEvent.PauseGameEvent.AddListener(OnPause);
    }

    public void OnDestroy() {
        GameEvent.SpeedChangeEvent.RemoveListener(OnSpeedChange);
        GameEvent.PauseGameEvent.RemoveListener(OnPause);
        GameEvent.ResumeGameEvent.RemoveListener(OnResume);
    }
}