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



    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = cor;
        portaBau.GetComponent<SpriteRenderer>().color = cor;
        keyInterface.SetActive(false);
        keyInterface.GetComponent<Image>().color = cor;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.GetComponent<Soldier>().AddItem(key);
            Destroy(this.gameObject);
            keyInterface.SetActive(true);
        }
    }

    public int GetCodigo()
    {
        return codigo;
    }
}