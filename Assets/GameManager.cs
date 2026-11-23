using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI textoPuntuacion;
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
}