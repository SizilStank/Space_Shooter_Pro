using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //[SerializeField] private float _centaSpawnCounter;
    //[SerializeField] private bool _isGameTimerStrated;

    [SerializeField] private GameObject _bgWaveOne, _bgWaveTwo, _bgWaveThree, _bgBOSS;

    [SerializeField] private List<GameObject> _addEnemyAToList;
    [SerializeField] private GameObject _enemyAToAdd;

    [SerializeField] private SpawnManager _spawnManager;

    

    private void OnEnable()
    {
        EventManager.EnemyAAddToList += EnemyAAddToList;       
    }

    private void OnDisable()
    {
        EventManager.EnemyAAddToList -= EnemyAAddToList;       
    }

    private void Start()
    {
        if (!GameObject.Find("SpawnManager").TryGetComponent<SpawnManager>(out _spawnManager))
        {
            _spawnManager.enabled = false;
            Debug.LogError("_spawnManager is NULL");
        }

    }

    private void Update()
    {
        EndFirstEnemyWave();
    }


    private void EndFirstEnemyWave()//calling from the SpawnManager
    {
        if (_addEnemyAToList.Count >= 10)//change to 60 its a 10 for testing
        {
            _spawnManager.StopFirstEnemyWaveSpawnControl();
            _bgWaveOne.SetActive(false);
            _bgWaveTwo.SetActive(true);
        }
    }


    private void EnemyAAddToList()
    {
        _addEnemyAToList.Add(_enemyAToAdd);
    }

}
