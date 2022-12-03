using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{

    public static event UnityAction LaserCollected;
    public static void OnLaserCollected() => LaserCollected?.Invoke();

    //*********************************************************//

    public static event UnityAction SubtractLaserCollected;
    public static void OnSubtractLaserCollected() => SubtractLaserCollected?.Invoke();

    //*********************************************************//

    public static event UnityAction StartGameAudio; //the Audio Manager and the Asteroid are listining for this event
    public static void OnStartGameAudio() => StartGameAudio?.Invoke();

    //*********************************************************//

    public static event UnityAction EnemyAAddToList;
    public static void OnEnemyAAddToList() => EnemyAAddToList?.Invoke();//adding on the eventmanager

    //*********************************************************//

    public static event UnityAction RemoveEnemyAFromList;
    public static void OnRemoveEnemyAFromList() => RemoveEnemyAFromList?.Invoke();//adding on the eventmanager

    //*********************************************************//

    public static event UnityAction CentaAddToList;
    public static void OnCentaAddToList() => CentaAddToList?.Invoke();//adding on the eventmanager

    //*********************************************************//

    public static event UnityAction CentaRemoveFromList;
    public static void OnCentaRemoveFromList() => CentaRemoveFromList?.Invoke();//adding on the eventmanager


}

   



