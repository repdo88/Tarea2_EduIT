using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{

    [SerializeField] private float velocidad = 5f; // Velocidad de movimiento
    private Vector3 move; // Vector de movimiento

    [SerializeField] private float rotacionVelocidad = 200f; // Velocidad de rotación
    private float rotacion; // Variable para la rotación

    // Variables para limitar movimiento del auto
    private float limIzquierda;
    private float limDerecha;
    private float limAdelante;
    private float limAtras;
    private float anchoPlano;
    private float largoPlano;
    public Transform plano; // Referencia al plano donde se mueve el auto

    // Start is called before the first frame update
    void Start()
    {
        Renderer planoRenderer = plano.GetComponent<Renderer>();
        float anchoPlano = planoRenderer.bounds.size.x; // Ancho del plano
        float largoPlano = planoRenderer.bounds.size.z; // Largo del plano

        Renderer autoRenderer = GetComponent<Renderer>();
        float largoAuto = autoRenderer.bounds.size.z / 2f; // Largo del auto (la mitad para centrarlo)

        float centroX = plano.position.x; // Centro del plano en X
        float centroZ = plano.position.z; // Centro del plano en Z

        limIzquierda = centroX - (anchoPlano / 2f) + largoAuto; // Límite izquierdo
        limDerecha = centroX + (anchoPlano / 2f) - largoAuto; // Límite derecho
        limAdelante = centroZ + (largoPlano / 2f) - largoAuto; // Límite adelante
        limAtras = centroZ - (largoPlano / 2f) + largoAuto; // Límite atrás
    }

    // Update is called once per frame
    void Update()
    {
        // Movimiento del auto
        move = new Vector3(0, 0, 0); // Reiniciar el vector de movimiento cada frame
        
        if (Input.GetKey(KeyCode.UpArrow))
        {
            move.z += 1; // Mover hacia adelante
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            move.z -= 1; // Mover hacia atrás
        }

        transform.Translate(move.normalized * velocidad * Time.deltaTime); // Mover el objeto

        Vector3 pos = transform.position; // Obtener la posición actual del auto
        pos.x = Mathf.Clamp(pos.x, limIzquierda, limDerecha); // Limitar movimiento en X
        pos.z = Mathf.Clamp(pos.z, limAtras, limAdelante); // Limitar movimiento en Z
        transform.position = pos; // Aplicar la posición limitada


        // Rotación del auto
        rotacion = 0f; // Reiniciar la rotación cada frame

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rotacion -= 1; // Rotar hacia la izquierda
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rotacion += 1; // Rotar hacia la derecha
        }

        transform.Rotate(new Vector3(0, rotacion * rotacionVelocidad * Time.deltaTime, 0)); // Aplicar la rotación

    }
}
