using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    public bool debug;

    public virtual SteeringOutput GetKinematic(AiAgent agent)
    {
        return new SteeringOutput { angular = agent.transform.rotation };
    }

    public virtual SteeringOutput GetSteering(AiAgent agent)
    {
        return new SteeringOutput { angular = Quaternion.identity };
    }
}
