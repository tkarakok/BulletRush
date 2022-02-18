using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FinishController : MonoBehaviour
{
    
    public Transform magazine;
    public Transform robot;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            if (!GameManager.Instance.finish)
            {
                GameManager.Instance.finish = true;
            }
            other.transform.SetParent(null);
            other.transform.DOMove(magazine.transform.position, .25f).OnComplete(()=> other.gameObject.SetActive(false));
            
            if (BulletMovementController.Instance.transform.childCount == 0)
            {
                StateManager.Instance.state = State.EndGame;
                
                robot.DORotate(new Vector3(0,0,0),.5f);
                GameManager.Instance.StartFinish();
            }
        }
    }

}
