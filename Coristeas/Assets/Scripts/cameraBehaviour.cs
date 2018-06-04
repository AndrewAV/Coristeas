using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class cameraBehaviour : MonoBehaviour
{
    private float offsetX;
    public float positionFinal;
    public GameObject roca;
    private float posicionInicial;
    public bool retornar;
    public float velocidad;
    private Vector3 posicionInicialRoca;


    // Use this for initialization 
    void Start()
    {
        posicionInicial = this.gameObject.transform.position.x;
        posicionInicialRoca = roca.transform.position;
    }

    // Update is called once per frame 
    void Update()
    {
        offsetX = this.gameObject.transform.position.x - roca.transform.position.x;
        if (offsetX <= 0 && this.gameObject.transform.position.x < positionFinal && !retornar)
        {
            this.gameObject.transform.position = new Vector3(roca.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
        }
        else if (retornar)
        {
            if (this.gameObject.transform.position.x > posicionInicial)
            {
                this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x - velocidad, this.gameObject.transform.position.y, this.gameObject.transform.position.z);

            }
            else
            {
                retornar = false;
                roca.transform.position = posicionInicialRoca;
                roca.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
                roca.GetComponent<rock>().reestableserSpring();
            }
        }
    }

    public void pene()
    {

    }
}
