using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FinishController : MonoBehaviour
{
    
    public Transform magazine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            
            other.transform.SetParent(null);
            other.transform.DOMove(magazine.transform.position, .25f).OnComplete(()=> other.transform.DOMove(GameManager.Instance.firePoint.position,.1f));
            if (BulletMovementController.Instance.transform.childCount == 0)
            {
                StateManager.Instance.state = State.EndGame;
                GameManager.Instance.StartFinish();
            }
        }
    }

    


}
