using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class playerController : NetworkBehaviour {
    public GameObject rock;
	// Use this for initialization
	void Start () {
        
        if (!isLocalPlayer)
        {
            Destroy(rock.GetComponent<rock>());
            return;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
