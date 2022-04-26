using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalletExploration : MonoBehaviour
{
    public GameObject currentNode;


    private void Start()
    {
        //List<GameObject> the_list = BFS_Exploration();
        //foreach(GameObject go in the_list)
        //{ 
        //    Debug.Log(go.transform.position);
        //}
    }

    public List<GameObject> BFS_Exploration()
    {
        List<GameObject> bfsList = new List<GameObject>();
        Queue<GameObject> bfsQueue = new Queue<GameObject>();
        
        bfsQueue.Enqueue(currentNode);

        while(bfsQueue.Count > 0)
        {
            currentNode = bfsQueue.Dequeue();
            bfsList.Add(currentNode);

            NodeController currentNodeC = currentNode.GetComponent<NodeController>();
            PalletController currentNodeP = currentNode.GetComponent<PalletController>();
            if (!currentNodeP.visited)
            {
                currentNodeP.visited = true;
                
                if (currentNodeC.canMoveUp) bfsQueue.Enqueue(currentNodeC.nodeUp);
                if (currentNodeC.canMoveRight) bfsQueue.Enqueue(currentNodeC.nodeRight);
                if (currentNodeC.canMoveDown) bfsQueue.Enqueue(currentNodeC.nodeDown);
                if (currentNodeC.canMoveLeft) bfsQueue.Enqueue(currentNodeC.nodeLeft);
            }
        }

        return bfsList;
    }

}

