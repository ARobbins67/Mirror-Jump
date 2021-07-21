using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{        
    [SerializeField] float Amplitude = .3f;
    [SerializeField] float Frequency = 3f;

    // Update is called once per frame
    void Update() 
    {
        Vector3 newPos = transform.position;
        newPos.y += Amplitude*.01f*Mathf.Sin(Frequency * Time.time);
        transform.position = newPos;
    }
}
