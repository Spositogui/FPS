using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Shooting : NetworkBehaviour {

    private float damage;
    private float rangeBullet;
    private RaycastHit hit;

    [SerializeField]
    private Transform canTransform;

    public GameObject bullet;

    void Start ()
    {
        damage = 25;
        rangeBullet = 25;	
	}
	
	// Update is called once per frame
	void Update ()
    {
        CheckPlayerShooting();
	}

    private void CheckPlayerShooting()
    {
        if (!isLocalPlayer)//controle para saber se foi o jogador que atirou
            return;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {   //Um ponto no meio da dela que despara
        if(Physics.Raycast(canTransform.TransformPoint(0, 0, 0.5f),canTransform.forward, out hit, rangeBullet))
        {
            Debug.Log(hit.transform.tag);
            GameObject bala = Instantiate(bullet, canTransform.position, canTransform.rotation);

            if(hit.transform.tag == "Player")
            {
                string name = hit.transform.name;
                CmdTellServer(name, damage);
            }
        }
    }

    [Command]//dizr ao servidor onde o hit acertou
    private void CmdTellServer(string name , float damage)
    {
        GameObject go = GameObject.Find(name);
        go.GetComponent<SyncPlayer>().OnDamage(damage);
    }
}
