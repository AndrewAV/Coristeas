using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rock : MonoBehaviour {

    public Rigidbody2D rb;

    public Rigidbody2D anchor;
    //Tiempo luego del disparo para que desaparezca el resorte.
    public float realeseTime= 0.15f;
    //Si presiona la bola.
    private bool isPressed = false;

    private float maxDragDistance = 2f;

    private void Update()
    {
        if (isPressed)
        {
            //Para que la bola pueda ser arrastrada.
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Si sobre pasa el limite de arrastre.
            if (Vector3.Distance(mousePos, anchor.position) > maxDragDistance)
            {
                rb.position = anchor.position + ( mousePos - anchor.position).normalized * maxDragDistance;
            }
            else
            {  
                rb.position = mousePos;
            }
           
        }
    }
    //Si presiona la bola.
    private void OnMouseDown()
    {
        isPressed = true;
        //kinematic para que no sea arrastrada por el resorte mientras se esta presionando.
        rb.isKinematic=true;
    }

    //Si se suelta la bola.
    private void OnMouseUp()
    {
        isPressed = false;
        rb.isKinematic = false;
        StartCoroutine( Release());
    }

    //Rutina para soltar el resorte luego de un tiempo.
    IEnumerator Release()
    {
        //Se espera un tiempo.
        yield return new WaitForSeconds(realeseTime);
        //Luego suelta el Spring para que la bola vuele libre.
        GetComponent<SpringJoint2D>().enabled = false;
        //Para que no se pueda manipular mas la bola.
        //this.enabled = false;
    }
}
