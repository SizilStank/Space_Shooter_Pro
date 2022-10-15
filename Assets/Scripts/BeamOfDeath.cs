using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamOfDeath : MonoBehaviour
{
    [SerializeField] private GameObject anchor;
    [SerializeField] private float velocity;



    // Update is called once per frame
    void FixedUpdate()
    {
        transform.RotateAround(anchor.transform.localPosition, Vector3.back, Time.deltaTime * velocity);
    }
}
