using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spyke : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            GameEvents.onGameover.Invoke();
        }
    }

}
