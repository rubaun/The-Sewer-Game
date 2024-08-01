using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed;
    public int hit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += new Vector3(1 * speed * Time.deltaTime, 0, 0);
        StartCoroutine("DestroyAfter");
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(!other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Chão"))
        {
            Destroy(this.gameObject);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(!other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Chão"))
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator DestroyAfter()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(this.gameObject);
    }
    
}
