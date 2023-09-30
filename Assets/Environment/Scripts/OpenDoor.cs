using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public GameObject Door;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        {
            Debug.Log("Collided");
            Door.SetActive(false);
        }
    }

}
