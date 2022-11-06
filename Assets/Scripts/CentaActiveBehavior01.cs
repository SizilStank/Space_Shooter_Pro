using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentaActiveBehavior01 : MonoBehaviour
{
    [SerializeField] private GameObject _spawnInCenta;
    [SerializeField] private float _spawnWaitTine;
    [SerializeField] private int _spawnCounter01;
    [SerializeField] private bool _canWeSpawn;

    private void Start()
    {
        _canWeSpawn = true;
        StartCoroutine(SpawnInCenta());
    }

    //public void CallAwesomeCoroutine() { StartCoroutine("SpawnInCenta"); }
    IEnumerator SpawnInCenta()
    {
        while (_canWeSpawn == true)
        {
            _spawnCounter01++;
            Vector3 _randomPosY = new Vector3(-11, Random.Range(4.5f, 0f), 0);
            GameObject spawnIn = Instantiate(_spawnInCenta, _randomPosY, Quaternion.identity);
            yield return new WaitForSeconds(_spawnWaitTine);
            Debug.Log("CENTA");
        }
    }

    public void CountingSpawns(int count)
    {
        _spawnCounter01 = count;
    }

    public void CanWeSpawn()
    {
        _canWeSpawn = false;
    }
}
