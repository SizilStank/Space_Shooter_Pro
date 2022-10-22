using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Thruster : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Image _sliderImage;
    [SerializeField] private Slider _slider;



    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _slider = GetComponent<Slider>();

        _slider.minValue = 0f;
        _slider.maxValue = 100f;
        _slider.value = 100f;
    }


    public void SetThruster(float thrusters)
    {
        _slider.value = thrusters;
    }

    public void ResetThrusters(float resetThruster)
    {

        _slider.value = resetThruster;

        /*_slider.value = Mathf.Lerp(_slider.minValue, _slider.maxValue, _fillTime);

        _fillTime += 0.375f * Time.deltaTime;*/
    }
}
