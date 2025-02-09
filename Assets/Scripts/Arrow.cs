using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed;
    public int hit;
    private bool direcaoDir;
    

    // Update is called once per frame
    void FixedUpdate()
    {
        if(direcaoDir)
        {
            transform.position += new Vector3(1 * speed * Time.deltaTime, 0, 0);
        }
        else if(!direcaoDir)
        {
            transform.position += new Vector3(-1 * speed * Time.deltaTime, 0, 0);
        }

        //transform.position += new Vector3(1 * speed * Time.deltaTime, 0, 0);

        StartCoroutine("DestroyAfter");
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(!other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Chão") 
            || other.gameObject.CompareTag("TriggerLeft") || other.gameObject.CompareTag("TriggerRight"))
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator DestroyAfter()
    {
        yield return new WaitForSeconds(2.0f);
        Destroy(this.gameObject);
    }

    public void ArrowRight()
    {
        direcaoDir = true;
    }

    public void ArrowLeft()
    {
        direcaoDir = false;
    }

    public int GetHit()
    {
        return hit;
    }
    
}
