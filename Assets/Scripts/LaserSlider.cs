using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserSlider : MonoBehaviour
{
    [SerializeField] Player _player;
    [SerializeField] Image _sliderImage;
    [SerializeField] Slider _slider;

    private void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.maxValue = 15;
        _slider.value = 15;
    }


    public void SetShots(int shots)
    {
        _slider.value = shots;

        if (shots == 0)
        {
            _sliderImage.gameObject.SetActive(false);
        }
        else
        {
            _sliderImage.gameObject.SetActive(true);
        }
    }


}
