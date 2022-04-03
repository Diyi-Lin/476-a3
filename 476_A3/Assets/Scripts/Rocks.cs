using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Rocks : MonoBehaviour, IsDamagable
{
    PhotonView PV;
    public AudioSource m_ShootingAudio;         
    public AudioClip m_ExploClip;            

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }
    public void TakeDamage()
    {
        PV.RPC("TakeDamage_RPC", RpcTarget.AllViaServer);
    }

    [PunRPC]
    void TakeDamage_RPC()
    {
        if (!PV.IsMine)
            return;
        Destroy(this.gameObject);
        m_ShootingAudio.clip = m_ExploClip;
        m_ShootingAudio.Play();
    }
}
