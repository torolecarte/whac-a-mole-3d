using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerController : MonoBehaviour
{
    // Inspector Properties.
    public Camera MainCamera;
    public LayerMask RaycastLayerMask;

    // Members.
    private Animator _animator;
    private bool _isHitting = false;


    // Life Cycle.
    void Start()
    {
        _animator = this.GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            _isHitting = true;
    }
    private void FixedUpdate()
    {
        MoveHammerToMousePosition();
        Hit();
    }

    private void Hit()
    {
        if (!_isHitting)
            return;

        _isHitting = false;
        _animator.SetTrigger("Hit");
    }
    private void MoveHammerToMousePosition()
    {
        Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit raycastHit, Single.MaxValue, RaycastLayerMask))
        {
            if(raycastHit.transform.tag != "Hammer")
                transform.position = raycastHit.point;
        }
    }

    private void SetIsHittingFalse()
    {
        _isHitting = false;
    }
}
