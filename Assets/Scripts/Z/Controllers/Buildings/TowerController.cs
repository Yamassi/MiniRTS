using System.Collections;
using UnityEngine;
using System;

public class TowerController : MonoBehaviour, IInteractable
{
    [SerializeField] private bool _isConquered;
    [SerializeField] private UnitsCheckColliderController _unitsCheckCollider;
    [SerializeField] private float _goldMiningCoolDown;
    [SerializeField] private int _goldMiningAmount;
    [SerializeField] private float _goldMiningCoolDownTimer;
    [SerializeField] private PopUpTextController _popUpTextPrefab;
    [SerializeField] private Camera _camera;
    [SerializeField] private ProgressBarController _progressBar;
    private BuildingsEventManager _buildingsEventManager;
    private bool _isFirstStart = true;
    private Vector3 _popUpOffset = new Vector3(0, 0, 2.2f);
    public bool IsConquered { get => _isConquered; }
    public void Init(BuildingsEventManager buildingsEventManager)
    {
        _buildingsEventManager = buildingsEventManager;
    }

    public void StartConquer()
    {
        if (!_unitsCheckCollider.IsProtected)
        {
            _progressBar.gameObject.SetActive(true);
            _progressBar.StartConquer();
            _progressBar.OnCoquered += ConquerComplete;
        }
    }
    public void StopConquer()
    {
        _progressBar.StopConquer();
        _progressBar.gameObject.SetActive(false);
        _progressBar.OnCoquered -= ConquerComplete;
    }
    void ConquerComplete()
    {
        _progressBar.OnCoquered -= ConquerComplete;
        _buildingsEventManager.TowerConquered();
        _progressBar.gameObject.SetActive(false);
        _isConquered = true;
    }
    private void Update()
    {
        if (_isConquered)
        {
            //Башня приносит золото с кулдауном
            _goldMiningCoolDownTimer += Time.deltaTime;
            if (_isFirstStart)
            {
                _isFirstStart = false;
                StartCoroutine(GoldMiningOn());
            }
        }
    }
    //Башня приносит золото каждые 10 секунд
    IEnumerator GoldMiningOn()
    {
        while (_isConquered)
        {
            if (_goldMiningCoolDownTimer > _goldMiningCoolDown)
            {
                //PopUp Галды + 1
                _buildingsEventManager.GoldMining();
                if (_popUpTextPrefab != null)
                {
                    ShowPopUpText(_goldMiningAmount);
                }
                _goldMiningCoolDownTimer = 0;
            }
            yield return null;

        }
    }
    private void ShowPopUpText(int gold)
    {
        var text = Instantiate(_popUpTextPrefab, transform.position + _popUpOffset, Quaternion.Euler(55, 0, 0), transform);
        text.GetComponent<TextMesh>().text = "+" + gold.ToString();
    }
}
