using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Asteroid : MonoBehaviour
{

    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float zAngle;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private GameObject _astroidExplod;
    [SerializeField] private SpawnManager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(-0.08f, 4.17f, 0);
        _audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        AsteroidRotation();
    }

    private void AsteroidRotation()
    {
        transform.Rotate(0, 0, zAngle * _rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if (collision.CompareTag("PlayerLaser"))
        {
            _audioManager.AstroidDestroyed();
            GameObject astroidExplod = Instantiate(_astroidExplod, transform.position, Quaternion.identity);
            _spawnManager.StartGameAfterAstroidDestroy();
            Destroy(this.gameObject);
            Destroy(astroidExplod, 1);
        }
    }
}
