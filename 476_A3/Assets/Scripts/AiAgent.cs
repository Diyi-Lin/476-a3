using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAgent : MonoBehaviour
{
    public float maxSpeed;
    public float maxDegreesDelta;
    public bool lockY = true;
    public bool debug;


    public Transform trackedTarget;
    [SerializeField] Vector3 targetPosition;

    public Vector3 TargetPosition
    {
        get => trackedTarget != null ? trackedTarget.position : targetPosition;
    }
    public Vector3 TargetForward
    {
        get => trackedTarget != null ? trackedTarget.forward : Vector3.forward;
    }
    public Vector3 TargetVelocity
    {
        get
        {
            Vector3 v = Vector3.zero;
            if (trackedTarget != null)
            {
                AiAgent targetAgent = trackedTarget.GetComponent<AiAgent>();
                if (targetAgent != null)
                    v = targetAgent.Velocity;
            }

            return v;
        }
    }

    public Vector3 Velocity { get; set; }

    public void TrackTarget(Transform targetTransform)
    {
        trackedTarget = targetTransform;
    }

    public void UnTrackTarget()
    {
        trackedTarget = null;
    }
    // Start is called before the first frame update
    private void Awake()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {

        if (debug)
            Debug.DrawRay(transform.position, Velocity, Color.red);

        GetKinematicAvg(out Vector3 kinematicAvg, out Quaternion rotation);

        Velocity = kinematicAvg.normalized * maxSpeed;

        transform.position += Velocity * Time.deltaTime;

        rotation = Quaternion.Euler(0, rotation.eulerAngles.y, 0);
        transform.rotation = rotation;
    }

    private void GetKinematicAvg(out Vector3 kinematicAvg, out Quaternion rotation)
    {
        kinematicAvg = Vector3.zero;
        Vector3 eulerAvg = Vector3.zero;
        AIMovement[] movements = GetComponents<AIMovement>();
        int count = 0;
        foreach (AIMovement movement in movements)
        {
            kinematicAvg += movement.GetKinematic(this).linear;
            eulerAvg += movement.GetKinematic(this).angular.eulerAngles;

            ++count;
        }

        if (count > 0)
        {
            kinematicAvg /= count;
            eulerAvg /= count;
            rotation = Quaternion.Euler(eulerAvg);
        }
        else
        {
            kinematicAvg = Velocity;
            rotation = transform.rotation;
        }
    }

    private void GetSteeringSum(out Vector3 steeringForceSum, out Quaternion rotation)
    {
        steeringForceSum = Vector3.zero;
        rotation = Quaternion.identity;
        AIMovement[] movements = GetComponents<AIMovement>();
        foreach (AIMovement movement in movements)
        {
            steeringForceSum += movement.GetSteering(this).linear;
            rotation *= movement.GetSteering(this).angular;
        }
    }
}

