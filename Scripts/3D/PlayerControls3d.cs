using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class PlayerControls3d : MonoBehaviour{

    //JUMP VARIABLES
    private bool canJump;
    private bool jumpUnlocked;
    private float jumpPower;
    private float airControl;
    public bool grounded;

    //MOVEMENT VARIABLES
    public float movementSpeed;
    private float baseSpeed;
    public bool moving;
    public bool canMove;

    //ENERGY
    private float energy;
    private float maxEnergy;
    private float batteryGrade;
    
    //COMPONENTS
    private Rigidbody rb;
    private PlayerUI pui;

    //OTHER
    private string levelName;
    public Transform spawnPoint;
    public bool toShip;

    //PARTS COLLECTED
    private int batteryCollected;
    private Puzzle2Script p2s;

    void Awake()
    {
        //GETS COMPONENTS
        rb = GetComponent<Rigidbody>();
        levelName = SceneManager.GetActiveScene().name;
        pui = GameObject.Find("Canvas").GetComponent<PlayerUI>();
        if(levelName == "CrossRoads" && PlayerPrefs.GetString("XroadsSpawn") != "")
        {
            spawnPoint = GameObject.Find(PlayerPrefs.GetString("XroadsSpawn")).transform;
        }
        else
        {
            spawnPoint = GameObject.Find("SpawnPoint").transform;
        }
        transform.position = spawnPoint.position;
        try
        {
          p2s = GameObject.Find("Puzzle2").GetComponent<Puzzle2Script>();
        }
        catch { }
    }

    void Start ()
    {
        Screen.orientation = ScreenOrientation.Landscape;
        grounded = true;
        toShip = false;
        GetCollected();
        //SETS START VALUES
        canJump = true;
        canMove = true;
        jumpUnlocked = true; //DEBUG VALUE
        jumpPower = 350f;
        airControl = 1f;
        baseSpeed = 5f;
        maxEnergy = 100f;
        energy = 100f;
        pui.GetEnergy(energy);
        

	}
	void GetCollected()
    {
        batteryCollected = PlayerPrefs.GetInt("BatteryCollected");
    }
	// Update is called once per frame
	void Update ()
    {
        Movements();
        ToShipEffect();
        if(energy == 0)
        {
            canMove = false;
        }
	}
    public void StartBeaming()
    {
        toShip = true;
    }
    void ToShipEffect()
    {
        if(toShip)
        {
            canJump = false;
            canMove = false;
            rb.velocity = new Vector3(rb.velocity.x, 10f, rb.velocity.z);
        }
    }
    void Movements()
    {
        //SETS MOVEMENT SPEED
        movementSpeed = baseSpeed;
        //REMOVES ENERGY IF MOVING
        if(moving && levelName != "SpaceShip")
        {
            energy -= 1 * Time.deltaTime;
            pui.GetEnergy(energy);
            if(energy < 0)
            {
                energy = 0;
            }
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
       
    }


    public void Jump()
    {
        if(canJump && energy >= 5f)
        {
            canJump = false;
            grounded = false;
            GameObject.Find("PlayerBody").GetComponent<Animator>().SetBool("isGrounded", false);
            rb.AddForce(Vector3.up * jumpPower);
            airControl = 0.25f;
            energy -= 5;
            pui.GetEnergy(energy);
        }
    }
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Ground")
        {
            canJump = true;
            grounded = true;
            airControl = 1f;
            GameObject.Find("PlayerBody").GetComponent<Animator>().SetBool("isGrounded", true);

        }
        if(col.gameObject.tag == "Critter1")
        {
            pui.ShowGotcha(0);
            PlayerPrefs.SetInt("Critter1", 1);
            Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "Critter2")
        {
            pui.ShowGotcha(1);
            PlayerPrefs.SetInt("Critter2", 1);
            Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "Critter3")
        {
            pui.ShowGotcha(2);
            PlayerPrefs.SetInt("Critter3", 1);
            Destroy(col.gameObject);
        }
    }

    void OnTriggerStay(Collider col)
    {
        if(col.gameObject.tag == "EnergyStation")
        {
            if(energy < maxEnergy)
            {
                energy += 10f * Time.deltaTime;              
            }
            if(energy > maxEnergy)
            {
                energy = maxEnergy;
            }
            pui.GetEnergy(energy);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "SpacePad")
        {
            pui.SetInteract(0);
        }
        if (col.gameObject.tag == "SpacePad2")
        {
            pui.SetInteract(1);
        }
        if (col.gameObject.tag == "ShipCMD")
        {
            pui.SetInteract(2);
        }
        if (col.gameObject.tag == "P1T1")
        {
            pui.SetInteract(3);
        }
        if (col.gameObject.tag == "P1T2")
        {
            pui.SetInteract(4);
        }
        if (col.gameObject.tag == "P1T3")
        {
            pui.SetInteract(5);
        }
        if (col.gameObject.tag == "Planet1")
        {
            pui.SetInteract(6);
        }
        if(col.gameObject.tag == "Planet2")
        {
            pui.SetInteract(7);
        }
        if (col.gameObject.tag == "P2R")
        {
            p2s.counter = 1;
            p2s.moving = true;
        }
        if (col.gameObject.tag == "P2L")
        {
            p2s.counter = 2;
            p2s.moving = true;
        }
        if (col.gameObject.tag == "P2U")
        {
            p2s.counter = 3;
            p2s.moving = true;
        }
        if (col.gameObject.tag == "P2B")
        {
            p2s.counter = 4;
            p2s.moving = true;
        }
        if (col.gameObject.tag == "P2B")
        {
            p2s.counter = 4;
            p2s.moving = true;
        }
        if (col.gameObject.tag == "P2Beam")
        {
            p2s.BeamSet();
        }
        if(col.gameObject.tag == "ToCave")
        {
            pui.SetInteract(8);
        }
        if(col.gameObject.tag == "FromCave")
        {
            pui.SetInteract(6);
        }
        if(col.gameObject.tag == "ToSea")
        {
            pui.SetInteract(7);
        }
        if(col.gameObject.tag == "FromSea")
        {
            pui.SetInteract(10);
        }
        if (col.gameObject.tag == "TrainToXr")
        {
            pui.SetInteract(11);
        }
        if (col.gameObject.tag == "Train2CAve")
        {
            pui.SetInteract(9);
        }
    }
    void OnTriggerExit(Collider col)
    {
        if(col.gameObject.tag == "SpacePad")
        {
            pui.HideInteract();
        }
        if (col.gameObject.tag == "SpacePad2")
        {
            pui.HideInteract();
        }
        if (col.gameObject.tag == "ShipCMD")
        {
            pui.HideInteract();
        }
        if (col.gameObject.tag == "P1T1")
        {
            pui.HideInteract();
        }
        if (col.gameObject.tag == "P1T2")
        {
            pui.HideInteract();
        }
        if (col.gameObject.tag == "P1T3")
        {
            pui.HideInteract();
        }
        if (col.gameObject.tag == "Planet1")
        {
            pui.HideInteract();
        }
        if (col.gameObject.tag == "Planet2")
        {
            pui.HideInteract();
        }
        if (col.gameObject.tag == "FromCave")
        {
            pui.HideInteract();
        }
        if(col.gameObject.tag =="ToCave")
        {
            pui.HideInteract();
        }
        if (col.gameObject.tag == "ToSea")
        {
            pui.HideInteract();
        }
        if (col.gameObject.tag == "FromSea")
        {
            pui.HideInteract();
        }
        if (col.gameObject.tag == "Cave")
        {
            pui.HideInteract();
        }
        if (col.gameObject.tag == "TrainToXr")
        {
            pui.HideInteract();
        }
        if (col.gameObject.tag == "Train2CAve")
        {
            pui.HideInteract();
        }
        if (col.gameObject.tag == "P2R")
        {
            p2s.moving = false;
            p2s.counter = 0;
        }
        if (col.gameObject.tag == "P2L")
        {
            p2s.moving = false;
            p2s.counter = 0;
        }
        if (col.gameObject.tag == "P2U")
        {
            p2s.moving = false;
            p2s.counter = 0;
        }
        if (col.gameObject.tag == "P2B")
        {
            p2s.moving = false;
            p2s.counter = 0;
        }
    }
}
