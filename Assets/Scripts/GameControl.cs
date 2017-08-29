using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour 
{
	public static GameControl instance;         //A reference to our game control script so we can access it statically.
	public Text scoreText1;                     //A reference to the UI text component that displays the yellow player score.
	public Text scoreText2;                     //A reference to the UI text component that displays the green player score.
	public Text highScore;						//A reference to the UI text component that displays the player's high score.
	public GameObject gameOvertext;             //A reference to the object that displays the text which appears when the player dies.

	private int score1 = 0;                		//The yellow player's score.
	private int score2 = 0;                		//The green player's score.
	private int cont = 0;						//Control for increment speed
	private int aux = 0;						//Count how many players died
	public bool gameOver = false;              	//Is the game over?
	public float scrollSpeed = -1.5f;			//Defalt scroll speed. 


	void Awake()
	{
		//If we don't currently have a game control...
		if (instance == null)
			//...set this one to be it...
			instance = this;
		//...otherwise...
		else if(instance != this)
			//...destroy this one because it is a duplicate.
			Destroy (gameObject);
	}

	void Update()
	{
		//increment speed every 10 points
		if (cont > 10) {
			scrollSpeed -= 0.05f;
			cont = 0;
		}

		//If the game is over and the player has pressed some input...
		if (gameOver && Input.GetKey(KeyCode.Space)) {
			//...reload the current scene.
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		}

		//Competitive high score
		if (score1 > PlayerPrefs.GetInt("HighScore1", 0)) {
			PlayerPrefs.SetInt ("HighScore1", score1);
		}
		if (score2 > PlayerPrefs.GetInt("HighScore2", 0)) {
			PlayerPrefs.SetInt ("HighScore2", score2);
		}
		if (PlayerPrefs.GetInt ("HighScore1") > PlayerPrefs.GetInt ("HighScore2")) {
			highScore.text = "Recorde: " + PlayerPrefs.GetInt ("HighScore1", 0).ToString();
		} else {
			highScore.text = "Recorde: " + PlayerPrefs.GetInt ("HighScore2", 0).ToString();
		}
	}

	public void Bird1Scored()
	{
		//The bird can't score if the game is over.
		if (gameOver)
			return;
		//If the game is not over, increase the score...
		score1++;
		cont++;
		//...and adjust the score text.
		scoreText1.text = "Amarelo: " + score1.ToString ();
	}

	public void Bird2Scored()
	{
		//The bird can't score if the game is over.
		if (gameOver)
			return;
		//If the game is not over, increase the score...
		score2++;
		cont++;
		//...and adjust the score text.
		scoreText2.text = "Verde: " + score2.ToString ();
	}

	public void BirdDied()
	{
		aux++;
		if (aux == 2) {
			//Activate the game over text.
			gameOvertext.SetActive (true);
			//Set the game to be over.
			gameOver = true;
		}
	}
}