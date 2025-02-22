using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour {
    // Start is called before the first frame update
    private Animator _animator;
    
    void Start() {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
    }

    public void ReactToHit() {
        var behavior = transform.GetComponent<WanderingAI>();
        
        if (behavior != null && behavior.alive) {
            behavior.alive = false;
            transform.Rotate(-75f, 0, 0);
            _animator.SetFloat("Speed", 0);
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die() {
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }
}