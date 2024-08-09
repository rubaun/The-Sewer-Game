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
    public float moveV;
    public int velocidade;
    public bool estaPulando = false;
    public bool estaVivo = true;
    public bool estaEscada = false;
    private bool gameOver = false;
    private bool shot = false;
    private bool atirandoDir = true;
    private Vector3 posInicial;
    public int vida = 3;
    public GameObject arrow;
    public GameObject mira;
    public List<GameObject> vidas = new List<GameObject>();
    [Header("Inventário")]
    [SerializeField] private List<GameObject> inventario = new List<GameObject>();
    

    
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
            ViraDireita();
        }
        else if(Input.GetKey(KeyCode.A))
        {
            ViraEsquerda();
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
        moveV = Input.GetAxis("Vertical");
        
        if(estaEscada)
        {
            transform.position += new Vector3(0, moveV * velocidade * Time.deltaTime, 0);
        }
        else
        {
            transform.position += new Vector3(moveH * velocidade * Time.deltaTime, 0, 0);
        }
        
    
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

    private void OnTriggerStay2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Escada"))
        {
            VerificaEstaEscada();
        }    
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Escada"))
        {
            
            VerificaEstaEscada();
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
            
            if(atirandoDir)
            {
                Instantiate(arrow, mira.transform.position, Quaternion.identity).GetComponent<Arrow>().ArrowRight();
            }
            else
            {
                Instantiate(arrow, mira.transform.position, Quaternion.Euler(0,180f,0)).GetComponent<Arrow>().ArrowLeft();
            }
                       
            shot = true;
        }
    }

    private void VerificaEstaEscada()
    {
        estaEscada = !estaEscada;
        if(estaEscada)
        {
            rb.Sleep();
        }
        else
        {
            rb.WakeUp();
        }
    }

    IEnumerator AnimaMorte()
    {
        animPlayer.SetTrigger("Death");

        playerSound.DeathSound();

        yield return new WaitForSeconds(2.0f);

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

        if(estaVivo)
        {
            StopCoroutine("AnimaMorte");
        }
    }    

    private void ViraDireita()
    {
        sprite.flipX = false;
        atirandoDir = true;
    }   

    private void ViraEsquerda()
    {
        sprite.flipX = true;
        atirandoDir = false;
    }    

    public void AddItem(GameObject item)
    {
        inventario.Add(item);
    }

    public bool ConsultaChave(int codigo)
    {
        foreach(GameObject i in inventario)
        {
            if(i.GetComponent<Key>().GetCodigo() == codigo)
            {
                return true;
            }
        }
        return false;
    }

    public void RemoveItem(string item)
    {
        foreach(GameObject i in inventario)
        {
            if(i.name == item)
            {
                inventario.Remove(i);
                break;
            }
        }
    }
}
