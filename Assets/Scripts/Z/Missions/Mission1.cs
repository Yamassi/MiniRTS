using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Mission1 : MonoBehaviour
{
    [SerializeField] string _task1;
    [SerializeField] string _task1Ready;
    [SerializeField] string _task2;
    [SerializeField] string _task2Ready;
    [SerializeField] int _task1Data;
    [SerializeField] int _task2Data;
    [SerializeField] TextMeshProUGUI _text1;
    [SerializeField] TextMeshProUGUI _text2;
    TowerController[] _towers;

    private void Awake()
    {
        _text1.text = _task1 + " " + _task1Data;
        _text2.text = _task2 + " " + _task2Data;

        _towers = FindObjectsOfType<TowerController>();

        foreach (var tower in _towers)
        {
            // tower.TowerOnCoquered += Task1PartDone;
            // tower.TowerOnGoldMining += Task2PartDone;
        }
    }
    private void Update()
    {
        if (_task1Data == 0 && _task2Data == 0)
        {
            MissionComplete();
        }
    }

    void Task1PartDone()
    {
        _task1Data--;

        if (_task1Data <= 0)
        {
            print(_task1Data);
            _text1.text = _task1Ready;
        }
        else
        {
            _text1.text = _task1 + " " + _task1Data;
        }


    }
    void Task2PartDone()
    {
        _task2Data--;
        if (_task2Data <= 0)
        {
            _text2.text = _task2Ready;
        }
        else
        {
            _text2.text = _task2 + " " + _task2Data;
        }

    }

    void MissionComplete()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
