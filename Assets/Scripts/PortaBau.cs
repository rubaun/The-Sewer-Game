using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEditor;

public class PortaBau : MonoBehaviour
{
    [Header("Código da chave")]
    [SerializeField] private int codigo;
    [SerializeField] private AudioClip audioClip;
    [Header("Marque se for uma porta")]
    [SerializeField] private bool isPorta;
    [SerializeField] private SceneAsset nextScene;
    private AudioSource audioPlayer;
    private Animator anim;
    private GameObject player;
    private GameObject chaveInterface;
    [SerializeField] private List<GameObject> itens = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        chaveInterface = GameObject.FindGameObjectWithTag("Key");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && player.GetComponent<Soldier>().ConsultaChave(codigo))
        {
            audioPlayer.PlayOneShot(audioClip);
            if (anim != null)
            {
                anim.SetLayerWeight(1, 1);
            }
            player.GetComponent<Soldier>().RemoverChave(codigo);
            chaveInterface.SetActive(false);
            if (isPorta)
            {
                SceneManager.LoadScene(nextScene.name);
            }
            foreach (GameObject item in itens)
            {
                if(item.GetComponent<Key>())
                {
                    item.gameObject.SetActive(true);
                    item.GetComponent<Key>().ChaveParaInventario();
                }
                else
                {
                    player.GetComponent<Soldier>().AddItem(item);
                }
                
            }
            
        }
    }
}
