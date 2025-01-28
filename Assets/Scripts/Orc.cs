using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Orc : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float velocidadePatrulha;
    [SerializeField] private int vida;
    [SerializeField] private GameObject particulasMorte;
    [SerializeField] private bool encounter;
    private bool estahVulneravel = true;
    private bool estahVivo;
    private int direcao;
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

        if (collision.gameObject.CompareTag("Arrow") && estahVivo && estahVulneravel)
        {
            Dano(collision.gameObject.GetComponent<Arrow>().GetHit());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if(collision.gameObject.CompareTag("Player") && estahVivo)
        {
            encounter = true;
            EnemyFlip();
            StartCoroutine(Ataque());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && estahVivo)
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

        if (collision.gameObject.CompareTag("Player"))
        {
            StopCoroutine(Ataque());
            encounter = false;
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
            StartCoroutine("Vulneravel");
        }
    }

    IEnumerator Vulneravel()
    {
        estahVulneravel = false;
        yield return new WaitForSeconds(0.5f);
        estahVulneravel = true;
    }

    IEnumerator WaitToDeath()
    {
        anim.SetTrigger("Death");
        GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<Collider2D>().enabled = false;
        Instantiate(particulasMorte, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject.transform.parent.gameObject);
    }

    IEnumerator Ataque()
    {
        anim.SetBool("Ataque", true);

        yield return new WaitForSeconds(0.8f);

        if (encounter)
        {
            player.GetComponent<Soldier>().PerdeVida();
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
}
