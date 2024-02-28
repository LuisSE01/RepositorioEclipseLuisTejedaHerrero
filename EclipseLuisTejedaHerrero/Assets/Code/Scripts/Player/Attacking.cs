using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking : MonoBehaviour
{
    // Para cambiar la telca con la que atacaremos
    public KeyCode attackKey = KeyCode.Mouse0;
    // Referencia al Animator
    public Animator anim;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Si pulsamos la tecla de atacar asignada...
        if (Input.GetKeyDown(attackKey) == true)
        {
            anim.SetTrigger("Attack");
        }
        
    }
}
