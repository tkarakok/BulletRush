using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class BulletMovementController : Singleton<BulletMovementController>
{
    
    [SerializeField] private float _limitX = 2;
    private float _xSpeed = 10;
    [SerializeField] private float _forwardSpeed = 2;
    [SerializeField] private float _waveSpeed = .05f;
    [SerializeField] private float _waveScale = .1f;
    private float _lastTouchedX;
    Sequence sequence;

    // Update is called once per frame
    void Update()
    {
        if (StateManager.Instance.state == State.InGame)
        {
            float _touchXDelta = 0;
            float _newX = 0;
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    _lastTouchedX = Input.GetTouch(0).position.x;
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    _touchXDelta = 5 * (Input.GetTouch(0).position.x - _lastTouchedX) / Screen.width;
                    _lastTouchedX = Input.GetTouch(0).position.x;
                }
            }
            if (Input.GetMouseButton(0))
            {
                _touchXDelta = Input.GetAxis("Mouse X");
            }
            _newX = transform.position.x + _xSpeed * _touchXDelta * Time.deltaTime;
            _newX = Mathf.Clamp(_newX, -_limitX, _limitX);



            Vector3 newPosition = new Vector3(_newX, transform.position.y, transform.position.z + _forwardSpeed * Time.deltaTime);
            transform.position = newPosition;


            for (int i = 1; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                child.DOMoveX(transform.position.x, (i * .2f));

            }
        }

    }

    #region Kill
    public void KillObject(GameObject killObject)
    {
        DOTween.Kill(killObject);
        StartCoroutine(WaitAndAlign(killObject));
    }
    IEnumerator WaitAndAlign(GameObject bullet)
    {
        yield return new WaitForSeconds(.25f);
        sequence = DOTween.Sequence();

        sequence.Append(bullet.transform.DOMove(new Vector3(bullet.transform.position.x + Random.Range(-.5f,.5f) + Random.Range(-1f,1f),bullet.transform.position.y, bullet.transform.position.z + Random.Range(1,2.5f)), .5f).
            SetEase(Ease.Flash));
        Destroy(bullet.GetComponent<Collect>());
        StartCoroutine(SetActiveFalse(bullet));
    }
    public IEnumerator SetActiveFalse(GameObject gameObject)
    {
        gameObject.transform.GetChild(3).gameObject.SetActive(true);
        yield return new WaitForSeconds(.2f);
        gameObject.SetActive(false);
    }
    #endregion

    #region Wave

    public void StartWave()
    {
        StartCoroutine(Wave());
    }

    IEnumerator Wave()
    {
        for (int i = 1; i < transform.childCount; i++)
        {
            transform.GetChild(i).localScale += new Vector3(_waveScale, _waveScale, _waveScale);
            yield return new WaitForSeconds(_waveSpeed);
            transform.GetChild(i).localScale -= new Vector3(_waveScale, _waveScale, _waveScale);
        }

    }
    #endregion
}