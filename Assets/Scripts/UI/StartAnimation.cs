using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartAnimation : MonoBehaviour
{

    float actualTime;
    float countTime;
    int countAudio;


    [SerializeField] AudioClip audioSimple;
    [SerializeField] AudioClip audioKik1;
    [SerializeField] AudioClip audioKik2;
    [SerializeField] AudioClip[] audioArray;
    [SerializeField] AudioSource audioSource;


    void Start()
    {
        actualTime = 0f;
        countTime = 0f;
        countAudio = 0;

        Screen.orientation = ScreenOrientation.LandscapeLeft;

    }




    void Update()
    {
        actualTime += Time.deltaTime;
        countTime += Time.deltaTime;

        if (countTime > 1f)
        {
            countAudio++;
            if (countAudio % 3 == 1)
            {
                audioSource.PlayOneShot(audioSimple, 1f);
            }
            else if (countAudio % 3 == 2)
            {
                audioSource.PlayOneShot(audioKik1, 0.7f);
            }
            else if (countAudio % 3 == 0)
            {
                audioSource.PlayOneShot(audioKik2, 0.9f);
            }

            countTime = 0f;
        }



        if (actualTime > 9.8f)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
