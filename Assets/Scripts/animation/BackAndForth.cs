using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackAndForth : MonoBehaviour {
    // Start is called before the first frame update
    public float zMax = 16;
    public float zMin = -16;

    public float speed = 3;

    private int _direction = 1;

    void Start() {
    }

    // Update is called once per frame
    void Update() {
        transform.Translate(0, 0, _direction * speed * Time.deltaTime);

        bool bounced = false;
        if (transform.position.z > zMax || transform.position.z < zMin) {
            _direction = -_direction;
            bounced = true;
        }

        if (bounced) {
            transform.Translate(0, 0, _direction * speed * Time.deltaTime);
        }


        // Debug.Log("Sphere pos: " + transform.position);
    }

    private void OnTriggerEnter(Collider other) {
        // if(other.transform.position.z)
        var positionDelta = other.transform.position.z - transform.position.z;
        if (_direction > 0 && positionDelta > 0) {
            _direction = -_direction;
        }

        if (_direction < 0 && positionDelta < 0) {
            _direction = -_direction;
        }
    }
}