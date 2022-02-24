using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FinishController : MonoBehaviour
{
    
    public Transform magazine;
    public Transform robot;

    private GameObject _parent;

    private void Start()
    {
        _parent = GameObject.FindWithTag("Parent");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent == _parent.transform)
        {
            if (!GameManager.Instance.finish)
            {
                GameManager.Instance.finish = true;
                GameManager.Instance.confetti.SetActive(true);
            }
            other.transform.SetParent(null);
            other.transform.DOMove(magazine.transform.position, .25f).OnComplete(()=> other.gameObject.SetActive(false));
            AudioManager.Instance.PlaySound(AudioManager.Instance.confettiClip);
            if (BulletMovementController.Instance.transform.childCount == 0)
            {
                StateManager.Instance.state = State.EndGame;
                
                robot.DORotate(new Vector3(0,0,0),.5f);
                GameManager.Instance.StartFinish();
            }
        }
    }

}
