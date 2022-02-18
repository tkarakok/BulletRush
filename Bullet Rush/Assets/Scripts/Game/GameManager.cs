using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    
    public float force;
    public GameObject finishLine;
    public Transform firePoint;
    public List<GameObject> bullets;
    public Vector3 newPositionForBullet;
    public GameObject confetti;

    [HideInInspector]public int bulletCounter; // onPlayer
    [HideInInspector]public bool finish; // onPlayer
    [HideInInspector]public int bulletsInMagazine; // onMagazine
    [HideInInspector]public int bonusMultiplier;
    private float _maxDistance;


    private void Start()
    {
        finish = false;
        _maxDistance = finishLine.transform.position.z - BulletMovementController.Instance.transform.position.z;
        bulletCounter = 0;
        bulletsInMagazine = 0;
    }

    private void Update()
    {
        float distance = finishLine.transform.position.z - BulletMovementController.Instance.transform.position.z;
        UIManager.Instance.levelProgressBar.value = 1 - (distance / _maxDistance);
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
