using System.Collections;
using UnityEngine;

public class CentipedeSpawner : MonoBehaviour // this is on the Centa Spawner that spawns the Centa Wave
{

    [SerializeField] private  GameObject _spawn;
    [SerializeField][Range(0, 10)] float wait = 1f;
    [SerializeField] private int _spawnCounter = 0;
    [SerializeField] private float _timeCounter;
    [SerializeField] private Player _player;

    private void Start()
    {
        StartCoroutine(SpawnDelay());
    }

    private void Update()
    {
        _timeCounter = Time.time;

        if (_spawnCounter >= 6)
        {
            Destroy(this.gameObject);
        }

        if (_player == null)
        {
            StopCoroutine(SpawnDelay());
            Destroy(this.gameObject);
        }
    }


    IEnumerator SpawnDelay()
    {     
            while (_spawnCounter <= 6)
            {
                yield return new WaitForSeconds(wait);
                GameObject spawn = Instantiate(_spawn, transform.position, Quaternion.identity);
                spawn.transform.GetComponent<MovementTest>().SetWaitMotion(_spawnCounter);
                _spawnCounter++;
            }
    }
}
