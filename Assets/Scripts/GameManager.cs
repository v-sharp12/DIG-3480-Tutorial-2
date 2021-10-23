using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Manager Links")]
    public GameObject winScreen;
    public GameObject loseScreen;
    public GameObject player;
    public GameObject lvl01Group;
    public GameObject lvl02Group;
    public Transform spawnPoint;
    public AudioSource leveltrack;
    public AudioSource wintrack;

    [Header("Manager Variables")]
    public bool lvlOneComplete;
    public bool lvlTwoComplete;
    public bool gameWin;
    void Start()
    {
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        lvl01Group.SetActive(true);
        lvl02Group.SetActive(false);
        
        lvlOneComplete = false;
        lvlTwoComplete = false;
        gameWin = false;

        leveltrack.enabled = true;
        wintrack.enabled = false;

        
    }

    // Update is called once per frame
    void Update()
    {
        lvlControl();
        if (Input.GetKey(KeyCode.Escape))
        {
            Debug.Log("Game Closed.");
            Application.Quit();
        }
    }

    public void lvlControl()
    {
        if (player.GetComponent<PlayerScript>().scoreValue >= 4 && !lvlOneComplete && !lvlTwoComplete)
        {
            lvlOneComplete = true;
            lvl01Group.SetActive(false);
            lvl02Group.SetActive(true);
            player.GetComponent<PlayerScript>().scoreValue = 0;
            player.GetComponent<PlayerScript>().lives = 3;
            player.transform.position = spawnPoint.transform.position;
        }
        if (player.GetComponent<PlayerScript>().scoreValue >= 4 && lvlOneComplete && !lvlTwoComplete)
        {
            lvlTwoComplete = true;
            gameWin = true;
        }
        if(gameWin)
        {
            winScreen.SetActive(true);
            player.SetActive(false);

            leveltrack.enabled = false;
            wintrack.enabled = true;
        }
        if(player.GetComponent<PlayerScript>().lives <= 0)
        {
            loseScreen.SetActive(true);
            player.SetActive(false);
        }
    }
}
