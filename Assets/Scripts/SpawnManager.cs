using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    //[SerializeField] private GameObject _centaSpawner01;
    //[SerializeField] private CentaActiveBehavior _activeBehavior01;
    //[SerializeField] private float _centaSpawnCounter;
    //[SerializeField] private bool _isGameTimerStrated;

    
    [SerializeField] private GameObject _basicEnemyPrefab;
    [SerializeField] private GameObject _enemySpawnerContainer;
    [SerializeField] private GameObject _centaSpawner;

    [SerializeField] private GameObject _bgWaveOne, _bgWaveTwo, _bgWaveThree, _bgBOSS;

    [SerializeField] private List<GameObject> _addEnemyAToList;
    [SerializeField] private GameObject _enemyAToAdd;

    [SerializeField] private List<int> _addCentaSpawnToList;
    

    [SerializeField] private CentaActiveBehavior _centaActiveBehavior;
    

    [SerializeField] private Vector3 _initializeSpawmManagerPos = new Vector3(0, 10, 0);

    [SerializeField] private float _enemySpawnTimer = 1f;
    [SerializeField] private float _dropChance;
    //[SerializeField] private int _countingCentaSpawns;
    //[SerializeField] private int _counter;

    private bool _stopFirstEnemySpawn;
    private bool _stopSecondEnemySpawn;
    private bool _stopThirdEnemySpawn;
    private bool _stopBOSSSpawn;

    //__________PowerUp__________//
    [SerializeField] private GameObject[] _powerUps;
    [SerializeField] private GameObject _ammoDrop;
    private bool _powerUpActive = true;


    [SerializeField] private Player _player;

    private void OnEnable()
    {
        EventManager.EnemyAAddToList += EnemyAAddToList;
        EventManager.CentaAddToList += CentaAddToList;
    }

    private void OnDisable()
    {
        EventManager.EnemyAAddToList -= EnemyAAddToList;
        EventManager.CentaAddToList -= CentaAddToList;
    }

    // Start is called before the first frame update
    void Start()
    {
        

        transform.position = _initializeSpawmManagerPos;

        if (_basicEnemyPrefab == null || _enemySpawnerContainer == null)
        {
            Debug.LogError("GAME OBJECT is NULL!");
        }

        
/*        if (!GameObject.Find("Player").TryGetComponent<Player>(out _player))
        {
            _player.enabled = false;
            Debug.LogError("Player is Null");
        }*/
    }

    private void Update()
    {
        _dropChance = Random.Range(0f, 101f);
        StopFirstEnemyWaveSpawnControl();
        StopSecondEnemyWaveSpawnControl();
    }

    private void CentaAddToList()//CenataActiveBehavior is calling this
    {
        _addCentaSpawnToList.Add(0);
    }

    private void EnemyAAddToList()
    {
        _addEnemyAToList.Add(_enemyAToAdd);
    }

    IEnumerator WaitForEnenimesAndPowerUpsToSpawn()
    {
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(FirstEnemyWaveSpawnControl());
        StartCoroutine(PowerUpSpawnControl());
    }

    public void StartGameAfterAstroidDestroy()//Asteroid is calling this
    {
        StartCoroutine(WaitForEnenimesAndPowerUpsToSpawn());
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

    public void StopFirstEnemyWaveSpawnControl()//change to 60 its a 10 for testing
    {
        if (_addEnemyAToList.Count >= 10)
        {
            _stopFirstEnemySpawn = true;
            _bgWaveOne.SetActive(false);
            _bgWaveTwo.SetActive(true);
            StartCoroutine(CentaSpawnerSetActiveDelay());//Starting the Second Wave
            _addEnemyAToList.Clear();
        }
    }

    IEnumerator CentaSpawnerSetActiveDelay()//Starting the Second Wave
    {
        yield return new WaitForSeconds(2);
        _centaSpawner.SetActive(true);
        StopCoroutine(CentaSpawnerSetActiveDelay());    
    }

    public void StopSecondEnemyWaveSpawnControl()
    {
        if (_addCentaSpawnToList.Count >= 3)
        {
            _centaActiveBehavior.CanWeSpawn();//this sets the bool _canWeSpawn to false on the CentaActiveBehavior class
            StartCoroutine(WaitToChangeThiredWaveBG());
        }
    }

    IEnumerator WaitToChangeThiredWaveBG()
    {
        yield return new WaitForSeconds(10);
        _bgWaveTwo.SetActive(false);
        _bgWaveThree.SetActive(true);
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
                int randomPowerUp = Random.Range(0, 7);
                Instantiate(_powerUps[randomPowerUp], randomSpawnRange, Quaternion.identity);
                yield return new WaitForSeconds(Random.Range(3, 11));       
        }
    }

    public void StopPowerUpSpawnControl()//Player class is calling this
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

    public void SpwanAmmoPowerUp()//Player class is calling this
    {
        StartCoroutine(WaitToDropAmmo());
    }

   
}
