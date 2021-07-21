using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class StarManager : MonoBehaviour
{
    private static List<GameObject> StarList = new List<GameObject>();
    private Tilemap tilemap;

    // Start is called before the first frame update
    void Start()
    {
        ResetStarList();
        tilemap = GetComponent<Tilemap>();
    }
     
    // called after a level is loaded
    public void ResetStarList()
    {
        StarList.Clear();
        int children = gameObject.transform.childCount;
        Debug.Log("Children: " + children);
        for (int i = 0; i < children; i++)
        {            
            StarList.Add(gameObject.transform.GetChild(i).gameObject);
        }
        Debug.Log("StarList reset. \nCount: " + StarList.Count);
    }

    public int GetStarCount()
    {
        Debug.Log("Count: " + StarList.Count);
        return StarList.Count;
    }

    public static void RemoveStar(Collider2D collision)
    {
        if (StarList.Contains(collision.gameObject))
        {
            StarList.Remove(collision.gameObject);
            Destroy(collision.gameObject);
            Debug.Log("Star removed. \nCount : " + StarList.Count);
        }
        else
        {
            throw new NullReferenceException("Star not found!");
        }

    }
}
