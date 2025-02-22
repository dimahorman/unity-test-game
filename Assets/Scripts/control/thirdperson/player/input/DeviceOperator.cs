using System;
using UnityEngine;

public class DeviceOperator : MonoBehaviour {
    public float radius = 1.5f;

    private void Start() {
    }

    private void Update() {
        if (Input.GetButtonDown("Fire3")) {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);

            foreach (var collider in hitColliders) {
                var direction = collider.transform.position - transform.position;
                if (Vector3.Dot(transform.forward, direction) > 0.5f) {
                    collider.SendMessage("Operate", SendMessageOptions.DontRequireReceiver);
                }
            }
        }
    }
}