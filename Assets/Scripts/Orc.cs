using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Orc : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float velocidadePatrulha;
    [SerializeField] private int vida;
    private bool estahVivo;
    private int direcao;
    private bool encounter;
    private Animator anim;
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        direcao = 1;
        velocidadePatrulha = 0.5f;
        encounter = false;
        vida = 100;
        estahVivo = true;
    }

    private void FixedUpdate()
    {
        if (!encounter && estahVivo)
        {
            Patrulha();
        }
    }

    private void Patrulha()
    {
        Vector3 dirEnemy = new Vector3(direcao, 0, 0);
        transform.Translate(dirEnemy * velocidadePatrulha * Time.deltaTime);
        anim.SetBool("Andando", true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Patrulha") && estahVivo)
        {
            if (direcao == 1)
            {
                direcao = -1;
                sprite.flipX = true;
            }
            else
            {
                direcao = 1;
                sprite.flipX = false;
            }
        }

        if (collision.gameObject.CompareTag("Player") && estahVivo)
        {
            encounter = true;
            EnemyFlip();
            StartCoroutine(Ataque());
        }

        if (collision.gameObject.CompareTag("Arrow") && estahVivo)
        {
            Dano(collision.gameObject.GetComponent<Arrow>().GetHit());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            LevarDanoSoldier();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && estahVivo)
        {
            StopCoroutine(Ataque());
            encounter = false;

            if (direcao == 1)
            {
                direcao = -1;
                sprite.flipX = true;
            }
            else
            {
                direcao = 1;
                sprite.flipX = false;
            }
        }
    }

    public void Dano(int dano)
    {
        if (vida <= 0)
        {
            estahVivo = false;
            StartCoroutine(WaitToDeath());
        }
        else
        {
            vida -= dano;
            anim.SetTrigger("Hit");
        }
    }

    IEnumerator WaitToDeath()
    {
        anim.SetTrigger("Death");
        GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject.transform.parent.gameObject);
    }

    IEnumerator Ataque()
    {
        yield return new WaitForSeconds(1.5f);

        anim.SetBool("Ataque", true);

        if(encounter)
        {
            player.GetComponent<Soldier>().Morte();
        }

        StartCoroutine(Ataque());
    }

    private void EnemyFlip()
    {
        if (player.position.x < transform.position.x)
        {
            direcao = 1;
            sprite.flipX = true;
        }
        else
        {
            direcao = -1;
            sprite.flipX = false;
        }
    }

    private void LevarDanoSoldier()
    {
        if (Input.GetMouseButtonDown(0))
        {
            encounter = false;
            Dano(10);
        }
    }
}
