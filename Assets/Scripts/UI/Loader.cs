using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//necesari per utilitzar el mètode "Action"
using System;


// Aquesta classe és estàtica ja que no ha de ser motificada en el joc sota cap circumstància
public static class Loader
{
    public enum Scene
    {
        StartAnimation,
        MainMenu,
        PlayMenu,
        Loading,
        Settings,
        GameOver,
        GameFinish,
        Scene_0,
        Scene_1_1,
        Scene_1_2,
        Scene_1_3
    }

    // crear un array amb el conjunt d'escenes?
    //https://stackoverflow.com/questions/71680080/can-i-create-array-of-scenes-unity


    //Metode sense parametres que no retorna un valor
    private static Action onLoaderCallback;

    public static void Load(Scene scene)
    {
        // defineix l'acció del "loader callback" amb una "function arrow"
        // executar aquesta acció carrega l'escena seleccionada
        onLoaderCallback = () =>
        {
            SceneManager.LoadScene(scene.ToString());
        };

        // carrega la escena "Loading"
        SceneManager.LoadScene(Scene.Loading.ToString());
    }

    public static void LoaderCallback()
    {
        //s'executa despres de la primera actualitzacio de la pantalla
        if (onLoaderCallback != null)
        {
            onLoaderCallback();
            onLoaderCallback = null;
        }
    }
}
