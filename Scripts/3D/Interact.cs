using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Interact : MonoBehaviour {

    public int interaction;
    private LevelManager lmanager;
    private PlayerControls3d pc3d;
    private Puzzle1Script p1s;


	void Start ()
    {
        lmanager = GameObject.Find("ScriptBlock").GetComponent<LevelManager>();
        interaction = 0;
        pc3d = GameObject.Find("Player3D").GetComponent<PlayerControls3d>();
        try
        {
            p1s = GameObject.Find("Puzzle1").GetComponent<Puzzle1Script>();
        }
        catch { }


    }

    public void InteractFunction()
    {
        switch(interaction)
        {
            case 0:
                lmanager.CallWaitTimes("SpaceShip");
                pc3d.toShip = true;
                break;
            case 1:
                lmanager.CallWaitTimes("Planet");
                break;
            case 2:
                lmanager.CallWaitTimes("SpaceMenu");
                break;
            case 3:
                p1s.AddIcon(0);
                break;
            case 4:
                p1s.AddIcon(1);
                break;
            case 5:
                p1s.AddIcon(2);
                break;
            case 6:
                lmanager.CallWaitTimes("CrossRoads");
                PlayerPrefs.SetString("XroadsSpawn", "SpawnPoint1");
                //change spawn point in level
                break;
            case 7:
                lmanager.CallWaitTimes("Sea");
                //change spawn point in level
                break;
            case 8:
                lmanager.CallWaitTimes("Cave");
                break;
            case 9:
                lmanager.CallWaitTimes("Cave2");
                break;
            case 10:
                lmanager.CallWaitTimes("CrossRoads");
                PlayerPrefs.SetString("XroadsSpawn", "SpawnPoint2");
                break;
            case 11:
                lmanager.CallWaitTimes("CrossRoads");
                PlayerPrefs.SetString("XroadsSpawn", "SpawnPoint3");
                break;
        }
    }
}
