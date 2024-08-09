using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Lancador : MonoBehaviour
{
    public GameObject fireball;
    public GameObject mira;
    private bool areaAtaque;
    private bool atirando;

    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("Fire", 1, 1);
        areaAtaque = false;
        atirando = true;
        StartCoroutine("Atira");
    }

    // Update is called once per frame
    void Update()
    {
        if(areaAtaque && atirando)
        {
            StopCoroutine("Atira");
            atirando = false;
        }
        else if(!areaAtaque && !atirando)
        {
            StartCoroutine("Atira");
            atirando = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            areaAtaque = true;
        }
    
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            areaAtaque = false;
        }
    
    }

    public void Fire()
    {
        Instantiate(fireball, mira.transform.position, Quaternion.identity);
    }

    IEnumerator Atira()
    {
        do
        {
            Fire();
            yield return new WaitForSeconds(Random.Range(1, 3));
        }while(atirando);
       
    }
}
