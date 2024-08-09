using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaBau : MonoBehaviour
{
    [Header("Código da chave")]
    [SerializeField] private int codigo;
    private Animator anim;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && player.GetComponent<Soldier>().ConsultaChave(codigo))
        {
            
            
                anim.SetLayerWeight(1, 1);
            
        }
    }
}
