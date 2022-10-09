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
}


   



