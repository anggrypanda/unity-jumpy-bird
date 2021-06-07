using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour 
{
	public static GameplayController instance;

	public Text scoreText, bestScoreText, diamondScoreText, totalDiamondScoreText;
	private int countScore, countDiamond;

	public GameObject mainMenuObj, gameOverObj, birdMenuObj, UIObj;

	[HideInInspector]
	public bool playGame;

	public Text mainMenuBestScoreText, birdMenuBestScoreText,
		birdMenu_DisplayDiamondScoreText;

	public GameObject[] birds;
	public GameObject[] birdPriceText;
	public GameObject[] birdIcons;

	void Awake() 
	{
		MakeInstance ();
	}

	void Start () 
	{
		mainMenuBestScoreText.text = "Best: " + GameManager.instance.bestScore;

		InstantiatePlayer ();
	}
	
	void MakeInstance() 
	{
		if (instance == null) 
		{
			instance = this;
		}
	}

	public void DisplayScore(int score, int diamond) 
	{
		countScore += score;
		countDiamond += diamond;

		scoreText.text = countScore.ToString();
		diamondScoreText.text = countDiamond.ToString();
	}

	public void GameOver() 
	{
		playGame = false;

		gameOverObj.SetActive(true);
		gameOverObj.GetComponent<Animator>().Play (TagManager.FADE_IN_ANIMATION);

		int currentBestScore = GameManager.instance.bestScore;

		if (currentBestScore < countScore) 
		{
			GameManager.instance.bestScore = countScore;
		}
		GameManager.instance.diamondScore += countDiamond;

		if (countScore >= 25) 
		{
			GameManager.instance.birds[GameManager.instance.birds.Length - 1] = true;
		}
		GameManager.instance.SaveGameData ();
	}

	public void PlayGame() 
	{
		if (!playGame) 
		{	
			mainMenuObj.SetActive(false);
			UIObj.SetActive(true);

			playGame = true;

			bestScoreText.text = "Best: " + GameManager.instance.bestScore;
			totalDiamondScoreText.text = "Total: " + GameManager.instance.diamondScore;
		}
	}

	public void RestartGame() 
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);
	}

	public void MainMenu() 
	{
		gameOverObj.SetActive(false);
		birdMenuObj.SetActive(false);
		mainMenuObj.SetActive(true);

		InstantiatePlayer ();
	}

	public void BirdMenu() 
	{
		birdMenuObj.SetActive(true);
		mainMenuObj.SetActive(false);

		birdMenuBestScoreText.text = "Best: " + GameManager.instance.bestScore;
		birdMenu_DisplayDiamondScoreText.text = GameManager.instance.diamondScore.ToString();

		CheckBirds ();
	}

	void CheckBirds() 
	{
		for (int i = 0; i < birdPriceText.Length; i++) 
		{
			birdPriceText [i].SetActive(!GameManager.instance.birds[i + 1]);

			birdIcons [i].SetActive(GameManager.instance.birds[i + 1]);
		}
	}

	public void UnlockAndSelectBird() 
	{
		int selectedBirdIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

		// If the bird is locked
		if (!GameManager.instance.birds[selectedBirdIndex]) 
		{
			switch (selectedBirdIndex) 
			{	
			case 1:

				if (GameManager.instance.diamondScore >= 25) 
				{
					GameManager.instance.birds[selectedBirdIndex] = true;
					GameManager.instance.diamondScore -= 25;
					GameManager.instance.selectedIndex = selectedBirdIndex;
				}
				break;

			case 2:
				if (GameManager.instance.diamondScore >= 50) 
				{
					GameManager.instance.birds[selectedBirdIndex] = true;
					GameManager.instance.diamondScore -= 50;
					GameManager.instance.selectedIndex = selectedBirdIndex;
				}
				break;

			case 3:
				if (GameManager.instance.diamondScore >= 75) 
				{
					GameManager.instance.birds[selectedBirdIndex] = true;
					GameManager.instance.diamondScore -= 75;
					GameManager.instance.selectedIndex = selectedBirdIndex;
				}
				break;

			case 4:
				if (GameManager.instance.diamondScore >= 100) 
				{
					GameManager.instance.birds[selectedBirdIndex] = true;
					GameManager.instance.diamondScore -= 100;
					GameManager.instance.selectedIndex = selectedBirdIndex;
				}
				break;

			case 5:
				if (GameManager.instance.diamondScore >= 100) 
				{
					GameManager.instance.birds[selectedBirdIndex] = true;
					GameManager.instance.diamondScore -= 100;
					GameManager.instance.selectedIndex = selectedBirdIndex;
				}
				break;

			}

		} 
		else 
		{
			GameManager.instance.selectedIndex = selectedBirdIndex;
		}

		CheckBirds();
		birdMenu_DisplayDiamondScoreText.text = GameManager.instance.diamondScore.ToString();
		GameManager.instance.SaveGameData();
	}

	void InstantiatePlayer() 
	{
		GameObject player = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG);
		Vector3 pos = player.transform.position;

		player.SetActive(false);

		Instantiate (birds[GameManager.instance.selectedIndex], pos, Quaternion.identity);

		Camera.main.gameObject.GetComponent<CameraFollow>().FindPlayer();
	}
}