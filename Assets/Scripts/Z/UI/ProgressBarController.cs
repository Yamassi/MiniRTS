using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ProgressBarController : MonoBehaviour
{
    Slider _slider;
    public Action OnCoquered;
    private void OnEnable()
    {
        _slider = GetComponentInChildren<Slider>(true);
    }
    public void StartConquer()
    {
        StartCoroutine(ConquerProgress());
    }
    public void StopConquer()
    {
        _slider.value = 0;
    }
    IEnumerator ConquerProgress()
    {
        while (_slider.value < _slider.maxValue)
        {
            _slider.value += Time.deltaTime;
            yield return null;
        }
        if (_slider.value >= _slider.maxValue)
        {
            OnCoquered?.Invoke();
            yield return null;
        }
    }
}
