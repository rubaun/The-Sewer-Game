//namespaces
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject telaMorte;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject player;
    [SerializeField] private Soldier playerMovement;
    [SerializeField] private GameObject prefab;
    [SerializeField] private PlayerSound audioSource;
    [SerializeField] private string faseAtual;
    private GameObject buttonContinue;
    private bool pauseMenuAtivo;


    void Start() 
    {
        pauseMenuAtivo = false;

        if (SceneManager.GetActiveScene().name != "Menu")
        {
            Soldier player = FindObjectOfType<Soldier>();
            playerMovement = player.GetComponent<Soldier>();
            faseAtual = player.GetFase();
        }

        audioSource = GetComponent<PlayerSound>();

        

        if (SaveSystem.SaveExists() && SceneManager.GetActiveScene().name == "Menu")
        {
            buttonContinue = GameObject.Find("Continue");
            buttonContinue.GetComponent<Button>().interactable = true;
            buttonContinue.GetComponent<Image>().color = new Color(0, 147, 255, 255);
            buttonContinue.GetComponent<Button>().onClick.AddListener(ContinueGame);
            
        }
        else if(SceneManager.GetActiveScene().name == "Menu")
        {
            buttonContinue = GameObject.Find("Continue");
            buttonContinue.GetComponent<Image>().color = new Color(255, 255, 255, 255);
            buttonContinue.GetComponent<Button>().interactable = false;
        }
    }

    void Update() 
    {
        if(SceneManager.GetActiveScene().name != "Menu")
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                PauseMenu();
            }

            if (!playerMovement.VerificaSePlayerEstaVivo())
            {
                if (playerMovement.GameOver())
                {
                    TelaGameOver();
                }
                else
                {
                    TelaDeMorte();
                }
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
        if(!pauseMenu)
        {
            pauseMenuAtivo = !pauseMenuAtivo;
            pauseMenu.SetActive(true);
            Time.timeScale = 0.0f;
        }
        else if(pauseMenu)
        {
            pauseMenuAtivo = !pauseMenuAtivo;
            pauseMenu.SetActive(false);
            Time.timeScale = 1.0f;
        }
    }

    public void ContinueGame()
    {
        if(SaveSystem.SaveExists())
        {
            GameController gameController = GetComponent<GameController>();
            gameController.LoadGame();
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
        /*if(prefab != null)
        {
            Instantiate(prefab, 
                    playerMovement.PlayerPositionInicial(), 
                    Quaternion.identity);
            player = GameObject.FindGameObjectWithTag("Player");
            playerMovement = player.GetComponent<Soldier>();
            telaMorte.SetActive(false); 
        }*/

        
        SceneManager.LoadScene(faseAtual);
    }
}
