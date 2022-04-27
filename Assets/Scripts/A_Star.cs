using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_Star : MonoBehaviour
{
    
    public List<GameObject> getPath(GameObject start, GameObject target)
    {
        
        

        List<GameObject> path = new List<GameObject>();
        List<Node> priorityQueue = new List<Node>();
        priorityQueue.Add(new Node(start, target));

        Node current_Node;
        GameObject parentNode;


        while (true)
        {

            // deueue the node with specific priority
            current_Node = GetNodeWithPriority(priorityQueue);


            // if the current node is the target it will the loop through the steps count and 
            // will add all nodes to the path list to be returned
            if (current_Node.currentNode == target)
            {
                while (current_Node != null)
                {
                    path.Insert(0, current_Node.currentNode);
                    current_Node = current_Node.parent;

                }
                break;
            }
            
            NodeController currentNodeC = current_Node.currentNode.GetComponent<NodeController>();

            // This condition will avoid calling the parent.currentNode if the node is the initial one 
            // which it's parent is null and this will lead to error
            if (current_Node.parent == null)
            {
                parentNode = current_Node.currentNode;
            }
            else
            {
                parentNode = current_Node.parent.currentNode;
            }

            // check that the current node is connected to the next node and this next one is the 
            // parent, means it's not checked before
            if (currentNodeC.canMoveUp && (currentNodeC.nodeUp != parentNode))
                priorityQueue.Add(new Node(currentNodeC.nodeUp, target, current_Node));


            if (currentNodeC.canMoveRight && (currentNodeC.nodeRight != parentNode))
            priorityQueue.Add(new Node(currentNodeC.nodeRight, target, current_Node));
                


            //For debuging------------------------------------
            if (currentNodeC.canMoveDown && (currentNodeC.nodeDown != parentNode))
                priorityQueue.Add(new Node(currentNodeC.nodeDown, target, current_Node));

            if (currentNodeC.canMoveLeft && (currentNodeC.nodeLeft != parentNode))
                priorityQueue.Add(new Node(currentNodeC.nodeLeft, target, current_Node));

        }
       return path;
    }


    // the priority here of the dequeue is node with least cost
    private Node GetNodeWithPriority(List<Node> queue)
    {
        int index = 0;
        for (int i = 0; i < queue.Count; i++)
        {
            if (queue[i].cost < queue[index].cost)
            {
                index = i;
            }
        }
        Node selectedNode = queue[index];
        queue.RemoveAt(index);
        return selectedNode;
    }
}




class Node
{
    public GameObject currentNode;
    public GameObject target;
    public Node parent;
    public float cost = 0;
    public int steps_from_start;

    // start node creation
    public Node(GameObject current_Node, GameObject goal)
    {
        currentNode = current_Node;
        target = goal;
        parent = null;
        // calculating the A* alogorithm cost for the initial node
        steps_from_start = 0;
        cost = steps_from_start + manhattan(currentNode, target);    
    }

    // node connected with parent
    public Node(GameObject current_Node, GameObject goal, Node p)
    {
        currentNode=current_Node;
        target=goal;
        parent = p;
        // calculating the A* alogorithm cost for the current node
        steps_from_start = parent.steps_from_start + 1;
        cost = steps_from_start + manhattan(currentNode, target);
    }

    // manhattan is calculating the distance from the node to the goal 
    // and the role is equal |X_goal - X_node| + |Y_goal + Y_node| 
    public float manhattan(GameObject currentNode, GameObject target)
    {
        return Mathf.Abs(currentNode.transform.position.x - target.transform.position.x) + Mathf.Abs(currentNode.transform.position.y - target.transform.position.y);
    }
}