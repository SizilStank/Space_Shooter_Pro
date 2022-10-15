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
    }

    public void SetMaxLaser(int maxShots)
    {
        _slider.maxValue = maxShots;
        _slider.value = maxShots;
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
