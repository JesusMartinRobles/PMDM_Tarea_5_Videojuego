using UnityEngine;

public class Sonidos : MonoBehaviour
{
    public static Sonidos instancia;
    public AudioClip sonidoSalto;
    public AudioClip sonidoGolpe;
    public AudioSource audioSource;


    private void Awake() 
    {
        if(instancia == null) // Si la instancia es nula
        {
            instancia = this; // Establecer la instancia como esta clase
        }
        else
        {
            Destroy(this.gameObject); // Destruir este objeto
        }
    }

    public void PlaySalto() // Reproducir el sonido de salto
    {
        audioSource.clip = sonidoSalto; // Establecer el clip de audio
        audioSource.Play(); // Reproducir el clip de audio
    }

    public void PlayGolpe() // Reproducir el sonido de golpe
    {
        audioSource.clip = sonidoGolpe; // Establecer el clip de audio
        audioSource.Play(); // Reproducir el clip de audio
    }

    private void OnDestroy() // Cuando se destruye el objeto
    {
        instancia = null; // Establecer la instancia como nula
    }
}
