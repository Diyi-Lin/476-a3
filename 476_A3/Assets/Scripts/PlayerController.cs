using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviour, IsDamagable
{
    [SerializeField] GameObject TankTurret;
    [SerializeField] float mouseSensitivity, sprintSpeed, speed, smoothTime;

    Vector3 moveAmount;
    Vector3 smoothMoveVelocity;
    float AimingRotation;

    Rigidbody rb;
    PlayerManager playerManager;
    PhotonView PV;


     void Awake()
    {
        rb = GetComponent<Rigidbody>();
        PV = GetComponent<PhotonView>();

        playerManager = PhotonView.Find((int)PV.InstantiationData[0]).GetComponent<PlayerManager>();
    }

    private void Start()
    {
        if (!PV.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(rb);
        }
    }
    void Update()
    {
        if (!PV.IsMine)
            return;

        Move();
        Turn();
        Look();

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Move()
    {
        Vector3 moveDir = new Vector3(0, 0, Input.GetAxisRaw("Vertical")).normalized;
        moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed:speed), ref smoothMoveVelocity, smoothTime);
    }

    void Turn()
    {
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Horizontal") * 0.1f);
    }

    void Look()
    {
        AimingRotation += Input.GetAxisRaw("Mouse X") * mouseSensitivity;

        TankTurret.transform.localEulerAngles = Vector3.up * AimingRotation;
    }

    void Shoot()
    {
        Debug.Log("Shoot");
    }

    private void FixedUpdate()
    {
        if (!PV.IsMine)
            return;
        rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
    }

    public void TakeDamage()
    {
        PV.RPC("RPC_TakeDamage", RpcTarget.All);
    }

    [PunRPC]
    void RPC_TakeDamage()
    {
        if (!PV.IsMine)
            return;

        Debug.Log("Ahhh");

        Die();
    }

    void Die()
    {
        playerManager.Die();
    }

}
