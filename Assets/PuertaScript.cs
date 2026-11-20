using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PuertaScript : MonoBehaviour
{
    public GameObject[] listaEnemigos;
    TilemapRenderer dibujo;
    TilemapCollider2D colision;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int contador = 0;
        foreach (GameObject go in listaEnemigos)
        {
            MovimientoEnemigoM scr = go.GetComponent<MovimientoEnemigoM>();
            if (scr.EstaMuerto==true)
            {
                contador++;
            }
            // contador += go.GetComponent<ScriptBoton>().EstaPulsado() ? 1 : 0;
        }

        if (contador == listaEnemigos.Length)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }
    
}
