using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class castleMaterials : MonoBehaviour
{
    //Aguante del material.
    public int vida;
    //Velocidad minima a la cual se hace daño.
    private float velocidadMinima = 7f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.relativeVelocity.magnitude);
        //Si es golpeada a mayor velocidad que la minima.
        if (collision.relativeVelocity.magnitude > velocidadMinima)
        {
            quitarVida();
        }
    }

    //Resta vida al material.
    private void quitarVida()
    {
        vida--;
        //Se destruye si se queda sin vida.
        if (vida == 0)
        {
            Destroy(gameObject);
        }
    }




}

