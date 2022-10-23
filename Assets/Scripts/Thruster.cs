using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Thruster : MonoBehaviour
{
    [SerializeField] private Image _sliderImage;
    [SerializeField] private Slider _slider;



    private void Start()
    {
        if (!TryGetComponent<Slider>(out _slider))
        {
            Debug.LogError("Slider is Null");
            _slider.enabled = false;
        }
    }

    public void ThrusterSetup(float maxValue)
    {
        _slider.minValue = 0f;
        _slider.maxValue = maxValue;
        _slider.value = _slider.maxValue;
    }

    public void SetThruster(float thrusters)
    {
        _slider.value = thrusters;
    }

}
