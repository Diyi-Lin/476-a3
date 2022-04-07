using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//Heuristic B
public class BArrive : AIMovement
{
    public float stopRadius;
    private float perception = 90;
    public float distance;
    private void DrawDebug(AiAgent agent)
    {
        if (debug)
        {
            DebugUtil.DrawCircle(agent.TargetPosition, transform.up, Color.yellow, stopRadius);
        }
    }
    public override SteeringOutput GetKinematic(AiAgent agent)
    {
        DrawDebug(agent);

        var output = base.GetKinematic(agent);

        Vector3 desiredVelocity = agent.TargetPosition - agent.transform.position;
        float v = Mathf.Sqrt(agent.Velocity.sqrMagnitude);

        //calculate angle
        float angle = Vector3.Angle(transform.forward, desiredVelocity);
        if (angle > (perception-20*v) && agent.Velocity != Vector3.zero)
            return output;
        else
        {
            distance = Mathf.Sqrt(desiredVelocity.sqrMagnitude);

            if (distance > stopRadius)
            {
                float speed = Math.Min(agent.maxSpeed, distance * 0.5f);//assuming t2t = 10
                desiredVelocity = desiredVelocity.normalized * speed;
            }
            else { return output; }

            output.linear = desiredVelocity;

            if (debug) Debug.DrawRay(transform.position, output.linear, Color.cyan);
            return output;
        }
    }
}
