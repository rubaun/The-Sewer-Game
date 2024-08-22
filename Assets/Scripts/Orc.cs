using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc : MonoBehaviour
{
    public Animator anim;
    public SpriteRenderer sprite;
    public Vector3 pontoA;
    public Vector3 pontoB;
    public float velocidadePatrulha;
    public float velocidadePersegue;
    public float distanciaAtaque;
    public float distanciaDetecao;
    public GameObject jogador;
    public Vector3 pontoAtual;
    public Rigidbody2D rb;
    public bool persegue;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        jogador = GameObject.FindGameObjectWithTag("Player");
        pontoAtual = pontoA;
    }

    // Update is called once per frame
    void Update()
    {
        float distanciaPlayer = Vector3.Distance(transform.position, jogador.transform.position);

        if(distanciaPlayer < distanciaDetecao)
        {
            persegue = true;
        }
        else
        {
            persegue = false;
        }

        if(persegue && distanciaPlayer < distanciaAtaque)
        {
            ModoAtaque();
        }
        else if(persegue)
        {
            ModoPersegue();
        }
        else
        {
            ModoPatrulha();
        }
    }

    private void ModoAtaque()
    {
        rb.velocity = Vector3.zero;
    }

    private void ModoPersegue()
    {
        Vector3 direcaoMovimento = (jogador.transform.position - transform.position).normalized;
        rb.velocity = new Vector3(direcaoMovimento.x * velocidadePersegue, rb.velocity.y, 0);
    }

    private void ModoPatrulha()
    {
        Vector3 target = new Vector3(0,0,0);

        transform.position = Vector3.MoveTowards(transform.position, target, velocidadePatrulha * Time.deltaTime);

        if(Vector3.Distance(transform.position, target) < 0.001f)
        {
            if(!persegue)
            {
                target = pontoB;
            }
            else
            {
                target = pontoA;
            }
        }
    }


}
