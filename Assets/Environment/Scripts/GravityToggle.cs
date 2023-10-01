using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityToggle : MonoBehaviour
{
    bool Gravitylowered = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        JumpScript player = collision.GetComponent<JumpScript>();
        if (collision.gameObject.tag == "Player")
        {
           
            if(Gravitylowered == false)
            {
                player.rb.mass = 0.5f;
                //player.maxJumpPower = 20f;
                Gravitylowered = true;
                Debug.Log("Gravity Lowered");
            }
             
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        JumpScript player = collision.GetComponent<JumpScript>();
        player.rb.mass = 1f;
        //player.maxJumpPower = 20f;
        Gravitylowered = false;
        Debug.Log("Gravity Normal");
    }
}
