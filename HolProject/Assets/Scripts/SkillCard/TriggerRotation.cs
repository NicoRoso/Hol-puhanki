using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRotation : MonoBehaviour
{
    public void RotateCard()
    {
        foreach (Animator animator in GetComponentsInChildren<Animator>()) 
        {
            animator.SetTrigger("Rotate"); 
        }
    }
}
