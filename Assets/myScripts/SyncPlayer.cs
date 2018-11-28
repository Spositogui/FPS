using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking; // Unet API/ biblioteca de Rede (TCP/UDP)


public class SyncPlayer : NetworkBehaviour
{
	[SyncVar] Vector3 syncPos;
	[SyncVar] Quaternion syncPlayerRot;
	[SyncVar] Quaternion syncCameraRot;
    [SyncVar(hook = "LifeChange")]
    private float maxLife = 100;

    [SerializeField]
	Transform myPos;
	[SerializeField]
	Transform myCameraPos;
	private float speed = 15f;
    private Vector3 lastPosition;
    private Quaternion lastCamRot;
    private Quaternion lastRot;
    private float distance = 0.5f;

    private Text lifeTxt;

    private void Start()
    {
        if(isLocalPlayer)
        { 
            lifeTxt = GameObject.Find("lifeTxt").GetComponent<Text>();
            if (lifeTxt == null)
                return;

            ShowTextLife();
        }
    }

    protected virtual void FixedUpdate()
	{

		if(!isLocalPlayer)
		{
            //rotation
            myPos.rotation = Quaternion.Lerp(myPos.rotation, syncPlayerRot, Time.deltaTime * speed);
            myCameraPos.rotation = Quaternion.Lerp(myCameraPos.rotation, syncCameraRot, Time.deltaTime * speed);
            //movimento
            myPos.position = Vector3.Lerp (myPos.position, syncPos, Time.deltaTime*speed);
		}
		TransmitPosition ();

	}

	[Command] // sincroniza diretamente com o servidor, a função deve iniciar com Cmd
	void CmdPosToServer(Vector3 pos, Quaternion CamRot, Quaternion PlayerRot)
	{
        //rot
        syncPlayerRot = PlayerRot;
        syncCameraRot = CamRot;
        //mov
		syncPos = pos;
	}

	[ClientCallback]
	void TransmitPosition()
	{
		if(isLocalPlayer)
        {

            if(Vector3.Distance(myPos.position, lastPosition) > distance ||
               Quaternion.Angle(myCameraPos.rotation, lastCamRot) > distance ||
               Quaternion.Angle(myPos.rotation, lastRot) > distance)
            { 
                CmdPosToServer(myPos.position, myCameraPos.rotation, myPos.rotation);
                lastPosition = myPos.position;
                lastCamRot = myCameraPos.rotation;
                lastRot = myPos.rotation;
            }
        }
    }
    public void ShowTextLife()
    {
        if(isLocalPlayer)
        {
            lifeTxt.text = maxLife.ToString();
        }
    }

    public void LifeChange(float life)
    {
        maxLife = life;
        ShowTextLife();
    }

    public void OnDamage(float damage)
    {
        maxLife -= damage;
    }


}
