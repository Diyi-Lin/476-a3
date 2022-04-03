using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public float maxLifeTime = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,maxLifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController pc = other.GetComponent<PlayerController>();

        if(pc != null)
        {
            pc.TakeDamage();
            Destroy(this.gameObject,0f);
        }

        Rocks rock = other.GetComponent<Rocks>();

        if(rock != null)
        {
            rock.TakeDamage();
            Destroy(this.gameObject, 0f);
        }
    }
}
