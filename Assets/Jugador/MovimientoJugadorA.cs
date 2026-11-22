using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class MovimientoJugadorA : MonoBehaviour
{   
    public Transform destino;
    public int vidaMax;
    public float velocidad;
    public float fuerzaSalto;
    public Vector2 tamannoAtaque;
    public float distanciaAtaque;
    public float distanciaAtaque2;
    public LayerMask capaEnemigo;
    public float distanciaCaja;
    public Vector2 tamannoCaja;
    public LayerMask capaSuelo;
     Vector2 knockBack;
    GameObject aGolpear;
    bool saltando;
    bool atacando;
    int direccion;
    bool muerto = false;
    int DobleCount;
    Animator anim;
    Vector3 movimiento;
    Rigidbody2D cuerpo;
    SpriteRenderer sprite;
    int vidaRestante;
    void Start()
    {
        knockBack = Vector2.zero;
        vidaRestante = vidaMax;
        direccion = 1;
        movimiento = new Vector3();
        cuerpo = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        LeerMovimiento();
        LeerSalto();
        RotarPersonaje();
        EstaEnElSuelo();
        ActualizarAnimacion();
    }
    void ActualizarAnimacion()
    {
        if(!muerto){
            if(movimiento.x == 0)
            {
                anim.SetBool("Movimiento",false);
            }
            else
            {
                anim.SetBool("Movimiento",true);
            }
            if (!EstaEnElSuelo())
            {
                anim.SetBool("Saltando", true);
            }
            else
            {
                anim.SetBool("Saltando", false);
            }
            if (atacando && EstaEnElSuelo())
            {
                anim.SetTrigger("Ataque");
            }
            if(atacando && !EstaEnElSuelo())
            {
                anim.SetTrigger("AtaqueAire");
            }
        }
    }
    private void LeerSalto()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !saltando && EstaEnElSuelo())
        {
            saltando = true;
        }
    }
    private void RotarPersonaje()
    {
        if(!muerto){
            if (movimiento.x < 0)
            {
                sprite.flipX = true;
                direccion=-1;
            }
            else if (movimiento.x > 0)
            {
                sprite.flipX = false;
                direccion=1;
            }
        }
    }

    private void LeerMovimiento()
    {
        if (Input.GetKey(KeyCode.A))
        {
            movimiento = Vector3.left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            movimiento = Vector3.right;
        }
        else
        {
            movimiento = new Vector3();
        }
        if(Input.GetKeyDown(KeyCode.J)){
            
            atacando=true;
            
        }
        else
        {
            atacando=false;
        }
        if(muerto){
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    void FixedUpdate()
    {
        if (muerto)
        {
            cuerpo.velocity = Vector2.zero;
            return;
        }

        cuerpo.velocity = new Vector2(movimiento.x * velocidad, cuerpo.velocity.y);
        if(saltando)
        {
            cuerpo.AddForce(Vector2.up * fuerzaSalto);
            saltando = false;
        }
        
    }   
    public void RecibeDanno(int cuanto)
    {
         if (muerto)
        {
            return;
        }

    vidaRestante -= cuanto;

    Debug.Log("Ouch!! Vida restante: " + vidaRestante);

    if (vidaRestante <= 0)
    {
        muerto = true;
        anim.SetTrigger("Muerte");

        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        Invoke("gameover", 2f);
        return;
    }

    anim.SetTrigger("Dolor");

    }
    void gameover()
    {
        Camera.main.transform.position = destino.position;
    }
    public void Espadazo()
    {
        if (aGolpear != null) aGolpear.GetComponent<MovimientoEnemigoM>().RecibeDanno(1);
    }
    public void setVidaRestante(int fuera)
    {
        vidaRestante = fuera;

        if (vidaRestante <= 0 && !muerto)
        {
            muerto = true;
            anim.SetTrigger("Muerte");

            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<Rigidbody2D>().gravityScale = 0;
            Invoke("gameover", 2f);
        }
    }

    bool EstaEnElSuelo()
    {  
        RaycastHit2D rc = Physics2D.BoxCast(
            transform.position, tamannoCaja, 0, Vector2.down, distanciaCaja, capaSuelo);
        if (rc)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private bool DetectarEnemigo()
    {
        RaycastHit2D rayo = Physics2D.BoxCast(
        transform.position + Vector3.down * distanciaAtaque2,
        tamannoAtaque,
        0,
        Vector2.right * direccion,
        distanciaAtaque,
        capaEnemigo
    );

        if (rayo) {
            aGolpear = rayo.rigidbody.gameObject;
            //jugador.GetComponent<MovimientoJugador>().RecibeDanno(0, transform.position);
            return true;
        }
        else
        {
            aGolpear = null;
        }
        return false;
    }



    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + Vector3.down * distanciaCaja, tamannoCaja);
        if (DetectarEnemigo())
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawWireCube(
    transform.position + Vector3.right * direccion * distanciaAtaque
    + Vector3.down * distanciaAtaque2,
    tamannoAtaque
    );
     Gizmos.color = Color.white;
    }
}
