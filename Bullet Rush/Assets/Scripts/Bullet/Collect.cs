using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
    public Transform parent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collect"))
        {
            other.tag = "Untagged";
            GameManager.Instance.bullets.Add(other.gameObject);
            other.transform.SetParent(parent);
            other.gameObject.transform.position = GameManager.Instance.bullets[GameManager.Instance.bullets.Count - 1].transform.position + new Vector3(.6f, 0, .6f);
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
    }

}