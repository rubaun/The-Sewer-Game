using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Orc : MonoBehaviour
{
    [SerializeField] private float velocidadePatrulha;
    [SerializeField] private int direcao;
    [SerializeField] private BoxCollider2D triggerLeft;
    [SerializeField] private BoxCollider2D triggerRight;
    [SerializeField] private int vida;
    private bool encounter;
    private Animator anim;
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        triggerLeft = GetComponent<BoxCollider2D>();
        triggerRight = GetComponent<BoxCollider2D>();
        direcao = 1;
        velocidadePatrulha = 0.5f;
        encounter = false;
        vida = 100;
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    private void FixedUpdate()
    {
        if (!encounter)
        {
            Patrulha();
        }
    }

    private void Patrulha()
    {
        Vector3 dirEnemy = new Vector3(direcao, 0, 0);
        transform.Translate(dirEnemy * velocidadePatrulha * Time.deltaTime);
        anim.SetLayerWeight(1, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Patrulha"))
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
            anim.SetLayerWeight(2,1);
            encounter = true;
        }

        if(collision.gameObject.CompareTag("Arrow"))
        {
            Dano(collision.gameObject.GetComponent<Arrow>().GetHit());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            anim.SetLayerWeight(2, 0);
            encounter = false;
        }
    }

    public void Dano(int dano)
    {
        vida -= dano;
        anim.SetLayerWeight(4, 1);

        if (vida <= 0)
        {
            anim.SetLayerWeight(4, 0);
            anim.SetTrigger("Death");
            StartCoroutine("WaitToDeath");
        }
        else
        {
            anim.SetLayerWeight(4, 0);
        }
    }

    IEnumerable WaitToDeath()
    {
        yield return new WaitForSeconds(2.5f);
        Destroy(this.gameObject);
    }

}
