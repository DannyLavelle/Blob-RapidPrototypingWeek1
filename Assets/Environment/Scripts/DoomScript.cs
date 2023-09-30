using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoomScript : MonoBehaviour
{
    public GameObject Menu;
    public float speed = 1.0f; // The speed at which the object moves up.

    private void Update()
    {
        // Move the object up in the Y-axis.
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
    public void IncreaseSpeed()
    {
        speed = speed + 0.1f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("dead");
            MenuController menu = Menu.GetComponent<MenuController>();
            menu.DeathSequence();
        }
    }

}
