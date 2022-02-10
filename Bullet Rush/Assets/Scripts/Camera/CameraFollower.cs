using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform _target;
    private Vector3 _offset;


    private void Start()
    {
        _offset = gameObject.transform.position - _target.position;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = _target.position + _offset;
        transform.position = targetPosition;
    }
}
