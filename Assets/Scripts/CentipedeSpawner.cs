using System.Collections;
using UnityEngine;

public class CentipedeSpawner : MonoBehaviour // this is on the Centa Spawner that spawns the Centa Wave
{

    [SerializeField] private  GameObject _spawn;
    [SerializeField][Range(0, 10)] float wait = 1f;
    [SerializeField] private int _spawnCounter = 0;
    [SerializeField] private float _timeCounter;
    [SerializeField] private Player _player;

    private bool _canSpwn = true;

    private void Start()
    {
        StartCoroutine(SpawnDelay());
    }

    private void Update()
    {
        _timeCounter = Time.time;

        if (_spawnCounter == 6)
        {
            _canSpwn = false;
        }

        if (!_player )
        {
            _canSpwn = false;
        }
    }


    IEnumerator SpawnDelay()
    {     
            while (_canSpwn == true)
            {
                yield return new WaitForSeconds(wait);
                GameObject spawn = Instantiate(_spawn, transform.position, Quaternion.identity);
                spawn.transform.GetComponent<MovementTest>().SetWaitMotion(_spawnCounter);
                _spawnCounter++;
            }
    }

    
}
