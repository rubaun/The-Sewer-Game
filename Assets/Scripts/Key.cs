using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour
{
    private SpriteRenderer sprite;
    [ColorUsage(true, true)]
    [Header("Nomeie a Cor da chave")]
    [SerializeField] private Color cor = Color.white;
    [SerializeField] private string CorChave;
    private GameObject player;
    private AudioSource audioKey;
    [SerializeField] private AudioClip audioClip;



    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = cor;
        player = GameObject.FindGameObjectWithTag("Player");
        audioKey = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            audioKey.PlayOneShot(audioClip);
            ChaveParaInventario();
        }
    }

    public void ChaveParaInventario()
    {
        player.GetComponent<Soldier>().AddChave(CorChave, cor);
        DestuirComSom();
    }

    //Tocar o som e depois de um tempo destruir o objeto
    private void DestuirComSom()
    {
        audioKey.PlayOneShot(audioClip);
        Destroy(gameObject, audioClip.length);
    }

}