using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerController pc = other.GetComponent<PlayerController>();
        
        if(pc != null)
        {
            pc.BuffSpeed();
            Destroy(gameObject);
        }
    }
}
