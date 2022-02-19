using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Wall : MonoBehaviour
{
    public bool moveable = false;
    public bool human = false;
    public float duration;
    public Vector3 targerPosition;

    private Vector3 _firstPosition;

    // Start is called before the first frame update
    void Start()
    {
        _firstPosition = transform.position;
        if (moveable)
        {
            StartCoroutine(Move());
        }
    }

    IEnumerator Move()
    {
        while (true)
        {
            if (human)
            {
                GetComponent<Animator>().SetBool("Idle", false);
                GetComponent<Animator>().SetBool("Walk", true);
                HumanMovement();
            }
            else
            {
                transform.DOMove(targerPosition, duration).OnComplete(() => transform.DOMove(_firstPosition, duration));
            }
            yield return new WaitForSeconds(duration * 2);
        }
    }

    void HumanMovement()
    {
        transform.DOMove(targerPosition, duration).OnComplete(() => GetComponent<Animator>().SetBool("Walk",false));

    }




}
