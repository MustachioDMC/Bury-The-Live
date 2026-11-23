using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI textoPuntuacion;
    int puntuacion;

    void Start()
    {
        puntuacion = 0;
        ActualizarPuntuacion();
    }

    public void SumarPuntos(int puntos)
    {
        puntuacion += puntos;
        ActualizarPuntuacion();
    }

    void ActualizarPuntuacion()
    {
        textoPuntuacion.text = "Puntuacion: " + puntuacion;
    }
}