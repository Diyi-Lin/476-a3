using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Rocks : MonoBehaviour, IsDamagable
{
    public AudioSource m_ShootingAudio;         
    public AudioClip m_ExploClip;            

    private void Awake()
    {
    }
    public void TakeDamage()
    {
        TakeDamage();
    }

    [PunRPC]
    void TakeDamage_RPC()
    {
        Destroy(this.gameObject);
        m_ShootingAudio.clip = m_ExploClip;
        m_ShootingAudio.Play();
    }
}
