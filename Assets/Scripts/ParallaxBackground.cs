using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{

    private Transform cam;

    [SerializeField] private Transform sky;
    [SerializeField] private Transform trees;

    [Range(0f, 1f)]
    [SerializeField] private float parallaxSpeed;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
       sky.position = new Vector3(cam.position.x, cam.position.y, sky.position.z);
       trees.position = new Vector3(cam.position.x * parallaxSpeed, cam.position.y, trees.position.z);
      
       
        
    }
}
