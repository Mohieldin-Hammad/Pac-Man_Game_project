using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriorityQueue : MonoBehaviour
{
    List<GameObject> queue;

    public PriorityQueue()
    {
        queue = new List<GameObject>();
    }

    public PriorityQueue(List<GameObject> list)
    {
        queue = list;
    }

    //----------------------------
    // not implemented yet..
    //----------------------------
    // here I want to make sure that the  getcomponent is returning the variable
    public GameObject Dequeue()
    {
        int index = 0;
        for (int i = 0; i < queue.Count; i++)
        {
            
        }

        GameObject node = queue[index];
        queue.RemoveAt(index);
        return node;
    }

    public void Enqueue(GameObject newItem)
    {
        queue.Add(newItem);
    }

    public int Length()
    {
        return queue.Count;
    }

    
}
