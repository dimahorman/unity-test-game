using UnityEngine;

public class OrbitCameraAbove : MonoBehaviour {
    [SerializeField] private Transform target;
    
    public float rotSpeed = 1.5f;
    private float _rotY;
    private Vector3 _offset;
    
    private void Start() {
        _rotY = transform.eulerAngles.y;
        _offset = target.position - transform.position;
    }

    void LateUpdate() {
        float horInput = Input.GetAxis("Horizontal");
        if (horInput != 0) {
            _rotY += horInput * rotSpeed;
        }
        Quaternion rotation = Quaternion.Euler(0, _rotY, 0);
        transform.position = target.position - (rotation * _offset); 
        transform.LookAt(target);
    }
}