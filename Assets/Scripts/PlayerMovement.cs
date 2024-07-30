using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator animPlayer;
    public SpriteRenderer sprite;
    public Rigidbody2D rb;
    public float moveH;
    public float moveV;
    public int velocidade;
    public bool estaPulando = false;
    public bool estaVivo = true;
    private Vector3 posInicial;
    public int vida = 3;
    public GameObject[] vidas = new GameObject[3]; 
    

    
    // Start is called before the first frame update
    void Start()
    {
        animPlayer = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        posInicial = transform.position;
        vidas = GameObject.FindGameObjectsWithTag("Heart");
    }

    // Update is called once per frame
    void Update()
    {
        //Animação Run para a direita e esqueda
        if(Input.GetKey(KeyCode.D))
        {
            animPlayer.SetLayerWeight(2,1);
            sprite.flipX = false;
        }
        else if(Input.GetKey(KeyCode.A))
        {
            animPlayer.SetLayerWeight(2,1);
            sprite.flipX = true;
        }
        else
        {
            animPlayer.SetLayerWeight(2,0);
        }

        //Animação Pular
        if(estaPulando)
        {
            animPlayer.SetLayerWeight(1,1);
        }
        else
        {
            animPlayer.SetLayerWeight(1,0);
        }

        //Pular
        if(Input.GetKeyDown(KeyCode.Space) && !estaPulando)
        {
            rb.AddForce(transform.up * velocidade, ForceMode2D.Impulse);
            estaPulando = true;
        }
        
    }

    void FixedUpdate() 
    {
        moveH = Input.GetAxis("Horizontal");
        moveV = Input.GetAxis("Vertical");
        
        
        transform.position += new Vector3(moveH * velocidade * Time.deltaTime, 0, 0);
        

    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Chão"))
        {
            estaPulando = false;
        }

        if(other.gameObject.CompareTag("Buraco"))
        {
            estaVivo = false;
            Morte();
            Destroy(this.gameObject);
        }
    }

    public bool VerificaSePlayerEstaVivo()
    {
        return estaVivo;
    }

    public Vector3 PlayerPositionInicial()
    {
        return posInicial;
    }

    private void Morte()
    {
        vida--;
        switch(vida)
        {
            case 2:
            vidas[0].gameObject.SetActive(false);
            break;
            case 1:
            vidas[1].gameObject.SetActive(false);
            break;
            case 0:
            vidas[2].gameObject.SetActive(false);
            break;   
        }
    }


}
