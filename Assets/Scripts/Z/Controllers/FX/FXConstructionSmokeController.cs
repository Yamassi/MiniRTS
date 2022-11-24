using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXConstructionSmokeController : MonoBehaviour
{
    private ParticleSystem _fx;
    public void Init()
    {
        _fx = GetComponent<ParticleSystem>();
        var main = _fx.main;
        main.loop = true;
    }
    public void DeInit()
    {
        var main = _fx.main;
        main.loop = false;
        StartCoroutine(DisableAfter(2.5f));
    }

    IEnumerator DisableAfter(float time)
    {
        yield return new WaitForSeconds(time);
        _fx.gameObject.SetActive(false);
    }
}
