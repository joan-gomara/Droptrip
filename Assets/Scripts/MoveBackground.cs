using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{

    private float startPos;
    private float length;
    private GameObject cam;

    [SerializeField] private float parallaxEffect;
    [SerializeField] private bool repeat;
    [SerializeField] private float delay;


    void Start()
    {
        cam = GameObject.Find("Player Camera");
        startPos = transform.position.x;

        // mida per l'efecte parallax
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }




    void Update()
    {
        float distance = (cam.transform.position.x * parallaxEffect);
        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        // s'han de duplicar les capes en el costat necesari, ja que fins que no desapreix no torna a apareixer
        if(repeat)
        {
            float temp = (cam.transform.position.x) * (1 - parallaxEffect);

            if(temp > startPos + length)
            {
                length += delay;
                startPos += length;
            }
            if (temp < startPos - length)
            {
                length -= delay;
                startPos -= length;
            }
        }
    }
}
