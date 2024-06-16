using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{

    [SerializeField] Healthbar _healthbar;
    [SerializeField] Staminabar _staminabar;
    [SerializeField] PlayerController _playerController;

    float _playerOriginalSpeed;
    float _playerSprintSpeed;

    [SerializeField] SaveData _saveData;


    private void Start()
    {
        //_healthbar.SetMaxHealth(GameManager.Instance._playerHealth.MaxHealth);
        //_healthbar.SetMaxHealth(100);
        _staminabar.SetMaxStamina(GameManager.Instance._playerStamina.MaxStamina);
        //_playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        _playerOriginalSpeed = 2f;
        _playerSprintSpeed = 3f;

    }

    void Update()
    {
        //health
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerTakeDmg(20);
            Debug.Log(GameManager.Instance._playerHealth.Health);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            PlayerHeal(20);
            Debug.Log(GameManager.Instance._playerHealth.Health);
        }

        //stamina & speed
        if (Input.GetKey(KeyCode.Z))
        {
            if (GameManager.Instance._playerStamina.Stamina > 0)
            {
                PlayerUseStamina(60f);
                if (_playerController.moveSpeed != _playerSprintSpeed)
                {
                    _playerController.moveSpeed = _playerSprintSpeed;
                    //Debug.Log(_playerSprintSpeed);
                }
            }
            else
            {
                _playerController.moveSpeed = _playerOriginalSpeed;

            }
        }
        else
        {
            PlayerRegenStamina();
            if (_playerController.moveSpeed != _playerOriginalSpeed)
            {
                _playerController.moveSpeed = _playerOriginalSpeed;
                //Debug.Log(_playerOriginalSpeed);
            }

        }

        //savedata
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Saving game ...");
            //we can acces direct to fields because that class is serialized
            _saveData._currentLevel = SceneManager.GetActiveScene().buildIndex;
            _saveData._playerHealth = 100;

            _saveData._playerLocationX = gameObject.transform.position.x;
            _saveData._playerLocationY = gameObject.transform.position.y;
            _saveData._playerLocationZ = gameObject.transform.position.z;

            SaveManager.Save(_saveData);

        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("Loading game ...");
            _saveData = SaveManager.Load();

            gameObject.transform.position = new Vector3(_saveData._playerLocationX,
                _saveData._playerLocationY, _saveData._playerLocationZ);
        }

        //quit game
        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }

        // 
        if (GameManager.Instance._playerHealth.Health <= 0)
        {
            Loader.Load(Loader.Scene.GameOver);
        }
    }

    private void PlayerTakeDmg(int dmg)
    {
        GameManager.Instance._playerHealth.DmgUnit(dmg);
        _healthbar.SetHealth(GameManager.Instance._playerHealth.Health);

    }
    private void PlayerHeal(int healing)
    {
        GameManager.Instance._playerHealth.HealUnit(healing);
        _healthbar.SetHealth(GameManager.Instance._playerHealth.Health);
    }

    private void PlayerUseStamina(float staminaAmount)
    {
        GameManager.Instance._playerStamina.UseStamina(staminaAmount);
        _staminabar.SetStamina(GameManager.Instance._playerStamina.Stamina);
    }
    private void PlayerRegenStamina()
    {
        GameManager.Instance._playerStamina.RegenStamina();
        _staminabar.SetStamina(GameManager.Instance._playerStamina.Stamina);
    }
}
