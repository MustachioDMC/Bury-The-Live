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

   
}

