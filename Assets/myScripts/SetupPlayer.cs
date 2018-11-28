using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking; // Unet API/ biblioteca de Rede (TCP/UDP)


[RequireComponent(typeof(NetworkIdentity))] //Faz obrigatório o NetworkIdentity para o Player

public class SetupPlayer : NetworkBehaviour
{
	[SerializeField] Camera cam;
	[SerializeField] AudioListener audio;
	protected virtual void Start()
	{
		if(isLocalPlayer == false)
		{
			return;
		}

		//GameObject.Find("MainCamera").SetActive(false);
		cam.enabled = true;
		audio.enabled = true;
		GetComponent<CharacterController> ().enabled = true;
		GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController> ().enabled = true;
	}
}