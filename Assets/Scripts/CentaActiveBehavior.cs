using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentaActiveBehavior : MonoBehaviour  //This is on the GameObject in the Scene disabled...
{

   

    [SerializeField] private GameObject _spawnInCenta;
    [SerializeField] private GameObject _spawnInCenta01;
    [SerializeField] private float _spawnWaitTine;
    [SerializeField] private bool _canWeSpawn;

    private void Start()
    {
        _canWeSpawn = true;
        StartCoroutine(SpawnInCenta());
        StartCoroutine(SpawnInCenta01());
    }

    //public void CallAwesomeCoroutine() { StartCoroutine("SpawnInCenta"); }
    IEnumerator SpawnInCenta()
    {
        while (_canWeSpawn == true)
        {
            EventManager.OnCentaAddToList();//Adding to a list on the SpawnManager class
            yield return new WaitForSeconds(2);
            Vector3 _randomPosY = new Vector3(11, Random.Range(4.5f, 0f), 0);
            GameObject spawnIn = Instantiate(_spawnInCenta, _randomPosY, Quaternion.identity);
            yield return new WaitForSeconds(_spawnWaitTine);
            Debug.Log("CENTA");
        }
    }

    IEnumerator SpawnInCenta01()
    {
        while (_canWeSpawn == true)
        {
            EventManager.OnCentaAddToList();//Adding to a list on the SpawnManager class
            yield return new WaitForSeconds(5);
            Vector3 _randomPosY = new Vector3(-11, Random.Range(4.5f, 0f), 0);
            GameObject spawnIn01 = Instantiate(_spawnInCenta01, _randomPosY, Quaternion.identity);
            yield return new WaitForSeconds(_spawnWaitTine);
            Debug.Log("CENTA");
        }
    }

    public void CanWeSpawn()
    {
        _canWeSpawn = false;
    }

}
