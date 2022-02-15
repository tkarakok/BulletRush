using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    
    public float force;
    public Transform firePoint;
    public List<GameObject> bullets;
    public Vector3 newPositionForBullet;



    public void StartFinish()
    {
        StartCoroutine(Finish());
    }

    IEnumerator Finish()
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            
            bullets[i].SetActive(true);
            bullets[i].GetComponent<Rigidbody>().velocity = Vector3.forward * force;
            yield return new WaitForSeconds(.25f);
        }
    }

}
