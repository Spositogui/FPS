using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class IdPlayer : NetworkBehaviour {
    //Server
    [SyncVar]
    public string myName;

    //private
    private Transform myTransform;
    private NetworkInstanceId playerID;

    private void Awake()
    {
        myTransform = transform;
    }

    public override void OnStartLocalPlayer()
    {
        GetMyName();
        SetNamePlayer();
    }

    [Client]
    public void GetMyName()
    {
        playerID = GetComponent<NetworkIdentity>().netId;
        CmdTellMyName(MakeNamePlayer());
    }

    void Update ()
    {
        if (myTransform.name == "" || myTransform.name == "Player(Clone)")
        {
            SetNamePlayer();
        }
	}

    private void SetNamePlayer()
    {
        if(!isLocalPlayer)
        {
            myTransform.name = myName;
        }
        else
        {
            myTransform.name = MakeNamePlayer();
            myName = MakeNamePlayer();
        }
    }

    private string MakeNamePlayer()
    {
        string tempName = "Player" + playerID.ToString();
        return tempName;
    }

    [Command]
    private void CmdTellMyName(string name)
    {
        myTransform.name = name;
    }

    //dar nome
    public string MyName
    {
        get{ return myName;}
        set{ myName = value; }
    }
}
