using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceDir : AIMovement
{

    public override SteeringOutput GetKinematic(AiAgent agent)
    {
        var output = base.GetKinematic(agent);

        // TODO: calculate angular component
        if (agent.Velocity == Vector3.zero)
            return output;

        if (agent.Velocity != Vector3.zero)
        {
            output.angular = Quaternion.LookRotation(agent.Velocity);
        }

        return output;
    }
}
