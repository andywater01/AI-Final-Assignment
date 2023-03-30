using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class MazeAgent : Agent
{
    public float speed = 3f;
    public Transform target;
    private Rigidbody rb;

    public GameObject checkpoint1;
    public GameObject checkpoint2;
    public GameObject checkpoint3;
    public GameObject checkpoint4;
    public GameObject checkpoint5;
    public GameObject checkpoint6;
    public GameObject finishLine;
    public GameObject spawnLoc;

    Vector3 controlSignal = Vector3.zero;
    public override void Initialize()
    {
        rb = GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin()
    {
        target = checkpoint1.transform;
        transform.position = spawnLoc.transform.position;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);
        sensor.AddObservation(target.position);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {

        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];

        float moveSpeed = 6f;

        transform.position += (new Vector3(moveX, 0, moveZ) * moveSpeed * Time.deltaTime);

        //if (rb.velocity.sqrMagnitude > 25f)
        //{
        //    rb.velocity *= 0.95f;
        //}

        if (Vector3.Distance(transform.position, target.position) < 1.42f)
        {
            SetReward(1.0f);
            EndEpisode();
        }

        //AddReward(-1f / MaxStep);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            SetReward(-0.1f);
            EndEpisode();
            Debug.Log("Wall Hit");
        }


        if (collision.gameObject.name == "Checkpoint 1" && target == checkpoint1.transform)
        {
            SetReward(+0.1f);
            target = checkpoint2.transform;
            Debug.Log(target.gameObject.name + " " + "Hit!");
        }

        if (collision.gameObject.name == "Checkpoint 2" && target == checkpoint2.transform)
        {
            SetReward(+0.1f);
            target = checkpoint3.transform;
            Debug.Log(target.gameObject.name + " " + "Hit!");
        }

        if (collision.gameObject.name == "Checkpoint 3" && target == checkpoint3.transform)
        {
            SetReward(+0.1f);
            target = checkpoint4.transform;
            Debug.Log(target.gameObject.name + " " + "Hit!");
        }

        if (collision.gameObject.name == "Checkpoint 4" && target == checkpoint4.transform)
        {
            SetReward(+0.1f);
            target = checkpoint5.transform;
            Debug.Log(target.gameObject.name + " " + "Hit!");
        }

        if (collision.gameObject.name == "Checkpoint 5" && target == checkpoint5.transform)
        {
            SetReward(+0.1f);
            target = checkpoint6.transform;
            Debug.Log(target.gameObject.name + " " + "Hit!");
        }

        if (collision.gameObject.name == "Checkpoint 6" && target == checkpoint6.transform)
        {
            SetReward(+0.1f);
            target = finishLine.transform;
            Debug.Log(target.gameObject.name + " " + "Hit!");
        }


        else if (collision.gameObject.name == "FinishLine")
        {
            SetReward(+1.0f);
            EndEpisode();
            Debug.Log(target.gameObject.name + " " + "Hit!");
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;

        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }
}
