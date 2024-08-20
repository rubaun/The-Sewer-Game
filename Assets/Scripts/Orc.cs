using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc : MonoBehaviour
{
    public Animator anim;
    public SpriteRenderer sprite;
    public bool patrulhaMod;
    public bool attackMod;
    public bool estaVivo;
    public bool estaAndando;
    public int velocidade;
    public Vector3 inicialPos;
    public Vector3 target;
    public float distancia;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        inicialPos = this.gameObject.transform.position;
        target = inicialPos;
    }

    // Update is called once per frame
    void Update()
    {
        ModoPatrulha();
    }

    private void ModoPatrulha()
    {
        estaAndando = true;

        var step = velocidade * Time.deltaTime;

        var difTarget = new Vector3(target.x - distancia, target.y, 0);
        
        transform.position = Vector3.MoveTowards(transform.position, difTarget, step);

        if (Vector3.Distance(transform.position, difTarget) < 0.001f)
        {
            target.x *= -1.0f;
        }
    }

    private void ModoAttack()
    {

    }

    private void ModoDeAnimacao()
    {
        if(!estaAndando)
        {
            anim.SetLayerWeight(1,0);
            anim.SetLayerWeight(2,0);
            anim.SetLayerWeight(3,0);
            anim.SetLayerWeight(4,0);
        }
        else if(estaAndando)
        {
            anim.SetLayerWeight(1,1);
        }

    }

    private bool EstaVivo()
    {
        return estaVivo;
    }
}
