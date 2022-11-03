using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentaActiveBehavior : MonoBehaviour
{


    [SerializeField] private GameObject _spawnInCenta;

    private void Start()
    {
        StartCoroutine(SpawnInCenta());
    }

    // Start is called before the first frame update
    IEnumerator SpawnInCenta()
    {
        while (true)
        {
            Vector3 _randomPosY = new Vector3(11, Random.Range(4.5f, 0.47f), 0);
            GameObject spawnIn = Instantiate(_spawnInCenta, _randomPosY, Quaternion.identity);
            yield return new WaitForSeconds(12);
            Debug.Log("CENTA");
        }
        
    }

        

}
