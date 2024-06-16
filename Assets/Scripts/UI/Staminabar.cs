using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Staminabar : MonoBehaviour
{
    Slider _staminaSlider;

    private void Start()
    {
        _staminaSlider = GetComponent<Slider>();
    }

    public void SetMaxStamina(float maxStaminaBar)
    {
        _staminaSlider.maxValue = maxStaminaBar;
        _staminaSlider.value = maxStaminaBar;
    }

    public void SetStamina(float stamina)
    {
        _staminaSlider.value = stamina;
    }
}
