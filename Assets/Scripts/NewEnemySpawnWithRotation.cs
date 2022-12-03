using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEnemySpawnWithRotation : MonoBehaviour
{


    [SerializeField] private List<GameObject> _newEnemySpawn;
    [SerializeField] private GameObject _enemySpawn;
    [SerializeField] private float _spawnTime;
    [SerializeField] private List<Transform> _wayPoints;

    [SerializeField] private bool _spawning = true;

    // Start is called before the first frame update
    void Start()
    {
        NewEnemySpawn(); 
    }

    private void Update()
    {



        if (_newEnemySpawn.Count >= 32)
        {
            _spawning = false;
            StopCoroutine(NewEnemySpawnFlow());
        }

       
    }

    private void NewEnemySpawn()
    {
        StartCoroutine(NewEnemySpawnFlow());
    }

    IEnumerator NewEnemySpawnFlow()
    {
        while (_spawning == true)            
        {
           //yield return new WaitForSeconds(1);
            for (int i = 0; i < _wayPoints.Count; i++)
            {
                _newEnemySpawn.Add(_enemySpawn);
                Instantiate(_newEnemySpawn[i], _wayPoints[i].position, Quaternion.identity);
                yield return new WaitForSeconds(1);
            }
        }
        
    }

    /*public void RemoveFromList()
    {
        _newEnemySpawn.Remove(_enemySpawn);
    }*/
}


