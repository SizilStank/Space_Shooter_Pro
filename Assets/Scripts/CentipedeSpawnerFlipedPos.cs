using System.Collections;
using UnityEngine;

public class CentipedeSpawnerFlipedPos : MonoBehaviour
{
    [SerializeField] private GameObject _spawn;
    [SerializeField][Range(0, 10)] float wait = 1f;
    [SerializeField] private int _spawnCounter = 0;
    [SerializeField] private float _timeCounter;
    [SerializeField] private Player _player;

    private void Start()
    {
        StartCoroutine(SpawnDelay());

        if (!GameObject.Find("Player").TryGetComponent<Player>(out _player))
        {
            Debug.LogError("_player is NULL");
        }
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
            //Vector3 pos = new Vector3(-11, 3, 0);
            GameObject spawn = Instantiate(_spawn, transform.position, Quaternion.identity);
            spawn.transform.GetComponent<MovementTest>().SetWaitMotion(_spawnCounter);
            _spawnCounter++;
        }
    }
}
