using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStamina 
{
    //Fields
    float _currentStamina;
    float _currentMaxStamina;
    float _staminaSpeed;
    bool _pauseStamina = false;

    // Propierties
    public float Stamina
    {
        get
        {
            return _currentStamina;
        }
        set
        {
            _currentStamina = value;
        }
    }
    public float MaxStamina
    {
        get
        {
            return _currentMaxStamina;
        }
        set
        {
            _currentMaxStamina = value;
        }
    }

    public float StaminaSpeed
    {
        get
        {
            return _staminaSpeed;
        }
        set
        {
            _staminaSpeed = value;
        }
    }
    public bool PauseStamina
    {
        get
        {
            return _pauseStamina;
        }
        set
        {
            _pauseStamina = value;
        }
    }

    // Constructor
    public UnitStamina(float stamina, float maxStamina, float staminaSpeed, bool pauseStamina)
    {
        _currentStamina = stamina;
        _currentMaxStamina = maxStamina;
        _staminaSpeed = staminaSpeed;
        PauseStamina = pauseStamina;
    }

    //Methods

    public void UseStamina(float staminaAmount, bool continuous)
    {
        if(continuous)
        {
            _currentStamina -= staminaAmount * Time.deltaTime;
        }
        else
        {
            _currentStamina -= staminaAmount;
        }
    }

    public void WasteStamina()
    {
        if (!PauseStamina)
        {
            _currentStamina -= _staminaSpeed * Time.deltaTime;
        }
    }

    public void RegenStamina(float staminaAmount)
    {
        _currentStamina += staminaAmount;

        if (_currentStamina > _currentMaxStamina)
        {
            _currentStamina = _currentMaxStamina;
        }
    }

}
