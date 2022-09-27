using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnOnWayPoints : MonoBehaviour
{

    //Array of points in the game for the gameobject to spawn on
    [SerializeField] private Transform[] _spawnPoints;
    //Array of gameobjects that will be spawned on the spawn points
    [SerializeField] private GameObject _spawnObjects;


    // Start is called before the first frame update
    void Start()
    {
        InstantiateOnWayPoints();      
    }


    private void InstantiateOnWayPoints()
    {   //for loop iterating through the lenght of the spawn points and adding one
        for (int i = 0; i < _spawnPoints.Length; i++)
        {   //setting the position of the spawn points position in the game
            transform.position = _spawnPoints[i].transform.position;
            //spawning the gameobjects to thier respective spawn point in the game
            GameObject spawnObjects = Instantiate(_spawnObjects, transform.position, Quaternion.identity);//what is ... for?
        }
        
    }
}
