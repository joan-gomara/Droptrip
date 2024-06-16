using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCallback : MonoBehaviour
{
    //variable per definir si es la primera actualització
    private bool isFirstUpdate = true;

    [SerializeField] AudioClip audioClick;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(audioClick, 1f);

        Screen.orientation = ScreenOrientation.LandscapeLeft;

    }

    private void Update()
    {
        //un cop actualitzem la pantalla carreguem el retorn del Loader
        if(isFirstUpdate)
        {
            isFirstUpdate = false;
            Loader.LoaderCallback();
        }
    }
}
