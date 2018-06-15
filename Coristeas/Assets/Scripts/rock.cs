using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class rock : NetworkBehaviour {


    //Piedra
    public Rigidbody2D rockRB;
    //Ancla
    public Rigidbody2D anchor;
    //Tiempo luego del disparo para que desaparezca el resorte.
    public float realeseTime = 0.15f;
    //Si presiona la bola.
    private bool isPressed = false;
    //Camera
    Camera MainCamera;
    //Posicion inicial de la camara
    private float cameraInitialPosition;
    //Posicion inicial de la roca
    private Vector2 rockInitialPosition;
    //Distancia que se puede estirar el resorte
    private float maxDragDistance = 3f;

    private float offsetX;
    //Posicion limite de la camara.
    private float positionFinal;
    //Momento en el cual retornar al punto de origen
    public bool retornar;
    //Velocidad a la que regresa la camara
    public float velocidad;

    [SerializeField] GameObject castle;


    string _ID;


    private void Start()
    {
        _ID = "Player " + GetComponent<NetworkIdentity>().netId;
        transform.name = _ID;


        //Si el jugador no es local destruir este codigo.
        if (!isLocalPlayer)
        {
            CmdCastleApears();
            Destroy(this);
            return;
        }


        retornar = true;
        MainCamera = Camera.main;

        cameraInitialPosition = anchor.transform.position.x; //MainCamera.transform.position.x;
        rockInitialPosition = anchor.transform.position;

        if (rockInitialPosition.x < 0f)
        {
            positionFinal = 29;
        }
        else
        {
            positionFinal = -12;
        }

    }




    private void Update()
    {
        if (isPressed)
        {

            //Para que la bola pueda ser arrastrada.
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Si sobre pasa el limite de arrastre.
            if (Vector3.Distance(mousePos, anchor.position) > maxDragDistance)
            {
                rockRB.position = anchor.position + (mousePos - anchor.position).normalized * maxDragDistance;
            }
            else
            {
                rockRB.position = mousePos;
            }

        }

        if (rockInitialPosition.x < 0f)
        {
            offsetX = MainCamera.transform.position.x - rockRB.transform.position.x;
            //Segir a la bola
            if (offsetX <= 0 && MainCamera.transform.position.x < positionFinal && !retornar)
            {
                this.MainCamera.transform.position = new Vector3(rockRB.transform.position.x, this.MainCamera.transform.position.y, this.MainCamera.transform.position.z);
            }

            //Si debe retornar al origen
            else if (retornar)
            {
                if (this.MainCamera.transform.position.x > cameraInitialPosition)
                {
                    this.MainCamera.transform.position = new Vector3(this.MainCamera.transform.position.x - velocidad, this.MainCamera.transform.position.y, this.MainCamera.transform.position.z);

                }
                else
                {
                    retornar = false;
                    rockRB.transform.position = rockInitialPosition;
                    anchor.transform.position = rockInitialPosition;
                    rockRB.velocity = new Vector2(0f, 0f);
                    reestableserSpring();
                }
            }
        }

        else
        {
            offsetX = rockRB.transform.position.x - MainCamera.transform.position.x;
            //Segir a la bola
            if (offsetX <= 0 && MainCamera.transform.position.x > positionFinal && !retornar)
            {
                this.MainCamera.transform.position = new Vector3(rockRB.transform.position.x, this.MainCamera.transform.position.y, this.MainCamera.transform.position.z);
            }

            //Si debe retornar al origen
            else if (retornar)
            {
                if (this.MainCamera.transform.position.x < cameraInitialPosition)
                {
                    this.MainCamera.transform.position = new Vector3(this.MainCamera.transform.position.x + velocidad, this.MainCamera.transform.position.y, this.MainCamera.transform.position.z);

                }
                else
                {
                    retornar = false;
                    rockRB.transform.position = rockInitialPosition;
                    anchor.transform.position = rockInitialPosition;
                    rockRB.velocity = new Vector2(0f, 0f);
                    reestableserSpring();
                }
            }
        }

    }

    //Si presiona la bola.
    private void OnMouseDown()
    {
        isPressed = true;
        //kinematic para que no sea arrastrada por el resorte mientras se esta presionando.
        rockRB.isKinematic = true;
        //

    }

    //Si se suelta la bola.
    private void OnMouseUp()
    {
        hola();
        isPressed = false;
        rockRB.isKinematic = false;
        //StartCoroutine(Release());
        StartCoroutine(ComeBack());
    }



    [Client]
    void hola()
    {
        CmdImprimir(_ID);
    }

    //Rutina para soltar el resorte luego de un tiempo.
    IEnumerator Release()
    {
        //Se espera un tiempo.
        yield return new WaitForSeconds(realeseTime);
        //Luego suelta el Spring para que la bola vuele libre.
    //GetComponent<SpringJoint2D>().enabled = false;
        CmdRelease(_ID);
        //Para que no se pueda manipular mas la bola.
        this.enabled = false;
    }
    

    
    //Rutina para soltar el resorte luego de un tiempo.
    IEnumerator ComeBack()
    {
        //Se espera un tiempo.
        yield return new WaitForSeconds(4);
        retornar = true;
    }



    public void reestableserSpring(){
        GetComponent<SpringJoint2D>().enabled = true;
    }


    [Command]
    void CmdCastleApears()
    {
        GameObject instance = Instantiate(castle) as GameObject;
        NetworkServer.Spawn(instance);
    }

   



    [Command]
    void CmdRelease(string _ID)
    {
        Debug.Log(_ID);
        GameObject.Find(_ID).GetComponent<SpringJoint2D>().enabled = false;
    }

    [Command]
    void CmdImprimir(string _ID)
    {
        Debug.Log(_ID);
    }

}
