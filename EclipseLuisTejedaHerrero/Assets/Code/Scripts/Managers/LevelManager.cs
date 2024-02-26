using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //Variable de tiempo para la corrutina
    public float waitToRespawn;

    //Referencia al PlayerController
    private PlayerController _pCReference;
    //Referencia al CheckpointController
    private CheckpointController _cReference;
    //Referencia al UIController
    private UIController _uIReference;
    //Referencia al PlayerHealthController
    private PlayerHealthController _pHReference;

    private void Start()
    {
        //Inicializamos la referencia al PlayerController
        _pCReference = GameObject.Find("Player").GetComponent<PlayerController>();
        //Inicializamos la referencia al CheckpointController
        _cReference = GameObject.Find("CheckpointController").GetComponent<CheckpointController>();
        //Inicializamos la referencia al UIController
        _uIReference = GameObject.Find("Canvas").GetComponent<UIController>();
        //Inicializamos la referencia al PlayerHealthController
        _pHReference = GameObject.Find("Player").GetComponent<PlayerHealthController>();
    }

    //Método para respawnear al jugador cuando muere
    public void RespawnPlayer()
    {
        StartCoroutine(RespawnPlayerCo());
    }

    //Corrutina para respawnear al jugador
    private IEnumerator RespawnPlayerCo()
    {
        //Desactivamos al jugador
        _pCReference.gameObject.SetActive(false);
        //Esperamos un tiempo determinado
        yield return new WaitForSeconds(waitToRespawn);
        //Reactivamos al jugador
        _pCReference.gameObject.SetActive(true);
        //Lo ponemos en la posición de Respawn
        _pCReference.transform.position = _cReference.spawnPoint;
        //Ponemos la vida del jugador al máximo
        _pHReference.currentHealth = _pHReference.maxHealth;
        //Actualizamos la UI
        _uIReference.UpdateHealthDisplay();
    }
}
