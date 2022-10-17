using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnManager : MonoBehaviour
{

    //__________BasicEnemyType__________//
    [SerializeField] private GameObject _basicEnemyPrefab;
    [SerializeField] private GameObject _enemySpawnerContainer;
    [SerializeField] private Vector3 _initializeSpawmManagerPos = new Vector3(0, 10, 0);

    [SerializeField] private float _enemySpawnTimer = 1f;

    private bool _stopSpawn;

    //__________PowerUp__________//
    [SerializeField] private GameObject[] _powerUps;
    [SerializeField] private GameObject _ammoDrop;
    private bool _powerUpActive = true;


    [SerializeField] private Player _player;

    // Start is called before the first frame update
    void Start()
    {

        transform.position = _initializeSpawmManagerPos;

        if (_basicEnemyPrefab == null || _enemySpawnerContainer == null)
        {
            Debug.LogError("GAME OBJECT is NULL!");
        }

        _player = GameObject.Find("Player").GetComponent<Player>();

    }

    IEnumerator WaitForEnenimesToSpawn()
    {
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(EnemySpawnControl());
        StartCoroutine(PowerUpSpawner());
    }

    public void StartGameAfterAstroidDestroy()
    {
        StartCoroutine(WaitForEnenimesToSpawn());
       
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
            int randomPowerUp = Random.Range(0, 6);
            Instantiate(_powerUps[randomPowerUp], randomSpawnRange, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(1, 10));
        }
    }

    IEnumerator WaitToDropAmmo()
    {
        yield return new WaitForSeconds(3);
        Debug.Log("AmmoDROP!!!");
        Vector3 randomPos = new Vector3(Random.Range(-9, 9), transform.position.y, 0);
        GameObject newAmmoDrop = Instantiate(_ammoDrop, randomPos, Quaternion.identity);
        newAmmoDrop.transform.parent = _enemySpawnerContainer.transform;
    }

    public void SpwanAmmoPowerUp()
    {
        StartCoroutine(WaitToDropAmmo());
    }

    public void StopPowerUpSpawner()
    {
        _powerUpActive = false;
    }
}
