using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnableCritters : MonoBehaviour {

    private GameObject critter1;
    private GameObject critter2;
    private GameObject critter3;

    private int c1;
    private int c2;
    private int c3;

    private Scene sc;
    public string s;
	// Use this for initialization
	void Start ()
    {
        sc = SceneManager.GetActiveScene();
        s = sc.name;
        c1 = PlayerPrefs.GetInt("Critter1");
        c2 = PlayerPrefs.GetInt("Critter2");
        c3 = PlayerPrefs.GetInt("Critter3");
        Debug.Log("Hello" + c1 + c2 + c3);

        if (s == "SpaceShip")
        {
            try
            {
                critter1 = GameObject.Find("Critter");
                critter2 = GameObject.Find("Critter1");
                critter3 = GameObject.Find("Critter2");

                if(PlayerPrefs.GetInt("Critter1") == 0)
                {
                    critter1.SetActive(false);
                }
                if (PlayerPrefs.GetInt("Critter2") == 0)
                {
                    critter2.SetActive(false);
                }
                if (PlayerPrefs.GetInt("Critter3") == 0)
                {
                    critter3.SetActive(false);
                }
            }
            catch
            {

            }
        }
        if(s == "Cave2")
        {
            try
            {
                critter1 = GameObject.Find("Critter");
                if (PlayerPrefs.GetInt("Critter2") == 1)
                {
                    critter1.SetActive(false);
                }
            }
            catch
            {

            }     
        }
        if (s == "Cave")
        {
            try
            {
                critter1 = GameObject.Find("Critter");
                if (PlayerPrefs.GetInt("Critter1") == 1)
                {
                    critter1.SetActive(false);
                }
            }
            catch
            {

            }
            
        }
        if (s == "Sea")
        {
            try
            {
                critter1 = GameObject.Find("Critter");
                if (PlayerPrefs.GetInt("Critter3") == 1)
                {
                    critter1.SetActive(false);
                }
            }
            catch
            {

            }    
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
