using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerContactHandler : MonoBehaviour
{

    public Image itemImage;
    public PlayerAudioController audioController;

    bool canWinLevel = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("GAME OVER");
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
                Debug.Log("VOCÊ GANHOU");
            }
            else
            {
                Debug.Log("NÃO TÁ APTO");
            }
        }
    }
}
