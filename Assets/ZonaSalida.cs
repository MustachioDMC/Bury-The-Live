using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaSalida : MonoBehaviour
{
   public Transform origen;
    public Transform destino;
    bool haCruzado;
    float temporizador;
    BoxCollider2D colision;
    // Start is called before the first frame update
    void Start()
    {
        colision = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!haCruzado)
        {
            haCruzado = true;
            Camera.main.transform.position = destino.position;
        }
        
    }
} 


