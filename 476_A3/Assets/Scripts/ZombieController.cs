using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ZombieController : MonoBehaviour
{
    [SerializeField] GameObject TankTurret;
    [SerializeField] GameObject ShootPoint;
    public float countDownToShoot = 4;
    [SerializeField] AiAgent ai;
    [SerializeField] BArrive ba;
    Transform target;
    float countDown = 3.0f;

    public Rigidbody m_Shell;

    public AudioSource m_ShootingAudio;         
    public AudioClip m_FireClip;                
    public void Update()
    {
        if (inShootRange())
        {
            countDownToShoot -= 1.0f * Time.deltaTime;
            if (countDownToShoot <= 0)
            {
                Shoot();
                countDownToShoot = 3;
            }
            countDown -= 1.0f * Time.deltaTime;
            if (countDown <= 0)
            {
                FindTarget();
                countDown = 16;
            }
        }

    }
    bool inShootRange()
    {
        if (ba.distance < 8f)
            return true;
        else return false;
    }

    void Shoot()
    {
        Rigidbody shellInstance = Instantiate(m_Shell, ShootPoint.transform.position, TankTurret.transform.rotation);

        shellInstance.velocity = 25f * TankTurret.transform.forward;

        m_ShootingAudio.clip = m_FireClip;
        m_ShootingAudio.Play();
    }

    public void FindTarget()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        target = players[Random.Range(0, players.Length)].transform;
        ai.trackedTarget = target;
    }
}
