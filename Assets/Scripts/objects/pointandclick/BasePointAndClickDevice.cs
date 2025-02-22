using System;
using UnityEngine;

public class BasePointAndClickDevice : MonoBehaviour {
    public float radius = 3.5f; 
    
    private void Update() {
        
    }

    private void OnMouseDown() {
        Transform player = GameObject.FindWithTag("Player").transform;

        var distance = Vector3.Distance(transform.position, player.position);
        if (distance <= radius) {
            if (Vector3.Dot(player.forward, transform.position - player.position) >= 0.5) {
                Operate();
            }
        }
    }

    public virtual void Operate() {
        // Override for specific device triggering
    }
}