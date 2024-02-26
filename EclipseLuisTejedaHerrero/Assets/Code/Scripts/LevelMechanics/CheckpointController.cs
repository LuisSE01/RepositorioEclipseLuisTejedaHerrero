using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    //Declaramos un array donde guardar los checkpoints de la escena
    private Checkpoint[] _checkpoints;

    //Referencia a la posición de reaparición del jugador
    public Vector3 spawnPoint;

    //Referencia al jugador
    private GameObject _player;

    // Start is called before the first frame update
    void Start()
    {
        //Buscamos todos los GameObjects que tengan el script asociado Checkpoint y los guardamos en nuestro array
        _checkpoints = GetComponentsInChildren<Checkpoint>(); //Buscamos en el objeto CheckpointController los scripts asociados a sus hijos
        //Guardamos la posición inicial del jugador por si no hemos tocado ningún Checkpoint
        spawnPoint = GameObject.Find("Player").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Método para desactivar los checkpoints
    public void DeactivateCheckpoints()
    {
        //Hacemos un bucle que pase por todos los checkpoints almacenados en el array
        foreach(Checkpoint c in _checkpoints)
        {
            //Hace el método de resetear ese Checkpoint concreto
            c.ResetCheckpoint();
        }
    }

    //Método para generar el punto de reaparición del jugador
    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        //El punto de spawn del jugador será el del checkpoint activo que le pasemos
        spawnPoint = newSpawnPoint;
    }
}
