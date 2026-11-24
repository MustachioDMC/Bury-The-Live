using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPlatano : MonoBehaviour
{
    public float altura = 0.3f;
    public float velocidad = 2f;

    Vector3 posicionInicial;

    void Start()
    {
        posicionInicial = transform.position;
    }

    void Update()
    {
        float movimiento = Mathf.Sin(Time.time * velocidad) * altura;

        transform.position = posicionInicial + Vector3.up * movimiento;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Algo ha tocado el platano: " + collision.name);

        if (collision.CompareTag("Player"))
        {
            Debug.Log("El jugador ha tocado el platano");

            MovimientoJugadorA jugador = collision.GetComponent<MovimientoJugadorA>();

            if (jugador != null)
            {
                jugador.Curar(3);
                Destroy(gameObject);
            }
        }
    }
}
