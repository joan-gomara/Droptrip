using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{

    private float startPos;
    private GameObject cam;
    [SerializeField] private float parallaxEffect;

    //endless:
    private float length;


    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Player Camera");
        startPos = transform.position.x;

        //endless:
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }



    // Update is called once per frame
    void Update()
    {
        float distance = (cam.transform.position.x * parallaxEffect);
        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);


        //endless:
        // duplicate the layers of background 2 times and add inside the first object (L & R)
        float temp = (cam.transform.position.x) * (1 - parallaxEffect);

        if(temp > startPos + length)
        {
            startPos += length;
        }
        if (temp < startPos - length)
        {
            startPos -= length;
        }
    }
}
