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
            GameManager.Instance.bulletCounter++;
            GameManager.Instance.bullets.Add(other.gameObject);
            other.transform.SetParent(parent);
            other.gameObject.transform.position = GameManager.Instance.bullets[0].transform.position +  (GameManager.Instance.bulletCounter * GameManager.Instance.newPositionForBullet);
            other.gameObject.AddComponent<Collect>().parent = parent;
            EventManager.Instance.CollectBullet();
        }
        else if (other.CompareTag("Slice") && gameObject.transform.parent != null)
        {
            other.tag = "Untagged";
            
            int index = GameManager.Instance.bullets.IndexOf(gameObject);
            int a = GameManager.Instance.bullets.Count - index;
            for (int i = index; i < GameManager.Instance.bullets.Count; i++)
            {
                GameManager.Instance.bulletCounter--;
                GameObject bullet = GameManager.Instance.bullets[i];
                bullet.transform.SetParent(null);
               
            }
            for (int i = 0; i < a; i++)
            {
                GameObject bullet = GameManager.Instance.bullets[GameManager.Instance.bullets.Count - 1];
                GameManager.Instance.bullets.Remove(bullet);
            }
            if (GameManager.Instance.bullets.Count == 0)
            {
                Debug.Log("Game Over");
            }
        }
        else if (other.CompareTag("Wall"))
        {
            GameManager.Instance.bulletCounter--;
            gameObject.SetActive(false);
            gameObject.transform.SetParent(null);
            GameManager.Instance.bullets.Remove(gameObject);
        }
        else if (other.CompareTag("Magazine"))
        {
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("Multiplier"))
        {
            GameManager.Instance.bonusMultiplier = other.GetComponent<BonusMultiplier>().bonusMultiplier;
            other.transform.DORotate(new Vector3(-180,180,0),.2f);
            gameObject.SetActive(false);
        }
    }

}