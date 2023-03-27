using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class MoveToGoalAgent : Agent
{

    [SerializeField] private Transform targetTransform;
    [SerializeField] private GameObject spawnLoc;

    public override void OnEpisodeBegin()
    {
        transform.position = spawnLoc.transform.position;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);
        sensor.AddObservation(targetTransform.position);
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];

        float moveSpeed = 3f;

        transform.position += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;

    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            SetReward(+1f);
            EndEpisode();
        }

        if (other.gameObject.tag == "Wall")
        {
            SetReward(-1f);
            EndEpisode();
        }
        Debug.Log("Collision");
    }


    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Ball")
    //    {
    //        SetReward(+1f);
    //        EndEpisode();
    //    }

    //    if (collision.gameObject.tag == "Wall")
    //    {
    //        SetReward(-1f);
    //        EndEpisode();
    //    }
    //    Debug.Log("Collision");
    //}

}
