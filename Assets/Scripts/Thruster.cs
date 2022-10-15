using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Thruster : MonoBehaviour
{
    [SerializeField] Player _player;
    [SerializeField] Image _sliderImage;
    [SerializeField] Slider _slider;

    private void Start()
    {
        _slider = GetComponent<Slider>();
    }

    public void SetMaxThruster(float maxThrusters)
    {
        _slider.maxValue = maxThrusters;
        _slider.value = maxThrusters;
    }


    public void SetThruster(float thrusters)
    {
        _slider.value = thrusters;

        if (thrusters <= 0)
        {
            _sliderImage.gameObject.SetActive(false);
        }
        else
        {
            _sliderImage.gameObject.SetActive(true);
        }
    }
}
