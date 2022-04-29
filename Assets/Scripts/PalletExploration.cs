using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PalletExploration : MonoBehaviour
{

    // current node at the first will be selected in the unity interface to choose
    // the node that the BFS search will start from
    public GameObject currentNode;


    //For debuging------------------------------------
    //private void Start()
    //{   

    //    // I am using this for testing that the BFS method is working properly and the all nodes
    //    // are stored in the list, and I make this by counting the message in the console and the 
    //    // the number of nodes in unity interface ... Right click on first node + shift button + right click on the last node

    //    List<GameObject> the_list = BFS_Exploration();
    //    int nodeIndex = 0;
    //    foreach(GameObject go in the_list)
    //    { 
    //        Debug.Log("node" + nodeIndex + go.transform.position);
    //        nodeIndex++;
    //    }
    //}
    //For debuging------------------------------------

    public List<GameObject> BFS_Exploration()
    {
        // the list will store all nodes and the queue will be used for the search
        List<GameObject> bfsList = new List<GameObject>();
        Queue<GameObject> bfsQueue = new Queue<GameObject>();

        // implementing the bfs search until the queue is empty
        bfsQueue.Enqueue(this.gameObject);
        while (bfsQueue.Count > 0)
        {
            currentNode = bfsQueue.Dequeue();
            bfsList.Add(currentNode);

            NodeController currentNodeC = currentNode.GetComponent<NodeController>();

            // visited is boolean variable, and all nodes in the game have this variable... it 
            // help me in the BFS search to check that this node is still not visited while the search is working
            if (!currentNodeC.visited)
            {
                currentNodeC.visited = true;

                // check that the current node is connected to the next node and that next one is not visited
                if (currentNodeC.canMoveUp && !currentNodeC.nodeUp.GetComponent<NodeController>().visited)
                    bfsQueue.Enqueue(currentNodeC.nodeUp);

                if (currentNodeC.canMoveRight && !currentNodeC.nodeRight.GetComponent<NodeController>().visited)
                    bfsQueue.Enqueue(currentNodeC.nodeRight);

                if (currentNodeC.canMoveDown && !currentNodeC.nodeDown.GetComponent<NodeController>().visited)
                    bfsQueue.Enqueue(currentNodeC.nodeDown);

                if (currentNodeC.canMoveLeft && !currentNodeC.nodeLeft.GetComponent<NodeController>().visited)
                    bfsQueue.Enqueue(currentNodeC.nodeLeft);
            }
        }
        // returning the list with out any duplicates
        return bfsList.Distinct().ToList();
    }

    public int palletsCount()
    {
        return BFS_Exploration().Count();
    }

}