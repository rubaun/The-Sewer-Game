//namespaces
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject telaMorte;
    public GameObject gameOver;
    public GameObject player;
    //public PlayerMovement playerMovement;
    public Soldier playerMovement;
    public GameObject prefab;
    public PlayerSound audioSource;

    void Start() 
    {
        playerMovement = player.GetComponent<Soldier>();
        audioSource = GetComponent<PlayerSound>();
    }

    void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu();
        }

        if(!playerMovement.VerificaSePlayerEstaVivo())
        {
            if(playerMovement.GameOver())
            {
                TelaGameOver();
            }
            else
            {
                TelaDeMorte();
            }
        }
    }

    public void PlayGame()
    {
        //audioSource.MouseOverSound();
        SceneManager.LoadScene("Fase1");
    }

    public void MenuPrincipal()
    {
        //audioSource.MouseOverSound();
        SceneManager.LoadScene("Menu");
    }

    public void SairGame()
    {
        //audioSource.MouseOverSound();
        Application.Quit();
    }

    public void PauseMenu()
    {
        if(pauseMenu != null)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0.0f;
        }        
    }

    public void Continue()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void TelaDeMorte()
    {
        if(telaMorte != null)
        {
            telaMorte.SetActive(true);
        }
        
    }

    public void TelaGameOver()
    {
        if(gameOver != null)
        {
            gameOver.SetActive(true);
        }
    }

    public void Replay()
    {
        if(prefab != null)
        {
            Instantiate(prefab, 
                    playerMovement.PlayerPositionInicial(), 
                    Quaternion.identity);
            player = GameObject.FindGameObjectWithTag("Player");
            playerMovement = player.GetComponent<Soldier>();
            telaMorte.SetActive(false); 
        }
    }
}
