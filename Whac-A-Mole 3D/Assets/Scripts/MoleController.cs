using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleController : MonoBehaviour
{
    // Inspector Properties.
    public float TimeBetweenHitsInSeconds = 1;

    // Members.
    private GameController _gameController;
    private CapsuleCollider _capsuleCollider;
    private Animator _animator;
    private bool _isHit = false;
    private float _timeSinceLastHit = 1;

    // Life Cycle.
    void Start()
    {
        _animator = this.GetComponentInParent<Animator>();
        _capsuleCollider = this.GetComponent<CapsuleCollider>();
        _gameController = FindObjectOfType<GameController>();
    }
    void Update()
    {
    }

    // Events.
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Hammer")
            return;

        _animator.Play("Hide");
        _animator.speed = 5;
        _gameController.IncrementScore();
        Debug.Log("Collider activated by:" + this.transform.parent.name);
    }
}
