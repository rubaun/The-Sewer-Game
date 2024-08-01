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
    private bool gameOver = false;
    private Vector3 posInicial;
    public int vida = 3;
    public List<GameObject> vidas = new List<GameObject>(); 
    

    
    // Start is called before the first frame update
    void Start()
    {
        animPlayer = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        posInicial = transform.position;
        Vidas();
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
            Morte();
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
        
        
        estaVivo = false;
        
        if(vida == 3)
        {
            vida--;
            vidas[2].gameObject.SetActive(false);
        }
        else if(vida == 2)
        {
            vida--;
            vidas[1].gameObject.SetActive(false);
        }
        else if(vida == 1)
        {
            vidas[0].gameObject.SetActive(false);
            sprite.enabled = false;
            gameOver = true;
        }

        Destroy(this.gameObject);
        
    }

    private void Vidas()
    {
        vidas.Add(GameObject.Find("Heart1"));
        vidas.Add(GameObject.Find("Heart2"));
        vidas.Add(GameObject.Find("Heart3"));

        if(vidas[1] == null)
        {
            vida = 1;
        }
        else if(vidas[2] == null)
        {
            vida = 2;
        }
    }

    public bool GameOver()
    {
        return gameOver;
    }

}
