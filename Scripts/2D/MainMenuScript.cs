using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {

    public Text stars;
    public Text animals;
    private int crits;
    private int c1;
    private int c2;
    private int c3;
    public Image FadeScreen;
    public Color[] clrs;
    private int b;
	// Use this for initialization
	void Start () {
        c1 = PlayerPrefs.GetInt("Critter1");
        c2 = PlayerPrefs.GetInt("Critter2");
        c3 = PlayerPrefs.GetInt("Critter3");
        crits = c1 + c2 + c3;
        Screen.orientation = ScreenOrientation.Landscape;
        GetStarsAnimals();
        StartCoroutine(WaitForIt());
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (b == 2)
        {
            FadeScreen.color = Color.Lerp(FadeScreen.color, clrs[1], Time.deltaTime * 4f);
        }
        if(b == 1)
        {
            FadeScreen.color = Color.Lerp(FadeScreen.color, clrs[0], Time.deltaTime * 4f);
        }
	}
    IEnumerator WaitForIt()
    {
        yield return new WaitForSeconds(1.0f);
        b = 1;
    }
    public void StartGame()
    {
        b = 2;
        StartCoroutine(Wait());
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("CrossRoads");
    }
    public void Musics()
    {

    }
    public void Sounds()
    {

    }
    public void GetStarsAnimals()
    {
        animals.text = "animals collected: " + crits + "/3";
        stars.text = "stars collected: " + "0" + "/3";
    }

}
