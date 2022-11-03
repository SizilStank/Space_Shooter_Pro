using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{

    public static event UnityAction LaserCollected;
    public static void OnLaserCollected() => LaserCollected?.Invoke();

     
    public static event UnityAction SubtractLaserCollected;
    public static void OnSubtractLaserCollected() => SubtractLaserCollected?.Invoke();

    //*********************************************************//

    public static event UnityAction StartGameAudio; //the Audio Manager and the Asteroid are listining for this event
    public static void OnStartGameAudio() => StartGameAudio?.Invoke();

    //*********************************************************//

    public static event UnityAction StartNewCentaSpawner;
    public static void OnStartNewCentaSpawner() => StartNewCentaSpawner?.Invoke();//adding on the eventmanager

    //*********************************************************//

    public static event UnityAction EnemyA1AddToList;
    public static void OnEnemyA1AddToList() => EnemyA1AddToList?.Invoke();//adding on the eventmanager


    public static event UnityAction EnemyA1RemoveFromList;
    public static void OnEnemyA1RemoveFromList() => EnemyA1RemoveFromList?.Invoke();//adding on the eventmanager

    //*********************************************************//

    public static event UnityAction EnemyAAddToList;
    public static void OnEnemyAAddToList() => EnemyAAddToList?.Invoke();//adding on the eventmanager


    public static event UnityAction EnemyARemoveFromList;
    public static void OnEnemyARemoveFromList() => EnemyARemoveFromList?.Invoke();//adding on the eventmanager


}

   



