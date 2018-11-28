using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting2 : MonoBehaviour {

    public GameObject theBullet;
    public Transform barrelEnd;

    public int bulletSpeed;

    private bool canShoot;
    private bool powerUp;
    private float despawnTime;
    private float timeNextShoot;
    private float timeLeft;
    private int life;

    void Start()
    {
        despawnTime = 2f;
        timeNextShoot = 1f;
        canShoot = true;
        powerUp = false;
        timeLeft = 0;
        life = 2;
    }

    void Update ()
    {
        if (!powerUp)
            timeNextShoot = 1f;
        else
            timeNextShoot = 0.1f;

        if(timeLeft > 0)
        {
            Debug.Log(timeLeft -= Time.deltaTime);//retirar o debug log
            powerUp = true;
        }
        else
            powerUp = false;


        if (Input.GetKey(KeyCode.Mouse0))
        {

            if(canShoot)
            {
                canShoot = false;
                Shoot();
                StartCoroutine(ShootingTime());
            }
        }	
	}

    IEnumerator ShootingTime()
    {
        yield return new WaitForSeconds(timeNextShoot);
        canShoot = true;
    }

    private void Shoot()
    {
        var bullet = Instantiate(theBullet, barrelEnd.position, barrelEnd.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;

        Destroy(bullet, despawnTime);
    }

    void OnTriggerEnter(Collider others){

        if(others.gameObject.CompareTag("PowerUp"))
        {
            timeLeft = 7;
            Destroy(others.gameObject);
        }
    }
}
