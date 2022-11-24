using System.Collections;
using UnityEngine;

public class BarracksController : MonoBehaviour, IInteractable
{
    [SerializeField] private BarracksGFXController _barracksGFX;
    [SerializeField] private BaseController _base;
    [SerializeField] private FXConstructionSmokeController _smokeFX;
    [SerializeField] private BarracksColliderController _barracksCollider;
    private Vector3 _barrackNotCompletePosition;
    private Vector3 _barrackCompletePosition;
    private Coroutine _barrackBuild;
    private bool _isBuildProcess;
    private bool _isCompleted;
    private BuildingsEventManager _buildingsEventManager;
    private UIEventManager _uIEventManager;
    public bool IsCompleted { get => _isCompleted; }
    public bool IsBuildProcess { get => _isBuildProcess; }

    public void Init(BuildingsEventManager buildingsEventManager, UIEventManager uIEventManager)
    {
        _buildingsEventManager = buildingsEventManager;
        _uIEventManager = uIEventManager;

        _barrackNotCompletePosition = new Vector3(_barracksGFX.transform.position.x, -3f, _barracksGFX.transform.position.z);
        _barrackCompletePosition = new Vector3(_barracksGFX.transform.position.x, 1.20f, _barracksGFX.transform.position.z);
    }
    public void StartBuildBarrack()
    {
        _isBuildProcess = true;

        _smokeFX.gameObject.SetActive(true);
        _smokeFX.Init();
        _barrackBuild = StartCoroutine(StartBuild());
    }
    public void StopBuild()
    {
        _isBuildProcess = false;

        StopAllCoroutines();
        _smokeFX.DeInit();
        _barracksGFX.transform.position = _barrackNotCompletePosition;
    }
    private IEnumerator StartBuild()
    {
        while (_barracksGFX.transform.position.y < _barrackCompletePosition.y)
        {
            print("whohoo");
            _base.transform.position = new Vector3(_base.transform.position.x, -1.1f, _base.transform.position.z);
            _smokeFX.transform.position = new Vector3(_smokeFX.transform.position.x, 0, _smokeFX.transform.position.z);
            // if (transform.position.y > 0f)
            //     _base.gameObject.SetActive(false);

            _barracksGFX.gameObject.transform.position += new Vector3(0, Time.deltaTime / 10, 0);
            yield return null;
        }
        if (_barracksGFX.transform.position.y >= _barrackCompletePosition.y)
        {
            _smokeFX.DeInit();
            _isCompleted = true;
            _barracksCollider.gameObject.SetActive(true);
            _barracksCollider.Init(_buildingsEventManager, this);
            yield return null;
        }
    }
}