using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Al declararlo fuera del GameManager podemos llamarlo desde cualquier script
public enum GameState {
    menu,
    inGame,
    gameOver
}



public class GameManager : MonoBehaviour
{
    //Estado actual de la partida
    public GameState currentGameState = GameState.menu;

    //Creando este script en un singleton del game manager
    public static GameManager sharedInstance;

    private void Awake()
    {
        if (sharedInstance == null) {
            sharedInstance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentGameState = GameState.menu;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartGame();
        }
    }

    public void StartGame() {
        SetGameState(GameState.inGame);
    }

    public void GameOver() {
        SetGameState(GameState.gameOver);
    }

    public void BackToMenu() {
        SetGameState(GameState.menu);
    }

    //Cambiar el estado del juego
    //Esto es como un semaforo
    private void SetGameState(GameState newGameState) {
        if (newGameState == GameState.menu) {
            //TODO: colocar logica de menu
        } else if (newGameState == GameState.inGame) {
            //TODO: Colocar la lógica 
        } else if (newGameState == GameState.gameOver) {
            //TODO: colocar lógca de game over
        }

        this.currentGameState = newGameState;

    }

}
