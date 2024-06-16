using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPoints : MonoBehaviour
{

    public Image[] healthImage;

    private void Update()
    {
        for (int i = 0; i < healthImage.Length; i++)
        {
            if (i < GameManager.Instance._lives)
            {
                healthImage[i].enabled = true;
            }
            else
            {
                healthImage[i].enabled = false;
            }
            
        }
    }

}
