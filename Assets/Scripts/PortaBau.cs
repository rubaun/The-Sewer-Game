using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEditor;

public class PortaBau : MonoBehaviour
{
    [Header("Cor que Abre")]
    [SerializeField] private Color corDoBau = Color.white;
    [SerializeField] private string cor;
    [SerializeField] private AudioClip audioClip;
    [Header("Cor da Chave dentro do Baú")]
    [SerializeField] private Color corDaChave = Color.white;
    [SerializeField] private string corChave;
    [Header("Marque se for uma porta")]
    [SerializeField] private bool isPorta;
    [SerializeField] private GameObject spritePorta1;
    [SerializeField] private GameObject spritePorta2;
    [Header("Cena a ser carregada")]
    [SerializeField] private GameObject nextScene;
    private AudioSource audioPlayer;
    private Animator anim;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();

        if(!isPorta)
        {
            GetComponent<SpriteRenderer>().color = corDoBau;
        }
        else
        {
            spritePorta1.GetComponent<SpriteRenderer>().color = corDoBau;
            spritePorta2.GetComponent<SpriteRenderer>().color = corDoBau;
        }
            
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && player.GetComponent<Soldier>().ConsultaChave())
        {
            if(cor == player.GetComponent<Soldier>().CorChave())
            {
                audioPlayer.PlayOneShot(audioClip);

                player.GetComponent<Soldier>().RemoverChave();

                if (anim != null)
                {
                    anim.SetLayerWeight(1, 1);
                }

                if (isPorta)
                {
                    SceneManager.LoadScene(nextScene.name);
                }
                else
                {
                    player.GetComponent<Soldier>().AddChave(corChave, corDaChave);
                    DestuirComSom();
                }
            }
            else if(cor == "")
            {
                DestuirComSom();
            }
            
        }
        else if(collision.gameObject.CompareTag("Player") && isPorta && cor == "")
        {
            SceneManager.LoadScene(nextScene.name);
        }
        else if(collision.gameObject.CompareTag("Player") && !isPorta && cor == "")
        {
            DestuirComSom();
        }
    }

    //Tocar o som e depois de um tempo destruir o objeto
    private void DestuirComSom()
    {
        audioPlayer.PlayOneShot(audioClip);
        Destroy(gameObject, audioClip.length);
    }
}
