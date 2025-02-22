using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;


public class DoorOpenService : BasePointAndClickDevice {
    [SerializeField] private Vector3 posShift;

    public Vector3 shiftRate = new Vector3(0, 10.5f, 0);
    private bool isShifting = false;
    public bool lockedByKey = false;
    private bool _open;

    private void Start() {
    }

    private void Update() {
    }

    public override void Operate() {
        if (isShifting || lockedByKey) return; 
        
        if (_open) {
            StartCoroutine(MoveDoorDownGradually());
        }
        else {
            StartCoroutine(MoveDoorUpGradually());
        }
        
        _open = !_open;
    }

    private IEnumerator MoveDoorDownGradually() {
        isShifting = true;
        Debug.Log("Starting moving door down");
        var finalPos = transform.position - posShift;
        while (finalPos.y < transform.position.y) {
            transform.position -= shiftRate * Time.deltaTime;
            yield return null;
        }
        isShifting = false;
        Debug.Log("Finished moving door down");
    }
    
    private IEnumerator MoveDoorUpGradually() {
        isShifting = true;
        var finalPos = transform.position + posShift;
        while (finalPos.y > transform.position.y) {
            transform.position += shiftRate * Time.deltaTime;
            yield return null;
        }
        isShifting = false;
    }

    public void Activate() {
        if (isShifting) return; 
        if (!_open) {
            StartCoroutine(MoveDoorUpGradually());
            _open = !_open;
        }
    }
    public void Deactivate() {
        if (isShifting) return; 
        if (_open) { 
            StartCoroutine(MoveDoorDownGradually());
            _open = !_open;
        }
    }
}