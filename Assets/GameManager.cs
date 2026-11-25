using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI textoPuntuacion;
    public Image[] corazones;
    public Sprite corazonLleno;
    public Sprite corazonVacio;
    public GameObject panelVictoria;
    bool victoria = false;
    int puntuacion;


    void Start()
    {
        puntuacion = 0;
        ActualizarPuntuacion();
        panelVictoria.SetActive(false);
        Time.timeScale = 1f;
    }
    void Update()
    {
        if (victoria && Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
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

    public void MostrarVictoria()
    {
        victoria = true;
        panelVictoria.SetActive(true);

        Time.timeScale = 0;
    }
    public void ActualizarVida(int vidaActual)
{
    for (int i = 0; i < corazones.Length; i++)
    {
        if (i < vidaActual)
        {
            corazones[i].sprite = corazonLleno;
        }
        else
        {
            corazones[i].sprite = corazonVacio;
        }
    }
}
}