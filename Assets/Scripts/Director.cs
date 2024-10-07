using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director : MonoBehaviour
{
    [SerializeField] private Soldier soldier;

    // Start is called before the first frame update
    void Start()
    {
        soldier = FindObjectOfType<Soldier>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
