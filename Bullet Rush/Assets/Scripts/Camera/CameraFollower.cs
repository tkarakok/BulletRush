using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
        if (StateManager.Instance.state == State.InGame)
        {
            Vector3 targetPosition = _target.position + _offset;
            transform.position = targetPosition;
        }
        else if (StateManager.Instance.state == State.EndGame)
        {
            _target = null;
            gameObject.transform.DOMoveX(0, .5f);
        }
    }
}
