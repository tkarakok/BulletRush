using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    
    public float force;
    public Transform firePoint;
    public List<GameObject> bullets;
    public Vector3 newPositionForBullet;

    [HideInInspector]public int bulletCounter; // onPlayer
    [HideInInspector]public int bulletsInMagazine; // onMagazine
    [HideInInspector]public int bonusMultiplier;

    private void Start()
    {
        bulletCounter = 0;
        bulletsInMagazine = 0;
    }

    public void StartFinish()
    {
        
        StartCoroutine(Finish());
    }

    IEnumerator Finish()
    {
        yield return new WaitForSeconds(1);
        for (int i = 0; i < bullets.Count; i++)
        {
            bullets[i].transform.position = firePoint.transform.position;
            bullets[i].SetActive(true);
            if (i == bullets.Count - 1)
            {
                bullets[i].AddComponent<GetMultiplier>();
            }
            bullets[i].GetComponent<Rigidbody>().velocity = Vector3.forward * force;
            yield return new WaitForSeconds(.25f);
        }
        
    }

}
