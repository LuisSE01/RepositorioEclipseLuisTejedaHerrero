using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    //Método que detecta cuando el jugador se mete dentro de un trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Comprobamos si es el jugador el que ha entrado en la zona de trigger
        if (collision.CompareTag("Player"))
        {
            //Debug.Log("Hit");
            //Sacamos del jugador el método que le hace daño
            collision.GetComponent<PlayerHealthController>().DealWithDamage();
        }
    }
}
