using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pelota_script : MonoBehaviour
{
    //Velocidad inicial de la pelota
    public float speed = 5f;

    //Variables rebote 
    public int bounce_min = 1;
    public int bounce_max = 6;
    public int player_bounces = 1;

    private PhysicsMaterial2D material;

    //Variable factor cambio de velocidad (magnitud de vector velocidad)
    public float factorCambioVelocidad = 1.2f;

    //Velocidad máxima alcanzable por la pelota
    public float maxVelocidad = 50f;


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = Random.insideUnitCircle.normalized * speed;
        material = GetComponent<CircleCollider2D>().sharedMaterial;
    }

    // Update is called once per frame
    //Se mantiene para motivios de testing y debugging
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        //Obtener componente Rigidbody2D para obtener velocidad del objeto
        Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();
        Debug.Log("Velocidad pelota: " + rigidBody.velocity.magnitude);

        //Limitar velocidad máxima del objeto para evitar problemas de colisión
        if (rigidBody.velocity.magnitude > factorCambioVelocidad)
            rigidBody.velocity = Vector3.ClampMagnitude(rigidBody.velocity, maxVelocidad);

    }

    //Funci�n inicial de prueba
    void Respawn()
    {

        player_bounces = (int) Random.Range(bounce_min, bounce_max);
        transform.position = Vector3.zero;

        GetComponent<Rigidbody2D>().velocity = Random.insideUnitCircle.normalized * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player_bounces--;
        }

        if(player_bounces== 0 || collision.gameObject.CompareTag("Piso"))
        {
            Debug.Log("La pelota ha sido destruida");
            Destroy(gameObject);
            
        }
    }

    public void aumentarVelocidad(float pFactorAumento)
    {

        GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity * pFactorAumento;
    }

    public void disminuirVelocidad(float pFactorAumento)
    {

        GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity / pFactorAumento;
    }

    public void cambiarTamano(float tamano)
    {
        Vector3 nuevoTamano = new Vector3(tamano, tamano, 1);
        GetComponent<Transform>().localScale = nuevoTamano; 
    }

    public void cambiarRebote(float pRebote)
    {
        PhysicsMaterial2D nuevoMaterial = Instantiate(GetComponent<CircleCollider2D>().sharedMaterial);

        nuevoMaterial.bounciness = pRebote;

        GetComponent<CircleCollider2D>().sharedMaterial = nuevoMaterial;

    }
}
