using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMoveCam : MonoBehaviour
{
    public float speed = 2f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;
    }
}
