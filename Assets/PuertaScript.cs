using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaScript : MonoBehaviour
{
    public MovimientoEnemigoM[] enemigos;

    private Collider2D colisionPuerta;
    private SpriteRenderer spritePuerta;

    bool abierta = false;
    bool todosMuertos = true;

    void Start()
    {
        colisionPuerta = GetComponent<Collider2D>();
        spritePuerta = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!abierta)
        {
            todosMuertos = true;

            foreach (MovimientoEnemigoM enemigo in enemigos)
            {
                if (enemigo != null && !enemigo.EstaMuerto)
                {
                    todosMuertos = false;
                    break;
                }
            }

            if (todosMuertos)
            {
                AbrirPuerta();
            }
        }
    }

    void AbrirPuerta()
    {
        abierta = true;
        colisionPuerta.enabled = false;
        spritePuerta.enabled = false;
    }

    public void CerrarPuerta()
    {
        colisionPuerta.enabled = true;
        spritePuerta.enabled = true;
        abierta = false;
    }
}
