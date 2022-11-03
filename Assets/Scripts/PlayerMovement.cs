using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // Henter inputsystem pakken

public class PlayerMovement : MonoBehaviour
{
  
    Vector2 movement; // variabel som kan holde en 2D-vektor værdi. 
    Rigidbody2D snailBody; // variabel som kan holde et rigid-body component.
    Animator snailAnimator; // variabel som kan holde et animator component.

    [SerializeField] float movementSpeed; // int-variabel som skal definerer sneglens hastighed. Fungerer som en "vektor-skalar".

    //int-variabler til timer
    float timer;        
    float seconds;  
    float minutes;  

    float snailPosition; // float variabel som holder styr på spillerens position på x-aksen.
    bool finished; // bool som holder styr på om spilleren er kommet i mål. 
    

    private void Awake()
    {
        snailBody = GetComponent<Rigidbody2D>(); // snailBody får Rigidbody2D componentet som ligger på "Doug the Slug" i Unity. 
        snailAnimator = GetComponent<Animator>(); // snailAnimator får Animator componentet som ligger på "Doug the Slug" i Unity.

        Debug.Log("READY! SET! GO!"); // skriver dette i konsollen. 

    }

    private void Start()
    {
        timer = 0;          // timer nulstilles. 
        finished = false;   // Rimelig self-explanatory.
    }


    void OnMovement(InputValue value) // funktion som gemmer spillerens inputs i movement variablen.
    {
        movement = value.Get<Vector2>(); // movement får en Vector2-værdi baseret på spillerens inputs.
        
        if (movement.x != 0) // Condition som er true når spilleren bevæger sig. 
        {
            snailAnimator.SetFloat("X", movement.x); // sætter "X" parameteren i Animatoren til movements x-vektor værdi.

            snailAnimator.SetBool("isMoving", true); // sætter "isMoving" parameteren i Animatoren til true.
        }
        else
        {
            snailAnimator.SetBool("isMoving", false); // sætter "isMoving" parameteren i Animatoren til false. 
        }
    
    }

     private void FixedUpdate()
    {
        snailBody.velocity = movement * movementSpeed; // Rigidbodyens velocity får vektoren fra "movement" og ganger denne med skalaren fra "movementspeed". 

        snailPosition = transform.position.x; // snailPosition holder styr på spillerens position ved at hente den fra objektets transform. 

        if (snailPosition > 6.5) // condition som er true når spilleren når hen til målstregen. 
        {
            finished = true; // Sætter finished til true.
            Debug.LogFormat("Congratulations! You finished in {0} seconds! That's {0} seconds of you life that you will never have back :')", timer); // Printer spillerens tid til console når spilleren kommer i mål.
         
        }

        PlayerTimeCalculator();

    }

    void PlayerTimeCalculator() // funktion som holder styr på spillerens tid. En slags stopur. 
    {
        if (finished == false) // condition som er true når finished er false. Finished bliver sat til false oppe i start(). 
        {
            timer = Time.fixedTime; // Starter en timer som opbevares i timer-variablen. 
        }
    }



}
