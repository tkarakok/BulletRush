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
            other.transform.SetParent(parent);
            other.gameObject.transform.position = transform.position + new Vector3(.6f, 0, .6f);
            other.gameObject.AddComponent<Collect>().parent = parent;
            EventManager.Instance.CollectBullet();
        }
    }

}