using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    //podem enllaçar un objecte des de unity
    //public GameObject gm;
    private GameSceneUI gameSceneUI;

    private void Start()
    {
        // Es necessari definir quin objecte "GameSceneUI" hem de canviar
        // En cas de no definir-lo hauriem d'enllaçar-lo manualment cada vegada
        gameSceneUI = GameObject.Find("Canvas").GetComponent<GameSceneUI>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // BUG BUG BUG
        // collision counts doble ( 2 colliders => normal, is triggered).
        if (collision.CompareTag("Player"))
        {
            gameSceneUI.score += 1f;
            Destroy(gameObject);

            print(gameSceneUI.score);
        }
    }
}
