using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {

    public Image energyBar;
    private float energy;
    private float maxEnergy;

    public Button jumpButton;
    private int canJump;

    public Button interactButton;
    private bool interactVisible;

    private PlayerControls3d pc3d;
    private Interact interact;

    public Sprite[] gotchas;
    public Image gotcha;

    public Image warning;
    public Image death;
    public Sprite[] deathsprite;
	void Start ()
    {
        maxEnergy = 100f;
        pc3d = GameObject.Find("Player3D").GetComponent<PlayerControls3d>();
        canJump = PlayerPrefs.GetInt("JumpUnlocked");
        canJump = 1; //DEBUG VALUE
        interact = interactButton.gameObject.GetComponent<Interact>();
        if(canJump == 1)
        {
            jumpButton.interactable = true;
        }
	}
    public void ShowDeath()
    {
        death.gameObject.SetActive(true);
        StartCoroutine(DeathAnimation());
    }
    IEnumerator DeathAnimation()
    {
        death.sprite = deathsprite[1];

        yield return new WaitForSeconds(1.0f);

        death.sprite = deathsprite[0];

        yield return new WaitForSeconds(1.0f);

        StartCoroutine(DeathAnimation());
    }
    public void SetInteract(int i)
    {
        interactButton.gameObject.SetActive(true);
        interactButton.interactable = true;
        interact.interaction = i;
    }

    public void ShowGotcha(int i)
    {
        gotcha.gameObject.SetActive(true);
        gotcha.sprite = gotchas[i];
        StartCoroutine(GotchaTime());
    }
    IEnumerator GotchaTime()
    {
        yield return new WaitForSeconds(4.0f);
        gotcha.gameObject.SetActive(false);
    }
    public void HideInteract()
    {
        
        interactButton.interactable = false;
        interactButton.gameObject.SetActive(false);
    }

    public void GetEnergy(float f)
    {
        energy = f;
        if(energy < 30f)
        {
            warning.gameObject.SetActive(true);
        }
        if(energy >= 30f)
        {
            warning.gameObject.SetActive(false);
        }
        if(energy <= 0)
        {
            ShowDeath();
        }
        energyBar.fillAmount = energy / maxEnergy;
    }

    public void DebugMenu()
    {
        SceneManager.LoadScene("SpaceMenu");   
    }
}
