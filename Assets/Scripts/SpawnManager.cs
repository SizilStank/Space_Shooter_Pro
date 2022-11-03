using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    //__________BasicEnemyType__________//
    [SerializeField] private GameObject _basicEnemyPrefab;
    [SerializeField] private GameObject _enemySpawnerContainer;
    [SerializeField] private GameObject _centaSpawner;
    
    [SerializeField] private Vector3 _initializeSpawmManagerPos = new Vector3(0, 10, 0);

    [SerializeField] private float _enemySpawnTimer = 1f;
    [SerializeField] private float _dropChance;

    private bool _stopFirstEnemySpawn;
    private bool _stopSecondEnemySpawn;
    private bool _stopThirdEnemySpawn;
    private bool _stopBOSSSpawn;

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
        if (!GameObject.Find("Player").TryGetComponent<Player>(out _player))
        {
            _player.enabled = false;
            Debug.LogError("Player is Null");
        }
    }

    private void Update()
    {
        _dropChance = Random.Range(0f, 101f);
    }

    IEnumerator WaitForEnenimesandPowerUpsToSpawn()
    {
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(FirstEnemyWaveSpawnControl());
        StartCoroutine(PowerUpSpawnControl());
    }

    public void StartGameAfterAstroidDestroy()
    {
        StartCoroutine(WaitForEnenimesandPowerUpsToSpawn());
    }

    IEnumerator FirstEnemyWaveSpawnControl()
    {
        while (_stopFirstEnemySpawn == false)
        {
            EventManager.OnEnemyAAddToList();
            Vector3 randomPos = new Vector3(Random.Range(-9, 9), transform.position.y, 0);
            GameObject newEnemy = Instantiate(_basicEnemyPrefab, randomPos, Quaternion.identity);
            newEnemy.transform.parent = _enemySpawnerContainer.transform;
            yield return new WaitForSeconds(_enemySpawnTimer);
        }
    }

    public void StopFirstEnemyWaveSpawnControl()
    {
        _stopFirstEnemySpawn = true;
        _centaSpawner.SetActive(true);
    }


    

    IEnumerator PowerUpSpawnControl()
    {
        while (_powerUpActive == true)
        {
            if (_dropChance < 0.7f)
            {
                Vector3 NewrandomSpawnRange = new Vector3(Random.Range(-9, 9), transform.position.y, 0);
                //int randomPowerUp = Random.Range(0, 6);
                Instantiate(_powerUps[5], NewrandomSpawnRange, Quaternion.identity);
                yield return new WaitForSeconds(Random.Range(10, 16));
            }
            
            
                Vector3 randomSpawnRange = new Vector3(Random.Range(-9, 9), transform.position.y, 0);
                int randomPowerUp = Random.Range(0, 6);
                Instantiate(_powerUps[randomPowerUp], randomSpawnRange, Quaternion.identity);
                yield return new WaitForSeconds(Random.Range(3, 11));       
        }
    }

    public void StopPowerUpSpawnControl()
    {
        _powerUpActive = false;
    }

    IEnumerator WaitToDropAmmo()
    {
        yield return new WaitForSeconds(3);
        Vector3 randomPos = new Vector3(Random.Range(-9, 9), transform.position.y, 0);
        GameObject newAmmoDrop = Instantiate(_ammoDrop, randomPos, Quaternion.identity);
        newAmmoDrop.transform.parent = _enemySpawnerContainer.transform;
    }

    public void SpwanAmmoPowerUp()
    {
        StartCoroutine(WaitToDropAmmo());
    }

   
}
