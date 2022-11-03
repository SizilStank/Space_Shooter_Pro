using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float _gameTimer = 1;

    //[SerializeField] private GameObject _spawnInCenta;

    [SerializeField] private GameObject _bgWaveOne, _bgWaveTwo, _bgWaveThree, _bgBOSS;

    [SerializeField] private List <GameObject> _addEnemyA1ToList;
    [SerializeField] private GameObject _enemyA1ToAdd;

    [SerializeField] private List<GameObject> _addEnemyAToList;
    [SerializeField] private GameObject _enemyAToAdd;

    [SerializeField] private SpawnManager _spawnManager;

    [SerializeField] private bool _isGameTimerStrated;

    private void OnEnable()
    {
        EventManager.EnemyAAddToList += EnemyAAddToList;
        EventManager.EnemyA1AddToList += EnemyA1AddToList;
        EventManager.EnemyA1RemoveFromList += EnemyA1RemoveFromList;
        EventManager.EnemyA1RemoveFromList += EnemyARemoveFromList;
    }

    private void OnDisable()
    {
        EventManager.EnemyAAddToList -= EnemyAAddToList;
        EventManager.EnemyA1AddToList -= EnemyA1AddToList;
        EventManager.EnemyA1RemoveFromList -= EnemyA1RemoveFromList;
        EventManager.EnemyA1RemoveFromList += EnemyARemoveFromList;
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


    private void EndFirstEnemyWave()
    {

    }

    #region CentaSpawnControl
    private void EnemyA1AddToList()
    {
        _addEnemyA1ToList.Add(_enemyA1ToAdd);
    }

    private void EnemyA1RemoveFromList()
    {
        _addEnemyA1ToList.Remove(_enemyA1ToAdd);
    }
    #endregion

    #region WaveOneControl
    private void EnemyAAddToList()
    {
        _addEnemyAToList.Add(_enemyAToAdd);
    }

    private void EnemyARemoveFromList()
    {
        _addEnemyAToList.Remove(_enemyAToAdd);
    }
    #endregion

}
