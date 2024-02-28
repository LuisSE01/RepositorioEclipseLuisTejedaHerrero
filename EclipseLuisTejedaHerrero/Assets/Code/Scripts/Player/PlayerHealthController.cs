using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    //Variables para controlar la vida actual del jugador y el máximo de vida que puede tener
    //[HideInInspector] -> es un atributo de la variable que me permite que una variable no sea visible en el editor de Unity pero se mantenga pública
    [HideInInspector] public int currentHealth;
    public int maxHealth;

    //Variables para controlar el tiempo de invencibilidad
    public float invincibleLength; //Me sirve para rellenar el contador
    private float _invincibleCounter; //Contador de tiempo

    //Referencia al UIController
    private UIController _uIReference;
    //Referencia al PlayerController
    private PlayerController _pCReference;
    //Referencia al SpriteRenderer del jugador
    private SpriteRenderer _sR;
    //Referencia al LevelManager
    private LevelManager _lReference;

    // Start is called before the first frame update
    void Start()
    {
        //Inicializamos la referencia de UIController
        _uIReference = GameObject.Find("Canvas").GetComponent<UIController>();
        //Inicializamos la referencia al PlayerController
        _pCReference = GetComponent<PlayerController>();
        //Inicializamos la referencia al SpriteRenderer
        _sR = GetComponent<SpriteRenderer>();
        //Inicializamos la referencia al LevelManager
        _lReference = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        //Inicializamos la vida del jugador
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //Comprobamos si el contador de invencibilidad aún no está vacío
        if(_invincibleCounter > 0)
        {
            //Le restamos al contador 1 cada segundo
            _invincibleCounter -= Time.deltaTime;

            //Cuando el contador haya decrecido hasta 0
            if(_invincibleCounter <= 0)
                //Cambiamos el color del sprite, mantenemos el RGB y ponemos la opacidad a tope
                _sR.color = new Color(_sR.color.r, _sR.color.g, _sR.color.b, 1f);
        }
    }

    //Método para manejar el daño
    public void DealWithDamage()
    {
        //Si el contador de tiempo de invencibilidad se ha agotado, es decir, si ya no somos invencibles
        if(_invincibleCounter <= 0)
        {
            //Restamos 1 de la vida que tengamos
            currentHealth--; //_currentHealth -= 1 <=> _currentHealth = _currentHealth - 1 <=> _currentHealth--

            //Si la vida está en 0 o por debajo (para asegurarnos de tener en cuenta solo valores positivos)
            if (currentHealth <= 0)
            {
                //Hacemos que la vida se ponga a cero si se queda en negativo
                currentHealth = 0;

                ////Hacemos desaparecer de momento al jugador
                //gameObject.SetActive(false);
                //Llamamos al método del LevelManager que respawnea al jugador
                _lReference.RespawnPlayer();
            }
            //Si el jugador ha recibido daño pero no ha muerto
            else
            {
                //Inicializamos el contador de invencibilidad
                _invincibleCounter = invincibleLength;
                //Cambiamos el color del sprite, mantenemos el RGB y ponemos la opacidad a la mitad
                _sR.color = new Color(_sR.color.r, _sR.color.g, _sR.color.b, .5f);
                //Llamamos al método que hace que el jugador realice el KnockBack
/*                _pCReference.Knockback();
*/            }

            //Actualizamos la UI (los corazones)
            _uIReference.UpdateHealthDisplay();
        }
       
    }
}
