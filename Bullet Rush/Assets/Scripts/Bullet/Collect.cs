using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Collect : MonoBehaviour
{
    public Transform parent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collect") )
        {
            other.tag = "Untagged";
            GameManager.Instance.bullets.Add(other.gameObject);
            other.transform.SetParent(parent);
            other.gameObject.transform.position = GameManager.Instance.bullets[GameManager.Instance.bullets.Count - 1].transform.position + GameManager.Instance.newPositionForBullet;
            other.gameObject.AddComponent<Collect>().parent = parent;
            EventManager.Instance.CollectBullet();
        }
        
        else if (other.CompareTag("Wall"))
        {
            GameManager.Instance.bullets.Remove(gameObject);
            transform.SetParent(null);
            gameObject.SetActive(false);
            if (GameManager.Instance.bullets.Count == 0)
            {
                Debug.Log("game over");
            }
        }
        else if (other.CompareTag("Magazine"))
        {
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("Multiplier"))
        {
            other.transform.DORotate(new Vector3(-180,180,0),.2f);
            gameObject.SetActive(false);
        }
    }

}