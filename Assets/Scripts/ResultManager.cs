using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultManager : MonoBehaviour
{
    public GameObject resultView;
    void Update()
    {
        string result = GameSpecification.Result;
        if (result == "WIN")
        {
            resultView.transform.Find("Winner").gameObject.SetActive(true);
            resultView.transform.Find("GameOver").gameObject.SetActive(false);
        }
        else if (result == "LOST")
        {
            resultView.transform.Find("GameOver").gameObject.SetActive(true);
            resultView.transform.Find("Winner").gameObject.SetActive(false);
        }
    }
}
