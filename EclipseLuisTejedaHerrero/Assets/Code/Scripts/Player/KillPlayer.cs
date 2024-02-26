using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    //Referencia al PlayerHealthController
    private PlayerHealthController _pHReference;
    //Referencia a UIController
    private UIController _uIReference;
    //Referencia al LevelManager
    private LevelManager _lMReference;

    private void Start()
    {
        //Inicializamos la referencia al PlayerHealthController
        _pHReference = GameObject.Find("Player").GetComponent<PlayerHealthController>();
        //Inicializamos la referencia al UIController
        _uIReference = GameObject.Find("Canvas").GetComponent<UIController>();
        //Inicializamos la referencia al UIController
        _lMReference = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    //Método para saber cuando el jugador ha entrado en la zona de muerte
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Si el jugador es el que ha entrado en la zona
        if (collision.CompareTag("Player"))
        {
            //Ponemos las vidas del jugador a 0
            _pHReference.currentHealth = 0;
            //Actualizamos las vidas en la UI
            _uIReference.UpdateHealthDisplay();
            //Respawneamos al jugador
            _lMReference.RespawnPlayer();
        }
    }
}
