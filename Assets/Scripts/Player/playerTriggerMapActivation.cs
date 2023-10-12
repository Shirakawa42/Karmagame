using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerTriggerMapActivation : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Map")
        {
            other.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Map")
        {
            other.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
