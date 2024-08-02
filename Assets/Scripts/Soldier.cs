using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    public Animator animPlayer;
    public SpriteRenderer sprite;
    public Rigidbody2D rb;
    private PlayerSound playerSound;
    public float moveH;
    public int velocidade;
    public bool estaPulando = false;
    public bool estaVivo = true;
    private bool gameOver = false;
    private bool shot = false;
    private Vector3 posInicial;
    public int vida = 3;
    public GameObject arrow;
    public GameObject mira;
    public List<GameObject> vidas = new List<GameObject>(); 
    

    
    // Start is called before the first frame update
    void Start()
    {
        animPlayer = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        playerSound = GetComponent<PlayerSound>();
        posInicial = transform.position;
        Vidas();
    }

    // Update is called once per frame
    void Update()
    {
        //Animação Run para a direita e esqueda
        if(Input.GetKey(KeyCode.D))
        {
            sprite.flipX = false;
        }
        else if(Input.GetKey(KeyCode.A))
        {
            sprite.flipX = true;
        }

        if(moveH > 0)
        {
            animPlayer.SetLayerWeight(1,1);
        }
        else if(moveH < 0)
        {
            animPlayer.SetLayerWeight(1,1);
        }
        else
        {
            animPlayer.SetLayerWeight(1,0);
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
            playerSound.JumpSound();
            rb.AddForce(transform.up * velocidade, ForceMode2D.Impulse);
            estaPulando = true;
        }

        //Ataque Espada
        if(Input.GetMouseButton(0))
        {
            animPlayer.SetLayerWeight(2,1);
            playerSound.Sword1Sound();
        }
        else
        {
            animPlayer.SetLayerWeight(2,0);
        }

        //Arco
        if(Input.GetMouseButton(1))
        {
            animPlayer.SetLayerWeight(3,1);
            ArrowShot();
        }
        else
        {
            animPlayer.SetLayerWeight(3,0);
            shot = false;
        }
        
        //Espada Pulo
        if(Input.GetMouseButton(2))
        {
            animPlayer.SetLayerWeight(4,1);
            playerSound.Sword2Sound();
        }
        else
        {
            animPlayer.SetLayerWeight(4,0);
        }
    }

    void FixedUpdate() 
    {
        moveH = Input.GetAxis("Horizontal");        
        
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

        StartCoroutine("AnimaMorte");
        
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

    private void ArrowShot()
    {
        if(!shot)
        {
            playerSound.ArrowSound();
            Instantiate(arrow, mira.transform.position, Quaternion.identity);
            shot = true;
        }
    }

    IEnumerator AnimaMorte()
    {
        animPlayer.SetTrigger("Death");

        playerSound.DeathSound();

        yield return new WaitForSeconds(2.0f);
        
        Destroy(this.gameObject);

        if(estaVivo)
        {
            StopCoroutine("AnimaMorte");
        }
    }           
}
