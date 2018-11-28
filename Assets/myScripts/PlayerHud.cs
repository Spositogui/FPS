using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHud : MonoBehaviour {

    [SerializeField]
    private Text scoreTxt;
    [SerializeField]
    private Text lifeTxt;

    private int scorePoints;
    private int life;

    private void Start()
    {
        scorePoints = 0;
        life = 2;

        scoreTxt.text = "SCORE: " + scorePoints;
        lifeTxt.text = "LIFE: " + life;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            lifeTxt.text = "LIFE: " + life--;
        }
    }


}
