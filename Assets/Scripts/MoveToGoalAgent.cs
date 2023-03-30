using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class MoveToGoalAgent : Agent
{

    [SerializeField] private Transform targetTransform1;
    [SerializeField] private Transform targetTransform2;
    [SerializeField] private Transform targetTransform3;
    [SerializeField] private Transform targetTransform4;
    [SerializeField] private Transform targetTransform5;
    [SerializeField] private Transform targetTransform6;
    [SerializeField] private Transform targetTransform7;
    [SerializeField] private GameObject spawnLoc;

    private bool[] hasCollected = { false, false, false, false, false, false, false };

    public override void OnEpisodeBegin()
    {
        for (int i = 0; i <= hasCollected.Length -1; i++)
        {
            hasCollected[i] = false;
        }

        transform.position = spawnLoc.transform.position;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);
        sensor.AddObservation(targetTransform1.position);
        sensor.AddObservation(targetTransform2.position);
        sensor.AddObservation(targetTransform3.position);
        sensor.AddObservation(targetTransform4.position);
        sensor.AddObservation(targetTransform5.position);
        sensor.AddObservation(targetTransform6.position);
        sensor.AddObservation(targetTransform7.position);
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];

        float moveSpeed = 12f;

        transform.position += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;

        //AddReward(-1f / MaxStep);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }


    public void OnTriggerEnter(Collider other) //Gives or takes rewards based on reaching checkpoints or hits walls.
    {
        if (other.gameObject.name == "Checkpoint 1")
        {
            if (hasCollected[0] == false)
            {
                SetReward(+1f);
                hasCollected[0] = true;
            }
            
            
        }

        if (other.gameObject.name == "Checkpoint 2")
        {
            if (hasCollected[1] == false)
            {
                SetReward(+1f);
                hasCollected[1] = true;
            }
            
        }

        if (other.gameObject.name == "Checkpoint 3")
        {
            if (hasCollected[2] == false)
            {
                SetReward(+2f);
                hasCollected[2] = true;
            }
            
        }

        if (other.gameObject.name == "Checkpoint 4")
        {
            if (hasCollected[3] == false)
            {
                SetReward(+1f);
                hasCollected[3] = true;
            }
            
        }

        if (other.gameObject.name == "Checkpoint 5")
        {
            if (hasCollected[4] == false)
            {
                SetReward(+1f);
                hasCollected[4] = true;
            }
            
        }

        if (other.gameObject.name == "Checkpoint 6")
        {
            if (hasCollected[5] == false)
            {
                SetReward(+1f);
                hasCollected[5] = true;
            }
            
        }

        if (other.gameObject.name == "FinishLine")
        {
            if (hasCollected[6] == false)
            {
                SetReward(+3f);
                hasCollected[6] = true;
                Debug.Log("AI Wins!");
            }
            EndEpisode();
        }

        if (other.gameObject.tag == "Wall")
        {
            SetReward(-1f);
            EndEpisode();
        }

        if (other.gameObject.tag == "Wall2")
        {
            SetReward(-3f);
            EndEpisode();
        }
        Debug.Log("Collision");
    }





}
