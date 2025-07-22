using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ControladorEscena : MonoBehaviour
{

    public GameObject Jugador;
    public Camera CamaraJuego;
    public GameObject[] BloquePrefabs;
    public float PunteroDeJuego;
    public float PuntoGeneracion = 12;
    public Text TextoDeJuego;
    public bool Perdiste;

    public bool juegoComenzado = false;
    public bool juegoIniciado = false;
    
    // Start is called before the first frame update
    void Start()
    {
        // Establecer la posición inicial del jugador en el eje X a 0
        Jugador.transform.position = new Vector2(-15, -4); // Posición inicial del jugador
        PunteroDeJuego = -7; // Puntero de juego
        Perdiste = false; // Inicializar la variable Perdiste
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Si se presiona el botón izquierdo del mouse
        {
            juegoComenzado = true; // El juego ha comenzado
            juegoIniciado = true; // El juego ha iniciado
        }
        if(Jugador != null && juegoComenzado) { // Si el jugador no es nulo y el juego ha comenzado
            CamaraJuego.transform.position = new Vector3(Jugador.transform.position.x, CamaraJuego.transform.position.y, CamaraJuego.transform.position.z); // Mover la cámara
            TextoDeJuego.text = "Puntos: " + Mathf.Floor(Jugador.transform.position.x); // Mostrar los puntos
        }
        else 
        {
            if(!Perdiste && juegoIniciado) // Si no has perdido
            {
                TextoDeJuego.text = "Game Over \nPulsa la pantalla dos veces para volver a jugar";
                Perdiste = true;
            }
            if (Perdiste) // Si has perdido
            {
                TextoDeJuego.text = "Game Over \nPulsa la pantalla dos veces para volver a jugar";
                if (Input.GetMouseButtonDown(0)) // Si se presiona el botón izquierdo del mouse
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
            
        }
        // Generar bloques
        while (Jugador != null && PunteroDeJuego<Jugador.transform.position.x + PuntoGeneracion) // Mientras el jugador no sea nulo y el puntero de juego sea menor que la posición del jugador en el eje X más el punto de generación
        {
            int IndiceBloque = UnityEngine.Random.Range(0, BloquePrefabs.Length -1); // Índice del bloque
            if (PunteroDeJuego < 0) // Si el puntero de juego es menor que 0
            {
                IndiceBloque = 4; // Índice del bloque
            }
            GameObject ObjetoBloque = Instantiate(BloquePrefabs[IndiceBloque]); // Instanciar el bloque
            ObjetoBloque.transform.SetParent(this.transform); // Establecer el bloque como hijo del controlador de la escena
            Bloque bloque = ObjetoBloque.GetComponent<Bloque>(); // Obtener el componente Bloque del bloque
            ObjetoBloque.transform.position = new Vector2(PunteroDeJuego + bloque.Tamaño/2, 0); // Posición del bloque
            PunteroDeJuego += bloque.Tamaño; // Aumentar el puntero de juego
        }
    }
}
