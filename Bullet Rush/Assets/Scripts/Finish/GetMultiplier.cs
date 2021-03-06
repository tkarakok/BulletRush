using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GetMultiplier : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Multiplier"))
        {
            GameManager.Instance.bonusMultiplier = other.GetComponent<BonusMultiplier>().bonusMultiplier;
            UIManager.Instance.EndGame();
            other.transform.DORotate(new Vector3(-180, 180, 0), .2f);
            gameObject.SetActive(false);
            AdManager.Instance.InterstitialAdShow();
        }
    }
}
