using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rock : MonoBehaviour {

    public Rigidbody2D rb;

    public float realeseTime= 0.15f;
    private bool isPressed = false;


    private void Update()
    {
        if (isPressed)
        {
            rb.position = Camera.main.ScreenToWorldPoint( Input.mousePosition);
        }
    }
    private void OnMouseDown()
    {
        isPressed = true;
        rb.isKinematic=true;
    }
    private void OnMouseUp()
    {
        isPressed = false;
        rb.isKinematic = false;

        StartCoroutine( Release());
    }
    IEnumerator Release()
    {
        yield return new WaitForSeconds(realeseTime);

        GetComponent<SpringJoint2D>().enabled = false;
        //this.enabled = false;
    }
}
