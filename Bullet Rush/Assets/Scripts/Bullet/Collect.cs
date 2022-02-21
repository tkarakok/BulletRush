using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Collect : MonoBehaviour
{
    public bool defaultBullet;
    public Transform parent;
    private int _level = 0;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collect"))
        {
            other.tag = "Untagged";
            GameManager.Instance.bulletCounter++;
            GameManager.Instance.bullets.Add(other.gameObject);
            other.transform.SetParent(parent);
            other.gameObject.transform.position = GameManager.Instance.bullets[0].transform.position + (GameManager.Instance.bulletCounter * GameManager.Instance.newPositionForBullet);
            other.gameObject.AddComponent<Collect>().parent = parent;
            EventManager.Instance.CollectBullet();
        }
        else if (other.CompareTag("Slice") && gameObject.transform.parent != null)
        {
            other.tag = "Untagged";
            AudioManager.Instance.PlaySound(AudioManager.Instance.obstacleClip);
            other.transform.parent.GetChild(1).gameObject.SetActive(false);
            int index = GameManager.Instance.bullets.IndexOf(gameObject);
            int a = GameManager.Instance.bullets.Count - index;
            for (int i = index; i < GameManager.Instance.bullets.Count; i++)
            {
                GameManager.Instance.bulletCounter--;
                if (GameManager.Instance.bulletCounter == 0)
                {
                    UIManager.Instance.GameOver();
                }
                GameObject bullet = GameManager.Instance.bullets[i];
                bullet.transform.SetParent(null);

            }
            for (int i = 0; i < a; i++)
            {
                GameObject bullet = GameManager.Instance.bullets[GameManager.Instance.bullets.Count - 1];
                GameManager.Instance.bullets.Remove(bullet);

            }

        }
        else if (other.CompareTag("Wall") || other.CompareTag("Human"))
        {
            if (other.CompareTag("Human"))
            {
                other.tag = "Untagged";
                other.GetComponent<Animator>().SetBool("Dead",true);
            }
            AudioManager.Instance.PlaySound(AudioManager.Instance.obstacleClip);
            GameManager.Instance.bulletCounter --;
            if (GameManager.Instance.bulletCounter == 0)
            {
                UIManager.Instance.GameOver();
            }
            gameObject.SetActive(false);
            gameObject.transform.SetParent(null);
            GameManager.Instance.bullets.Remove(gameObject);
        }
        else if (other.CompareTag("Magazine"))
        {
            if (GameManager.Instance.finish)
            {
                GetMagazineBonus();
            }
            else if (!defaultBullet && !GameManager.Instance.finish)
            {
                GetMagazineBonus();
            }
           
        }
        else if (other.CompareTag("Converter"))
        {
            transform.GetChild(_level).gameObject.SetActive(false);
            _level++;
            transform.GetChild(_level).gameObject.SetActive(true);


        }
        else if (other.CompareTag("Multiplier"))
        {

            other.transform.DORotate(new Vector3(-180, 180, 0), .2f);
            gameObject.SetActive(false);
        }
    }

    public void GetMagazineBonus()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeInHierarchy)
            {
                GameManager.Instance.bulletsInMagazine += (i + 1);
                GameManager.Instance.bulletCounter--;
                UIManager.Instance.InGameCoinUpdate();
            }
        }
        gameObject.SetActive(false);
        gameObject.transform.SetParent(null);
    }
}