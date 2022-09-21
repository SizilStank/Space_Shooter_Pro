using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnManager : MonoBehaviour
{

    //__________BasicEnemyType__________//
    [SerializeField] private GameObject _basicEnemyPrefab;
    [SerializeField] private float _enemySpawnTimer = 1f;
    [SerializeField] private GameObject _enemySpawnerContainer;
    [SerializeField] private Vector3 _initializeSpawmManagerPos = new Vector3(0, 10, 0);
    private bool _stopSpawn;

    //__________PowerUp__________//
    [SerializeField] private GameObject[] _powerUps;
    private bool _powerUpActive = true;

    // Start is called before the first frame update
    void Start()
    {

        transform.position = _initializeSpawmManagerPos;

        if (_basicEnemyPrefab == null || _enemySpawnerContainer == null)
        {
            Debug.Log("GAME OBJECT is NULL!");
        }

        StartCoroutine(EnemySpawnControl());

        StartCoroutine(PowerUpSpawner());

    }

    IEnumerator EnemySpawnControl()
    {
        while (_stopSpawn == false)
        {
            Vector3 randomPos = new Vector3(Random.Range(-9, 9), transform.position.y, 0);
            GameObject newEnemy = Instantiate(_basicEnemyPrefab, randomPos, Quaternion.identity);
            newEnemy.transform.parent = _enemySpawnerContainer.transform;
            yield return new WaitForSeconds(_enemySpawnTimer);
        }
    }

    public void StopEnemySpawner()
    {
        _stopSpawn = true;
    }

    IEnumerator PowerUpSpawner()
    {
        while (_powerUpActive)
        {
            Vector3 randomSpawnRange = new Vector3(Random.Range(-9, 9), transform.position.y, 0);
            int randomPowerUp = Random.Range(0, 3);
            Instantiate(_powerUps[randomPowerUp], randomSpawnRange, Quaternion.identity);            
            yield return new WaitForSeconds(Random.Range(5, 15));
        }
    }


    public void StopPowerUpSpawner()
    {
        _powerUpActive = false;
    }
}
