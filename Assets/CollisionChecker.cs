using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionChecker : MonoBehaviour
{
    public int winorlose = 0; //0 none, 1 win, 2 lose
    public BettingSystem bs;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "FinishLine")
        {
            winorlose = 1;
            
        }
        else if (other.gameObject.tag == "Wall")
        {
            winorlose = 2;
            
        }

        
        
    }

    public int GetWinOrLose()
    {
        return winorlose;
    }

    public void SetWinOrLose(int worl)
    {
        winorlose = worl;
    }
}
