using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    int player;
    public GameObject explosion;

    void Start()
    {
        player = LayerMask.NameToLayer("Player");
        GameManager.RegisterOrb(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == player)
        {
            Instantiate(explosion, transform.position, transform.rotation);
            gameObject.SetActive(false);
            AudioManager.playOrbAudio();
            GameManager.PlayerGrabbedOrb(this);
        }
    }

}
