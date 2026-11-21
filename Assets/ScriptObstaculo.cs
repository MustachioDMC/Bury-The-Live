using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ScriptObstaculo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        MovimientoJugadorA jugador = collision.GetComponent<MovimientoJugadorA>();

        if (jugador != null)
        {
            jugador.setVidaRestante(0);
            Debug.Log("Te mató el obstáculo");
        }
    }
}
