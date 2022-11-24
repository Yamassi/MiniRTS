using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Mission2 : MonoBehaviour
{
    [SerializeField] string _task1;
    [SerializeField] string _task1Ready;

    [SerializeField] int _task1Data;

    [SerializeField] TextMeshProUGUI _text1;

    TowerController[] _towers;

    private void Awake()
    {
        _text1.text = _task1 + " " + _task1Data;

        _towers = FindObjectsOfType<TowerController>();

        foreach (var tower in _towers)
        {
            // tower.TowerOnCoquered += Task1PartDone;
        }
    }
    private void Update()
    {
        if (_task1Data == 0)
        {
            MissionComplete();
        }
    }

    void Task1PartDone()
    {
        _task1Data--;

        if (_task1Data <= 0)
        {
            // print(_task1Data);
            _text1.text = _task1Ready;
        }
        else
        {
            _text1.text = _task1 + " " + _task1Data;
        }


    }
    void MissionComplete()
    {
        SceneManager.LoadSceneAsync(2);
    }
}
