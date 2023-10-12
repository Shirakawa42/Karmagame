using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveNeighbors : MonoBehaviour
{
    public List<GameObject> neighbors = new List<GameObject>();

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.tag);
        if (other.tag == "Map")
        {
            neighbors.Add(other.transform.gameObject);
        }
    }

    public void Remove()
    {
        foreach (GameObject neighbor in neighbors)
            neighbor.SetActive(false);
    }
}
