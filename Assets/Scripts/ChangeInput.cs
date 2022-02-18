using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeInput : MonoBehaviour
{
    public Sausage sausage;
    private bool active;
    public GameObject[] sausageArray;
    public Material blue;
    private void OnTriggerEnter(Collider other)
    {
        if (!active)
        {
            for (int i = 0; i < sausageArray.Length; i++)
            {
                sausageArray[i].GetComponent<MeshRenderer>().material = blue;
            }
            Sausage.reversed = true;
            active = true;
        }
    }
}
