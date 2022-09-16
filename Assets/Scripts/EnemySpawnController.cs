using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{

    [SerializeField] private GameObject _basicEnemy;
    [SerializeField] private float _enemySpawnTimer = 1f;
    [SerializeField] private GameObject _spawnerContainer;
    private bool _stopSawn;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 10, 0);

        if (_basicEnemy == null)
        {
            Debug.Log("_basicEnemy is NULL!");
        }

        StartCoroutine(EnemySpawnControl());

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator EnemySpawnControl()
    {
        while (_stopSawn == false)
        {
            Vector3 randomPos = new Vector3(Random.Range(-9, 9), transform.position.y, 0);
            GameObject newEnemy = Instantiate(_basicEnemy, randomPos, Quaternion.identity);
            newEnemy.transform.parent = _spawnerContainer.transform;
            yield return new WaitForSeconds(_enemySpawnTimer);
        }
    }

    public void StopSawn()
    {
        _stopSawn = true;
    }
}
