using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
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
    [Header("Cena a ser carregada")]
    [SerializeField] private SceneAsset nextScene;
    private AudioSource audioPlayer;
    private Animator anim;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        GetComponent<SpriteRenderer>().color = corDoBau;
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && player.GetComponent<Soldier>().ConsultaChave())
        {
            if (cor == player.GetComponent<Soldier>().CorChave())
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
        }
    }

    //Tocar o som e depois de um tempo destruir o objeto
    private void DestuirComSom()
    {
        audioPlayer.PlayOneShot(audioClip);
        Destroy(gameObject, audioClip.length);
    }
}
