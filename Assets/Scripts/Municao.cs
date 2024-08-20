using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Municao : MonoBehaviour
{
    public float velocidade;

    void Update()
    {
        transform.Translate(new Vector3(velocidade * Time.deltaTime, 0) , transform);

        //Destruir depois de 2s
        Invoke("Destruir", 2.0f);
    }

    private void Destruir()
    {
        Destroy(this.gameObject);
    }
}
