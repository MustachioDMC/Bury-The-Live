using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaSalida : MonoBehaviour
{
   PuertaScript puerta;

    void Start()
    {
        puerta = GetComponentInParent<PuertaScript>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            puerta.CerrarPuerta();
        }
    }
}

