using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ParallaxBg : MonoBehaviour
{
   Transform cam;
   Vector3 startPos;
    float distance;

    GameObject[] bg;
    Material[] mat;
    float[] backspeed; 
    float farBack;

    [Range(0.01f, 0.05f)]
    public float parallaxSpeed;
    // Start is called before the first frame update
    void Start()
    {
       cam = Camera.main.transform;
       startPos = cam.position;

       int totalbg = transform.childCount;
       mat = new Material[totalbg];
       backspeed  = new float[totalbg];
       bg = new GameObject[totalbg];

       for (int i = 0; i < totalbg; i++)
       {
            bg[i] = transform.GetChild(i).gameObject;
            mat[i] = bg[i].GetComponent<Renderer>().material;
       }
       CalculateBackSpeed(totalbg);
    }

    void CalculateBackSpeed(int totalbg){
        for (int i = 0; i < totalbg; i++)
        {
            if ((bg[i].transform.position.z - cam.position.z) > farBack)
            {
                farBack = bg[i].transform.position.z - cam.position.z;
            }
        }
        for (int i = 0; i < totalbg; i++)
        {
            backspeed[i] = 1 - (bg[i].transform.position.z - cam.position.z)/farBack;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        distance = cam.position.x - startPos.x;
        transform.position = new Vector3(cam.position.x, transform.position.y, 0);

        for (int i = 0; i < bg.Length; i++)
        {
            float speed = backspeed[i] * parallaxSpeed;
            mat[i].SetTextureOffset("_MainTex", new Vector2(distance, 0) * speed);
        }
    }
}
