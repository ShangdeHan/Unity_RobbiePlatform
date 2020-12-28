using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public GameObject death;
    int trapsLayer;
    void Start()
    {
        trapsLayer = LayerMask.NameToLayer("Traps");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == trapsLayer)
        {
            Instantiate(death, transform.position, transform.rotation);
            gameObject.SetActive(false);
            AudioManager.playDeathAudio();
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            GameManager.PlayerDied();
        }
    }
}
