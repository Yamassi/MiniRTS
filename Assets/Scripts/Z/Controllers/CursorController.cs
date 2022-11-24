using System.Collections;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    [SerializeField] Transform _circleCursor;
    [SerializeField] Transform _attackCursor;
    [SerializeField] Transform _axeCursor;
    [SerializeField] Transform _hammerCursor;
    [SerializeField] Transform _flagCursor;
    float _range;
    float _rangeMax = 0.4f;
    public void CursorMoveToPoint(Vector3 clickPosition)
    {
        _circleCursor.transform.position = new Vector3(clickPosition.x, 0.5f, clickPosition.z);
        _circleCursor.gameObject.SetActive(true);
        StartCoroutine(AnimateMoveToPoint());
    }
    public void CursorMoveToAttack(Vector3 clickPosition)
    {
        _attackCursor.transform.position = new Vector3(clickPosition.x, 2, clickPosition.z);
        _attackCursor.gameObject.SetActive(true);
        StartCoroutine(AnimateMoveToAttack());
    }
    public void CursorMoveToBuild(Vector3 clickPosition, float barrackHeight)
    {
        _hammerCursor.transform.position = new Vector3(clickPosition.x, barrackHeight + 1.1f, clickPosition.z);
        _hammerCursor.gameObject.SetActive(true);
        StartCoroutine(AnimateMoveToBuild());
    }
    public void CursorMoveToExtract(Vector3 clickPosition)
    {
        _axeCursor.transform.position = new Vector3(clickPosition.x, 2, clickPosition.z);
        _axeCursor.gameObject.SetActive(true);
        StartCoroutine(AnimateMoveToExtract());
    }
    public void CursorMoveToConquer(Vector3 clickPosition)
    {
        _flagCursor.transform.position = new Vector3(clickPosition.x, 1, clickPosition.z);
        _flagCursor.gameObject.SetActive(true);
        StartCoroutine(AnimateMoveToConquer());
    }
    IEnumerator AnimateMoveToPoint()
    {
        float rangeSpeed = 1.5f;
        while (_range < _rangeMax)
        {
            _range += rangeSpeed * Time.deltaTime;
            _circleCursor.localScale = new Vector3(_range, _range);
            yield return null;
        }

        if (_range > _rangeMax)
        {
            _range = 0.1f;
            yield return new WaitForSeconds(0.1f);
            _circleCursor.gameObject.SetActive(false);
        }
        yield return null;
    }
    IEnumerator AnimateMoveToAttack()
    {
        float rangeSpeed = 1.5f;
        while (_range < _rangeMax)
        {
            _range += rangeSpeed * Time.deltaTime;
            _attackCursor.localScale = new Vector3(_range, _range);
            yield return null;
        }
        if (_range > _rangeMax)
        {
            _range = 0.1f;
            yield return new WaitForSeconds(0.1f);
            _attackCursor.gameObject.SetActive(false);
        }
        yield return null;
    }
    IEnumerator AnimateMoveToBuild()
    {
        float rangeSpeed = 1.5f;
        while (_range < _rangeMax)
        {
            _range += rangeSpeed * Time.deltaTime;
            _hammerCursor.localScale = new Vector3(_range, _range);
            yield return null;
        }
        if (_range > _rangeMax)
        {
            _range = 0.1f;
            yield return new WaitForSeconds(0.1f);
            _hammerCursor.gameObject.SetActive(false);
        }
        yield return null;
    }
    IEnumerator AnimateMoveToExtract()
    {
        float rangeSpeed = 1.5f;
        while (_range < _rangeMax)
        {
            _range += rangeSpeed * Time.deltaTime;
            _axeCursor.localScale = new Vector3(_range, _range);
            yield return null;
        }
        if (_range > _rangeMax)
        {
            _range = 0.1f;
            yield return new WaitForSeconds(0.1f);
            _axeCursor.gameObject.SetActive(false);
        }
        yield return null;
    }
    IEnumerator AnimateMoveToConquer()
    {

        float rangeSpeed = 1.5f;
        while (_range < _rangeMax)
        {
            _range += rangeSpeed * Time.deltaTime;
            _flagCursor.localScale = new Vector3(_range, _range);
            yield return null;
        }
        if (_range > _rangeMax)
        {
            _range = 0.1f;
            yield return new WaitForSeconds(0.1f);
            _flagCursor.gameObject.SetActive(false);
        }
        yield return null;
    }
}
