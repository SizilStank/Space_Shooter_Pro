using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField] private GameObject[] _basicEnemyPrefab;
    [SerializeField] private GameObject _enemySpawnerContainer;
    [SerializeField] private GameObject _centaSpawner;
    [SerializeField] private GameObject _thirdWaveSpawnerActive;
    [SerializeField] private GameObject _enemyBRams;
    [SerializeField] private GameObject _boss;

    [SerializeField] private GameObject _bgWaveOne, _bgWaveTwo, _bgWaveThree, _bgBOSS;

    [SerializeField] private List<GameObject> _addEnemyAToList;
    [SerializeField] private GameObject _enemyAToAdd;

    [SerializeField] private List<GameObject> _addCentaSpawnToList;
    [SerializeField] private GameObject _centaSpawnAddToList;

    [SerializeField] private List<GameObject> _thirdWaveList;
    [SerializeField] private GameObject _thirdEnemyObjectToRemove;


    [SerializeField] private CentaActiveBehavior _centaActiveBehavior;
    

    [SerializeField] private Vector3 _initializeSpawmManagerPos = new Vector3(0, 10, 0);

    [SerializeField] private float _enemySpawnTimer = 1f;
    [SerializeField] private float _enemyBSpawnTimer = 1f;
    [SerializeField] private float _dropChance;

    private bool _stopFirstEnemySpawn;
    private bool _stopSecondEnemySpawn;
    private bool _stopThirdEnemySpawn;
    private bool _stopBOSSSpawn;
    private bool _alwaysSpawningEnemyB = true;

    //__________PowerUp__________//
    [SerializeField] private GameObject[] _powerUps;
    [SerializeField] private GameObject _ammoDrop;
    private bool _powerUpActive = true;


    [SerializeField] private Player _player;

    private void OnEnable()
    {
        EventManager.RemoveEnemyAFromList += RemoveEnemyAFromList;
        EventManager.RemoveThirdWaveEnemiesFromList += RemoveThirdWaveEnemiesFromList;
        EventManager.CentaAddToList += CentaAddToList;
        EventManager.CentaRemoveFromList += CentaRemoveFromList;
    }

    private void OnDisable()
    {
        EventManager.RemoveEnemyAFromList -= RemoveEnemyAFromList;
        EventManager.RemoveThirdWaveEnemiesFromList -= RemoveThirdWaveEnemiesFromList;
        EventManager.CentaAddToList -= CentaAddToList;
        EventManager.CentaRemoveFromList -= CentaRemoveFromList;
    }

    private void Awake()
    {
        _boss.SetActive(false);
    }

    void Start()
    {
        
        transform.position = _initializeSpawmManagerPos;

        if (_basicEnemyPrefab == null || _enemySpawnerContainer == null)
        {
            Debug.LogError("GAME OBJECT is NULL!");
        }    
    }

    private void Update()
    {
        _dropChance = Random.Range(0f, 101f);
        StopFirstEnemyWaveSpawnControl();
        StopSecondEnemyWaveSpawnControl();
        SetActiveBoss();
    }

    private void CentaAddToList()//CenataActiveBehavior is calling this
    {
        _addCentaSpawnToList.Add(_centaSpawnAddToList);
    }

    private void CentaRemoveFromList()
    {
        _addCentaSpawnToList.Remove(_centaSpawnAddToList);
    }

    private void RemoveEnemyAFromList()
    {
        _addEnemyAToList.Remove(_enemyAToAdd);
    }

    private void RemoveThirdWaveEnemiesFromList()
    {
        _thirdWaveList.Remove(_thirdEnemyObjectToRemove);
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
        StartCoroutine(AlwaysSpawingEnemyB());
    }

    IEnumerator AlwaysSpawingEnemyB()
    {
        while (_alwaysSpawningEnemyB == true)//_alwaysSpawningEnemyB is set to true.
        {
            Vector3 randomPos = new Vector3(Random.Range(-9, 9), transform.position.y, 0);
            GameObject enemyB = Instantiate(_enemyBRams, randomPos, Quaternion.identity);
            enemyB.transform.parent = _enemySpawnerContainer.transform;
            yield return new WaitForSeconds(_enemyBSpawnTimer);
        }
    }

    IEnumerator FirstEnemyWaveSpawnControl()
    {
        for (int i = 0; i < 10; i++)//change to 60 its a 10 for testing
        {
            Vector3 randomPos = new Vector3(Random.Range(-9, 9), transform.position.y, 0);
            GameObject newEnemy = Instantiate(_basicEnemyPrefab[Random.Range(0, _basicEnemyPrefab.Length)], randomPos, Quaternion.identity);
            newEnemy.transform.parent = _enemySpawnerContainer.transform;
            yield return new WaitForSeconds(_enemySpawnTimer);

            
        }
    }

    public void StopFirstEnemyWaveSpawnControl()
    {
        if (_addEnemyAToList.Count <= 0)
        {
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

    private void StopSecondEnemyWaveSpawnControl()
    {
        if (_addCentaSpawnToList.Count <= 0)
        {
            //Destroy(_centaActiveBehavior);
            _bgWaveTwo.SetActive(false);
            _bgWaveThree.SetActive(true);
            ThirdEnemyWaveSpawnControl();
            _alwaysSpawningEnemyB = false;
        }
    }

    private void ThirdEnemyWaveSpawnControl()
    {
        _thirdWaveSpawnerActive.SetActive(true);    
    }

    private void SetActiveBoss()
    {
        if (_thirdWaveList.Count <= 0)
        {
            _boss.SetActive(true);
        }
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
                int randomPowerUp = Random.Range(0, 8);
                Instantiate(_powerUps[randomPowerUp], randomSpawnRange, Quaternion.identity);
                yield return new WaitForSeconds(Random.Range(3, 11));       
        }
    }

    public void StopPowerUpSpawnControl()//Player class is calling this
    {
        _powerUpActive = false;
    }

    public void StopEnemyBSpawnControl()//Player class is calling this
    {
        _alwaysSpawningEnemyB = false;
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
