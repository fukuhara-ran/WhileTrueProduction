using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ParallaxBg : MonoBehaviour
{
   public Transform cameraTransform;
    public float parallaxEffectMultiplier;

    private Transform[] layers;
    private float viewZone = 1.0f;
    private int leftIndex;
    private int rightIndex;
    private float backgroundSize;

    void Start()
    {
        cameraTransform = Camera.main.transform;
        layers = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            layers[i] = transform.GetChild(i);
        }

        leftIndex = 0;
        rightIndex = layers.Length - 1;
        backgroundSize = layers[0].GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate()
    {
        float deltaX = cameraTransform.position.x * parallaxEffectMultiplier;
        transform.position = new Vector3(deltaX, transform.position.y, transform.position.z);

        if (cameraTransform.position.x - viewZone > layers[rightIndex].position.x)
        {
            ScrollRight();
        }

        if (cameraTransform.position.x + viewZone < layers[leftIndex].position.x)
        {
            ScrollLeft();
        }
    }

    private void ScrollLeft()
    {
        layers[rightIndex].position = new Vector3(layers[leftIndex].position.x - backgroundSize, layers[leftIndex].position.y, layers[leftIndex].position.z);
        leftIndex = rightIndex;
        rightIndex--;
        if (rightIndex < 0)
        {
            rightIndex = layers.Length - 1;
        }
    }

    private void ScrollRight()
    {
        layers[leftIndex].position = new Vector3(layers[rightIndex].position.x + backgroundSize, layers[rightIndex].position.y, layers[rightIndex].position.z);
        rightIndex = leftIndex;
        leftIndex++;
        if (leftIndex == layers.Length)
        {
            leftIndex = 0;
        }
    }
}
