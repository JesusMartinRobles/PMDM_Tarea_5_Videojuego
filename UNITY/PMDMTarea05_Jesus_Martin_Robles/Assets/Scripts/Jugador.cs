using System;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    public int FuerzaSalto;
    public int VelocidadMovimiento;
    public bool EnSuelo;
    public ControladorEscena controladorEscena;
    public Animator animator;
    public String animationTag = "moving";
    
    

    // Start is called before the first frame update
    void Start()
    {
        controladorEscena = GameObject.FindObjectOfType<ControladorEscena>(); // Encontrar el controlador de la escena
        this.transform.position = new Vector3(0, this.transform.position.y, this.transform.position.z); // Posición inicial del jugador
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, this.GetComponent<Rigidbody2D>().velocity.y); // Velocidad inicial del jugador
        animator = this.transform.GetChild(0).GetComponent<Animator>(); // Encontrar el animator del jugador
        animator.SetBool(animationTag, false); // Inicializar la animación del jugador
    }

    // Update is called once per frame
    void Update()
    {
        Animator animator = GetComponent<Animator>();
        if(controladorEscena.juegoComenzado)
        {
            // Siempre mover al jugador hacia adelante cuando el juego ha comenzado
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(VelocidadMovimiento, this.GetComponent<Rigidbody2D>().velocity.y);

            if(Input.GetMouseButtonDown(0) && EnSuelo) // Si se presiona el botón izquierdo del mouse y el jugador está en el suelo
            {
                Sonidos.instancia.PlaySalto(); // Reproducir el sonido de salto
                EnSuelo = false; 
                this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, FuerzaSalto)); 
            }
            animator = this.transform.GetChild(0).GetComponent<Animator>(); // Encontrar el animator del jugador
            animator.SetBool(animationTag, true); // Inicializar la animación del jugador
           
        }
        else
        {
            // Si el juego no ha comenzado, el jugador no se mueve
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, this.GetComponent<Rigidbody2D>().velocity.y);
            animator = this.transform.GetChild(0).GetComponent<Animator>(); // Encontrar el animator del jugador
            animator.SetBool(animationTag, false); // Inicializar la animación del jugador
        }
    }

    private void OnCollisionEnter2D(Collision2D c1) // Cuando el jugador colisiona con otro objeto
    {
        EnSuelo = true;
        if(c1.gameObject.tag == "Obstaculo") // Si el objeto con el que colisiona el jugador tiene la etiqueta "Obstaculo"
        {
            GameObject.Destroy(this.gameObject); // Destruir al jugador
        }
    }
    private void OnTriggerEnter2D(Collider2D c1) // Cuando el jugador entra en contacto con otro objeto
    {
        EnSuelo = true; // El jugador está en el suelo
        if(c1.tag == "Obstaculo") // Si el objeto con el que colisiona el jugador tiene la etiqueta "Obstaculo"
        {
            GameObject.Destroy(this.gameObject); // Destruir al jugador
            Sonidos.instancia.PlayGolpe(); // Reproducir el sonido de golpe
        }
    }
}
