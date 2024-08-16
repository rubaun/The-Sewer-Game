using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class Key : MonoBehaviour
{
    [Header("Código da chave")]
    [SerializeField] private int codigo;
    [Header("Chave usada na fase")]
    [SerializeField] private GameObject key;
    [Header("Porta ou Baú que abrirá")]
    [SerializeField] private GameObject portaBau;
    private SpriteRenderer sprite;
    [ColorUsage(true, true)]
    [Header("Cor da chave")]
    [SerializeField] private Color cor = Color.white;
    [Header("Chave da Interface do Usuário")]
    [SerializeField] private GameObject keyInterface;
    private GameObject player;
    private AudioSource audioKey;
    [SerializeField] private AudioClip audioClip;
    [Header("Animação da chave")]
    [SerializeField] private float velocidade = 1.5f;
    [SerializeField] private Vector3 inicialPos;
    [SerializeField] private Vector3 target;
    [SerializeField] private float distancia = 1.0f;



    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        keyInterface.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
        audioKey = GetComponent<AudioSource>();
        SetColors();
        inicialPos = this.gameObject.transform.position;
        target = inicialPos;
    }

    // Update is called once per frame
    void Update()
    {
        
        var step = velocidade * Time.deltaTime; 
        var difTarget = new Vector3(target.x, target.y - distancia, 0);
        transform.position = Vector3.MoveTowards(transform.position, difTarget, step);

        if (Vector3.Distance(transform.position, difTarget) < 0.001f)
        {
            target.y *= -1.0f;
        }
    }

    private void OnDisable()
    {
        audioKey.PlayOneShot(audioClip);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //audioKey.PlayOneShot(audioClip);
            ChaveParaInventario();
        }
    }

    public int GetCodigo()
    {
        return codigo;
    }

    public void ChaveParaInventario()
    {
        player.GetComponent<Soldier>().AddItem(key);
        keyInterface.SetActive(true);
        key.SetActive(false);
        SetColors();
    }

    private void SetColors()
    {
        sprite.color = cor;
        portaBau.GetComponent<SpriteRenderer>().color = cor;
        keyInterface.GetComponent<Image>().color = cor;
    }
}