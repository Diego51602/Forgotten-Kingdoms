using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicaController : MonoBehaviour
{
    public AudioClip audio1;
    public AudioClip audio2;

    private AudioSource audioSource;
    private BarraDeVida barraDeVida;

    private bool reproduciendoAudio1 = true;
    bool barraDeVidaVisible = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        barraDeVida = FindObjectOfType<BarraDeVida>(); // Busca la instancia de BarraDeVida en la escena
        
        // Configurar audioSource para reproducir audio1 al inicio
        audioSource.clip = audio1;
        audioSource.Play();
    }

    void Update()
    {
        // Verificar si la barra de vida está activa (visible)
        barraDeVidaVisible = barraDeVida.gameObject.activeSelf;

        if (reproduciendoAudio1 && barraDeVidaVisible)
        {
            // Si estamos reproduciendo audio1 y la barra de vida se hace visible, cambiamos a audio2
                if (audioSource.clip != audio2)
                {
                    audioSource.clip = audio2;
                    audioSource.Play();
                    reproduciendoAudio1 = false; // Ya no estamos reproduciendo audio1
                }
        }
        else
        {
            // Si no estamos reproduciendo audio1, comprobamos si la barra de vida se oculta para volver a audio1
            if (!barraDeVidaVisible)
            {
                if (audioSource.clip != audio1)
                {
                    audioSource.clip = audio1;
                    audioSource.Play();
                    reproduciendoAudio1 = true; // Estamos reproduciendo audio1 nuevamente
                }
            }
        }
    }
}
