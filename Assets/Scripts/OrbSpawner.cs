using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrbSpawner : MonoBehaviour
{
    [SerializeField] private GameObject blueOrbPrefab;
    [SerializeField] private GameObject pinkOrbPrefab;
    [SerializeField] private GameObject player;

    [SerializeField] private GameObject timerText;
    [SerializeField] private GameObject gameOverPanel;

    public int score = 0;
    public bool gameIsRunning = true;
    private float timer = 60;
    private float counter = 0;

    private void Update()
    {
        if (gameIsRunning)
        {
            timer -= Time.deltaTime;
            timerText.GetComponent<Text>().text = (Mathf.Ceil(timer)).ToString(); //update timer with whole number form

            if(timer <= 0)
            {
                GameOver(); //run game over sequence
                gameIsRunning = false;
            }
            
            counter += Time.deltaTime;
            if (counter >= 2)
            {
                counter = 0; //reset counter

                Vector2 spawnDir = Random.insideUnitCircle;
                Vector3 spawnPos = player.transform.position + (20 * new Vector3(spawnDir.x, 0, spawnDir.y));
                spawnPos = new Vector3(spawnPos.x, Random.Range(0f, 2f), spawnPos.z);
                bool spawnBlue = Random.value > 0.5f;
                if (spawnBlue)
                {
                    //spawn blue orb
                    var newOrb = Instantiate(blueOrbPrefab, spawnPos, Quaternion.identity);
                    newOrb.GetComponent<OrbAI>().orbSpawner = this; //initialize this
                    newOrb.GetComponent<OrbAI>().player = player; //initialize player location
                    newOrb.GetComponent<OrbAI>().isBlue = true; //initialize color bool
                }
                else
                {
                    //spawn pink orb
                    var newOrb = Instantiate(pinkOrbPrefab, spawnPos, Quaternion.identity);
                    newOrb.GetComponent<OrbAI>().orbSpawner = this; //initialize this
                    newOrb.GetComponent<OrbAI>().player = player; //initialize player location
                    newOrb.GetComponent<OrbAI>().isBlue = false; //initialize color bool
                }
            }
        }
    }

    private void GameOver()
    {
        //update game over panel before displaying screen
        gameOverPanel.transform.GetChild(1).GetComponent<Text>().text = "Your Final Score Was: " + score.ToString();

        timerText.SetActive(false);
        gameOverPanel.SetActive(true);
    }

    public void UpdateScore() => timerText.transform.GetChild(0).GetComponent<Text>().text = "Score: " + score.ToString();

    public void PlayAgain()
    {
        score = 0; //reset score
        timer = 60; //reset timer

        gameOverPanel.SetActive(false);
        timerText.SetActive(true);

        gameIsRunning = true;
    }

}
