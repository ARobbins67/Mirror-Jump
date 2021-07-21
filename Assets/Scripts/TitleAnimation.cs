using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleAnimation : MonoBehaviour
{
    [SerializeField] float Amplitude = .5f;
    [SerializeField] float Frequency = 3f;

    private RectTransform rect;

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(gameObject.name == "TitleMirror")
        {
            Vector3 newPosition = rect.anchoredPosition;
            newPosition.y -= Amplitude * Mathf.Sin(Frequency * Time.time);
            rect.anchoredPosition = newPosition;
        }
        else
        {
            Vector3 newPosition = rect.anchoredPosition;
            newPosition.y += Amplitude * Mathf.Sin(Frequency * Time.time);
            rect.anchoredPosition = newPosition;
        }

    }
}