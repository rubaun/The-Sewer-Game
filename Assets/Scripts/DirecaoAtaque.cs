using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirecaoAtaque : MonoBehaviour
{
    [SerializeField] private int direcao;
    [SerializeField] private BoxCollider2D triggerLeft;
    [SerializeField] private BoxCollider2D triggerRight;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(triggerLeft.IsTouching(collision))
        {
            Debug.Log("Colisão com triggerLeft");
        }
        else if (triggerRight.IsTouching(collision))
        {
            Debug.Log("Colisão com triggerRight");
        }
    }
}
