using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

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



    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        keyInterface.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
        audioKey = GetComponent<AudioSource>();
        SetColors();
    }

    // Update is called once per frame
    void Update()
    {
        
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