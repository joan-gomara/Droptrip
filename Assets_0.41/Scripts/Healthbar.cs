using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    Slider _healthSlider;

    private void Start()
    {
        _healthSlider = GetComponent<Slider>();
        //parche:
        _healthSlider.maxValue = 100;
        _healthSlider.value = 100;
    }

    // ERROR

    //public void SetMaxHealth(int maxHealth)
    //{
    //    _healthSlider.maxValue = maxHealth;
    //    _healthSlider.value = maxHealth;
    //}

    public void SetHealth(int health)
    {
        _healthSlider.value = health;
    }

}
