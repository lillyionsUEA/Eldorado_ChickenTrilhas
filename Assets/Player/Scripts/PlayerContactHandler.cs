using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerContactHandler : MonoBehaviour
{

    public Image itemImage;
    public PlayerAudioController audioController;
    public string goToLevel = "Level2";

    bool canWinLevel = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            Debug.Log("VOCÊ PEGOU O MEU OVO");
            Destroy(collision.gameObject);
            itemImage.color = new Color(0.98f, 0.945f, 0.765f);
            canWinLevel = true;
            audioController.PlayGetItem();
        }

        if (collision.gameObject.CompareTag("FinalPoint"))
        {
            if (canWinLevel)
            {
                SceneManager.LoadScene(goToLevel);
            }
            else
            {
                Debug.Log("NÃO TÁ APTO");
            }
        }
    }
}
