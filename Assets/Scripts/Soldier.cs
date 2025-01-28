using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Soldier : MonoBehaviour
{
    private Animator animPlayer;
    private SpriteRenderer sprite;
    private Rigidbody2D rb;
    private PlayerSound playerSound;
    private Vector3 playerPosition;
    private float moveH;
    private float moveV;
    private bool estaPulando = false;
    private bool estaVivo = true;
    private bool estaEscada = false;
    private bool gameOver = false;
    private bool shot = false;
    private bool atirandoDir = true;
    private Vector3 posInicial;
    private bool estahVulneravel = true;
    [SerializeField] private List<GameObject> vidas = new List<GameObject>();
    private bool temChave = false;
    private string corChave;
    private Color corDaInterface;
    private GameObject chaveInterface;
    [SerializeField] private float tempoInvulneravel = 1.5f;
    [SerializeField] private int velocidade;
    [SerializeField] private int vida = 3;
    [SerializeField] private string faseAtual;
    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject mira;

    
    // Start is called before the first frame update
    void Start()
    {
        animPlayer = GetComponent<Animator>();
        chaveInterface = GameObject.Find("Key");
        chaveInterface.SetActive(false);
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        playerSound = GetComponent<PlayerSound>();
        posInicial = transform.position;
        faseAtual = SceneManager.GetActiveScene().name;
        
        if(SaveSystem.SaveExists() && SaveSystem.GameLoaded())
        {
            GameData data = SaveSystem.Load();
            VidasLoading(data.vida);
            transform.position = new Vector3(data.checkpointPos[0], data.checkpointPos[1], 0);

            if(data.checkpoint)
            {
                FindAnyObjectByType<Checkpoint>().GetComponent<ParticleSystem>().Stop();
            }
        }
        else
        {
            VidasIniciais();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Animação Run para a direita e esqueda
        if (Input.GetKey(KeyCode.D) && moveH > 0)
        {
            ViraDireita();
            animPlayer.SetLayerWeight(1, 1);
        }
        else if (Input.GetKey(KeyCode.A) && moveH < 0)
        {
            ViraEsquerda();
            animPlayer.SetLayerWeight(1, 1);
        }
        else
        {
            animPlayer.SetLayerWeight(1, 0);
        }

        //Animação Pular
        if (estaPulando)
        {
            animPlayer.SetLayerWeight(6, 1);
        }
        else
        {
            animPlayer.SetLayerWeight(6, 0);
        }

        //Pular
        if (Input.GetKeyDown(KeyCode.Space) && !estaPulando)
        {
            if (rb.velocity.y == 0)
            {
                playerSound.JumpSound();
                rb.AddForce(transform.up * velocidade, ForceMode2D.Impulse);
                estaPulando = true;
            }
        }

        //Ataque Espada
        if (Input.GetMouseButtonDown(0))
        {
            animPlayer.SetLayerWeight(2, 1);
            playerSound.Sword1Sound();
        }
        else
        {
            animPlayer.SetLayerWeight(2, 0);
        }

        //Arco
        if (Input.GetMouseButtonDown(1))
        {
            animPlayer.SetLayerWeight(3, 1);
            ArrowShot();
        }
        else
        {
            animPlayer.SetLayerWeight(3, 0);
            shot = false;
        }

        //Espada Pulo
        if (Input.GetMouseButtonDown(2))
        {
            animPlayer.SetLayerWeight(4, 1);
            playerSound.Sword2Sound();
        }
        else
        {
            animPlayer.SetLayerWeight(4, 0);
        }
    }


    void FixedUpdate()
    {
        moveH = Input.GetAxis("Horizontal");
        moveV = Input.GetAxis("Vertical");

        if (estaEscada)
        {
            playerPosition = transform.position += new Vector3(0, moveV * velocidade * Time.deltaTime, 0);
        }
        else
        {
            playerPosition = transform.position += new Vector3(moveH * velocidade * Time.deltaTime, 0, 0);
        }


    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Chão"))
        {
            estaPulando = false;
        }

        if (other.gameObject.CompareTag("Buraco"))
        {
            Morte();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Escada"))
        {
            VerificaEstaEscada();
        }

        if (other.gameObject.CompareTag("Inimigo"))
        {
            Debug.Log("Inimigo");

            if (Input.GetMouseButton(0))
            {
                other.gameObject.GetComponent<Orc>().Dano(Ataque());
            }
            else if (Input.GetMouseButton(2))
            {
                other.gameObject.GetComponent<Orc>().Dano(AtaqueEspecial());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Escada"))
        {

            VerificaEstaEscada();
        }
    }

    private int Ataque()
    {
        return 10;
    }

    private int AtaqueEspecial()
    {
        return Random.Range(15, 25);
    }

    public bool VerificaSePlayerEstaVivo()
    {
        return estaVivo;
    }

    public Vector3 PlayerPositionInicial()
    {
        return posInicial;
    }

    public void Morte()
    {

        estaVivo = false;

        StartCoroutine("AnimaMorte");
    }

    private void VidasIniciais()
    {
        vida = 3;
        vidas.Add(GameObject.Find("Heart1"));
        vidas.Add(GameObject.Find("Heart2"));
        vidas.Add(GameObject.Find("Heart3"));
    }

    public void VidasLoading(int vidasI)
    {
        if(vidasI == 3)
        {
            vida = 3;
            vidas.Add(GameObject.Find("Heart1"));
            vidas.Add(GameObject.Find("Heart2"));
            vidas.Add(GameObject.Find("Heart3"));
        }
        else if(vidasI == 2)
        {
            vida = 2;
            vidas.Add(GameObject.Find("Heart1"));
            vidas.Add(GameObject.Find("Heart2"));
            GameObject.Find("Heart3").SetActive(false);
        }
        else if (vidasI == 1)
        {
            vida = 1;
            vidas.Add(GameObject.Find("Heart1"));
            GameObject.Find("Heart2").SetActive(false);
            GameObject.Find("Heart3").SetActive(false);
        }

    }

    public void PerdeVida()
    {
        if (estahVulneravel && vida > 0)
        {
            vida--;

            if (vida == 2)
            {
                Destroy(vidas[2]);
                animPlayer.SetLayerWeight(5, 1);
                StartCoroutine("Vulneravel");
            }
            else if (vida == 1)
            {
                Destroy(vidas[1]);
                animPlayer.SetLayerWeight(5, 1);
                StartCoroutine("Vulneravel");
            }
            else if (vida == 0)
            {
                Destroy(vidas[0]);
                animPlayer.SetLayerWeight(5, 1);
                Morte();
            }

            animPlayer.SetLayerWeight(5, 0);
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
                Instantiate(arrow, mira.transform.position, Quaternion.Euler(0, 180f, 0)).GetComponent<Arrow>().ArrowLeft();
            }

            StartCoroutine("DestroyArrow");
        }
    }

    IEnumerator DestroyArrow()
    {
        shot = true;
        yield return new WaitForSeconds(1.0f);
        shot = false;
    }

    private void VerificaEstaEscada()
    {
        estaEscada = !estaEscada;
        if (estaEscada)
        {
            rb.Sleep();
        }
        else
        {
            rb.WakeUp();
        }
    }

    private IEnumerator Vulneravel()
    {
        estahVulneravel = false;
        yield return new WaitForSeconds(tempoInvulneravel);
        estahVulneravel = true;
    }

    IEnumerator AnimaMorte()
    {
        animPlayer.SetTrigger("Death");

        playerSound.DeathSound();

        yield return new WaitForSeconds(2.0f);

        Destroy(this.gameObject);
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

    public int GetVida()
    {
        return vida;
    }

    public string GetFase()
    {
        return SceneManager.GetActiveScene().name;
    }

    //Sitema de chave
    public void AddChave(string cor, Color corDaChave)
    {
        temChave = true;
        corChave = cor;
        corDaInterface = corDaChave;
        chaveInterface.SetActive(true);
        chaveInterface.GetComponent<Image>().color = corDaInterface;
    }

    public bool ConsultaChave()
    {
        if (temChave)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public string CorChave()
    {
        return corChave;
    }

    public void RemoverChave()
    {
        temChave = false;
        corChave = "";
        chaveInterface.SetActive(false);
    }
}

    