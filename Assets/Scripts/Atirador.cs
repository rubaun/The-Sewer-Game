using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atirador : MonoBehaviour
{
    public GameObject fireball;
    public GameObject mira;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Fire", 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire()
    {
        Instantiate(fireball, mira.transform.position, Quaternion.identity);
    }
}
