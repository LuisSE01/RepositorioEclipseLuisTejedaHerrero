using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Para obtener la posición objetivo de la cámara
    public Transform target;
    //Variables para posición mínima y máxima en vertical de la cámara
    public float minHeight, maxHeight;

    //Referencias a las posiciones de los fondos
    public Transform farBackground, middleBackground;
    ////Variable donde guardar la última posición en X que tuvo el jugador
    //private float _lastXPos;
    //Referencia a la última posición del jugador en X e Y
    private Vector2 _lastPos;

    // Start is called before the first frame update
    void Start()
    {
        //Al empezar el juego la última posición del jugador será la actual
        //_lastXPos = transform.position.x;
        _lastPos = transform.position;
    }

    // LateUpdate se llama también una vez por frame, pero después de todos los Update del juego
    //Con lo cuál evitamos problemas de tirones de la cámara
    void LateUpdate()
    {
        ////La cámara sigue al jugador sin variar su posición en Z
        //transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        ////Creamos una restricción entre un mínimo y un máximo para el movimiento de la cámara en Y
        //float _clampedY = Mathf.Clamp(transform.position.y, minHeight, maxHeight);
        ////Actualizamos el movimiento de la cámara usando las restricciones
        //transform.position = new Vector3(transform.position.x, _clampedY, transform.position.z);

        //Esta línea equivale a todas las de arriba
        transform.position = new Vector3(target.position.x, Mathf.Clamp(target.position.y, minHeight, maxHeight), transform.position.z);

        //Variable que me permite conocer cuanto hay que moverse en X
        //float _amountToMoveX = transform.position.x - _lastXPos;
        //Referencia que me permite conocer cuanto hay que moverse en X e Y
        Vector2 _amountToMove = new Vector2(transform.position.x - _lastPos.x, transform.position.y - _lastPos.y);

        //Como el fondo del cielo se mueve a la misma velocidad que el jugador, le decimos que se mueva lo mismo que este
        //farBackground.position = farBackground.position + new Vector3(_amountToMoveX, 0f, 0f);
        farBackground.position = farBackground.position + new Vector3(_amountToMove.x, _amountToMove.y, 0f);
        //El fondo de las nubes se va a mover sin embargo a la mita de velocidad que lleve el jugador, luego se moverá la mitad
        //middleBackground.position += new Vector3(_amountToMoveX * .5f, 0f, 0f);
        middleBackground.position += new Vector3(_amountToMove.x, _amountToMove.y, 0f) * .5f;

        //Actualizamos la posición del jugador
        //_lastXPos = transform.position.x;
        _lastPos = transform.position;
    }
}
