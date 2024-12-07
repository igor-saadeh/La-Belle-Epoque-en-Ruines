using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        animator.SetTrigger("TouchingPlayer");
        GameEvents.onCollect.Invoke();
    }

    void OnAnimationEndDestroy()
    {
        Destroy(gameObject);
    }
}
