using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSegue : MonoBehaviour
{
    public GameObject player;
    public Vector3 posInicial;
    private bool voltar;
    public float limiteX1;
    public float limiteX2;
    public float limiteY1;
    public float limiteY2;

    void Start()
    {
        posInicial = transform.position;
        voltar = true;
    }

    void Update()
    {
        if(player != null && !voltar)
        {
            StartCoroutine("Contador");
        }

        if(player != null && voltar)
        {
            if(player.transform.position.x >= limiteX1 && 
                player.transform.position.y <= limiteY1 && 
                    player.transform.position.x <= limiteX2 && 
                        player.transform.position.y >= limiteY2)
            {
                transform.position = new Vector3(
                                    player.transform.position.x,
                                    player.transform.position.y,
                                    -3);
            }
        }

        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            voltar = false;
        }        
    }

    IEnumerator Contador()
    {
        yield return new WaitForSeconds(0.1f);
        transform.position = posInicial;
        voltar = true;
    }
}
