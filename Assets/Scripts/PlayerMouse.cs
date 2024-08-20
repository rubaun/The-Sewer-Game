using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouse : MonoBehaviour
{
    public Camera cam;
    private Vector3 direcao;
    private float angulo;
    public GameObject municao;
    public GameObject mira;
    public float velocidade;
    private float movimentoHorizontal;
    private float movimentoVertical;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Giro
        direcao = Input.mousePosition - cam.WorldToScreenPoint(transform.position);

        angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;
                
        transform.rotation = Quaternion.AngleAxis(angulo, Vector3.forward);

        //Movimentação
        /*movimentoHorizontal = Input.GetAxis("Horizontal");

        movimentoVertical = Input.GetAxis("Vertical");

        transform.position += new Vector3(
                                    movimentoHorizontal * velocidade * Time.deltaTime, 
                                    movimentoVertical * velocidade * Time.deltaTime, 0);
        */
        //Tiro
        if(Input.GetMouseButtonDown(0))
        {
            Instantiate(
                                municao, 
                                mira.transform.position, 
                                mira.transform.rotation
                                    );
        }
        
    }
}
