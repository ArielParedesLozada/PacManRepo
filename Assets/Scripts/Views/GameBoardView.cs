﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Zenject;

public class GameBoardView : MonoBehaviour, IGameBoardGateway {

	private static int boardWidth = 28;
	private static int boardHeight = 36;

	private bool didStartDeath = false;
	private bool didStartConsumed = false;

	public static int playerOneLevel = 1;
	public static int playerTwoLevel = 1;

	public int totalPellets = 0;
	public int score = 0;
	public static int playerOneScore = 0;
	public static int playerTwoScore = 0;

	public static int ghostConsumedRunningScore;

	public static bool isPlayerOneUp = true;
	public bool shouldBlink = false;

	public float blinkIntervalTime = 0.1f;
	private float blinkIntervalTimer = 0;

	public AudioClip backgroundAudioNormal;
	public AudioClip backgroundAudioFrightened;
	public AudioClip backgroundAudioPacManDeath;
	public AudioClip consumedGhostAudioClip;

	public Sprite mazeBlue;
	public Sprite mazeWhite;

	public Text playerText;
	public Text readyText;

	public Text highScoreText;
	public Text playerOneUp;
	public Text playerTwoUp;
	public Text playerOneScoreText;
	public Text playerTwoScoreText;
	public Image playerLives2;
	public Image playerLives3;

	public Text consumedGhostScoreText;

	public GameObject[,] board = new GameObject[boardWidth, boardHeight];

	public Image[] levelImages;

	private bool didIncrementLevel = false;

	bool didSpawnBonusItem1_player1;
	bool didSpawnBonusItem2_player1;
	bool didSpawnBonusItem1_player2;
	bool didSpawnBonusItem2_player2;

	[Inject] IGameBoardGateway _boardGateway;

	// Use this for initialization
	void Start () {

		Object[] objects = GameObject.FindObjectsOfType (typeof(GameObject));

		foreach (GameObject o in objects)
		{
			Vector2 pos = o.transform.position;

			if (o.name != "PacMan" && o.name != "Nodes" && o.name != "NonNodes" && o.name != "Maze" && o.name != "Pellets" && o.tag != "Ghost" && o.tag != "ghostHome" && o.name != "Canvas" && o.tag != "UIElements")
			{
				if (o.TryGetComponent<Tile>(out var tile))
				{
					if (tile.isPellet || tile.isSuperPellet)
					{
						totalPellets++;
					}
				}

				int x = Mathf.RoundToInt(pos.x);
				int y = Mathf.RoundToInt(pos.y);

				if (x >= 0 && x < boardWidth && y >= 0 && y < boardHeight)
				{
					board[x, y] = o;
				}
				else if (o.name != "GameInitializer" && o.name != "Installer")
				{
					Debug.LogWarning($"GameObject '{o.name}' fuera del rango del tablero: ({x},{y})");
				}
			}
			else
			{
				Debug.Log("Found PacMan at: " + pos);
			}
		}

		if (isPlayerOneUp) {

			if (playerOneLevel == 1) {

				GetComponent<AudioSource> ().Play ();
			}
		
		} else {

			if (playerTwoLevel == 1) {

				GetComponent<AudioSource> ().Play ();
			}
		}

		Debug.Log("Gateway inyectado: " + _boardGateway);

		StartGame ();
	}

	void Update () {

		UpdateUI ();

		CheckPelletsConsumed ();

		CheckShouldBlink ();

		BonusItems ();
	}

	void BonusItems () {

		if (GameMenu.isOnePlayerGame) {

			SpawnBonusItemForPlayer (1);

		} else {

			if (isPlayerOneUp) {

				SpawnBonusItemForPlayer (1);

			} else {

				SpawnBonusItemForPlayer (2);
			}
		}
	}

	void SpawnBonusItemForPlayer (int playernum) {

		if (playernum == 1) {

			if (GameMenu.playerOnePelletsConsumed >= 70 && GameMenu.playerOnePelletsConsumed < 170) {

				if (!didSpawnBonusItem1_player1) {

					didSpawnBonusItem1_player1 = true;
					SpawnBonusItemForLevel (playerOneLevel);
				}
			
			} else if (GameMenu.playerOnePelletsConsumed >= 170) {

				if (!didSpawnBonusItem2_player1) {

					didSpawnBonusItem2_player1 = true;
					SpawnBonusItemForLevel (playerOneLevel);
				}
			}

		} else {

			if (GameMenu.playerTwoPelletsConsumed >= 70 && GameMenu.playerTwoPelletsConsumed < 170) {

				if (!didSpawnBonusItem1_player2) {

					didSpawnBonusItem1_player2 = true;
					SpawnBonusItemForLevel (playerTwoLevel);
				}

			} else if (GameMenu.playerTwoPelletsConsumed >= 170) {

				if (!didSpawnBonusItem2_player2) {

					didSpawnBonusItem2_player2 = true;
					SpawnBonusItemForLevel (playerTwoLevel);
				}
			}
		}
	}

	void SpawnBonusItemForLevel (int level) {

		GameObject bonusitem = null;

		if (level == 1) {
			bonusitem = Resources.Load ("Prefabs/bonus_cherries", typeof(GameObject)) as GameObject;
		} else if (level == 2) {
			bonusitem = Resources.Load ("Prefabs/bonus_strawberry", typeof(GameObject)) as GameObject;
		} else if (level == 3) {
			bonusitem = Resources.Load ("Prefabs/bonus_peach", typeof(GameObject)) as GameObject;
		} else if (level == 4) {
			bonusitem = Resources.Load ("Prefabs/bonus_peach", typeof(GameObject)) as GameObject;
		} else if (level == 5) {
			bonusitem = Resources.Load ("Prefabs/bonus_apple", typeof(GameObject)) as GameObject;
		} else if (level == 6) {
			bonusitem = Resources.Load ("Prefabs/bonus_apple", typeof(GameObject)) as GameObject;
		} else if (level == 7) {
			bonusitem = Resources.Load ("Prefabs/bonus_grapes", typeof(GameObject)) as GameObject;
		} else if (level == 8) {
			bonusitem = Resources.Load ("Prefabs/bonus_grapes", typeof(GameObject)) as GameObject;
		} else if (level == 9) {
			bonusitem = Resources.Load ("Prefabs/bonus_galaxian", typeof(GameObject)) as GameObject;
		} else if (level == 10) {
			bonusitem = Resources.Load ("Prefabs/bonus_galaxian", typeof(GameObject)) as GameObject;
		} else if (level == 11) {
			bonusitem = Resources.Load ("Prefabs/bonus_bell", typeof(GameObject)) as GameObject;
		} else if (level == 12) {
			bonusitem = Resources.Load ("Prefabs/bonus_bell", typeof(GameObject)) as GameObject;
		} else {
			bonusitem = Resources.Load ("Prefabs/bonus_key", typeof(GameObject)) as GameObject;
		}

		Instantiate (bonusitem);
	}

	void UpdateUI () {

		playerOneScoreText.text = playerOneScore.ToString ();
		playerTwoScoreText.text = playerTwoScore.ToString ();

		int currentLevel;

		if (isPlayerOneUp) {

			currentLevel = playerOneLevel;

			if (GameMenu.livesPlayerOne == 3) {

				playerLives3.enabled = true;
				playerLives2.enabled = true;

			} else if (GameMenu.livesPlayerOne == 2) {

				playerLives3.enabled = false;
				playerLives2.enabled = true;

			} else if (GameMenu.livesPlayerOne == 1) {

				playerLives3.enabled = false;
				playerLives2.enabled = false;
			}

		} else {

			currentLevel = playerTwoLevel;

			if (GameMenu.livesPlayerTwo == 3) {

				playerLives3.enabled = true;
				playerLives2.enabled = true;

			} else if (GameMenu.livesPlayerTwo == 2) {

				playerLives3.enabled = false;
				playerLives2.enabled = true;

			} else if (GameMenu.livesPlayerTwo == 1) {

				playerLives3.enabled = false;
				playerLives2.enabled = false;
			}
		}

		for (int i = 0; i < levelImages.Length; i++) {

			Image li = levelImages [i];
			li.enabled = false;
		}

		for (int i = 1; i < levelImages.Length + 1; i++) {

			if (currentLevel >= i) {

				Image li = levelImages[i-1];
				li.enabled = true;
			}
		}
	}

	void CheckPelletsConsumed () {

		if (isPlayerOneUp) {

			//- Player one is playing
			if (totalPellets == GameMenu.playerOnePelletsConsumed) {

				PlayerWin (1);
			}
	
		} else {

			//- Player two is playing
			if (totalPellets == GameMenu.playerTwoPelletsConsumed) {

				PlayerWin (2);
			}
		}
	}

	void PlayerWin (int playerNum) {

		if (playerNum == 1) {

			if (!didIncrementLevel) {
				didIncrementLevel = true;
				playerOneLevel++;

				StartCoroutine (ProcessWin (2));
			}

		} else {

			if (!didIncrementLevel) {
				didIncrementLevel = true;
				playerTwoLevel++;

				StartCoroutine (ProcessWin (2));
			}
		}
	}

	IEnumerator ProcessWin (float delay) {

		GameObject pacMan = GameObject.Find ("PacMan");
		pacMan.transform.GetComponent<PacManView> ().canMove = false;
		pacMan.transform.GetComponent<Animator> ().enabled = false;

		transform.GetComponent<AudioSource> ().Stop ();

		GameObject[] o = GameObject.FindGameObjectsWithTag ("Ghost");

		foreach (GameObject ghost in o) {

			ghost.transform.GetComponent<GhostView> ().canMove = false;
			ghost.transform.GetComponent<Animator> ().enabled = false;
		}

		yield return new WaitForSeconds (delay);

		StartCoroutine (BlinkBoard (2));
	}

	IEnumerator BlinkBoard (float delay) {

		GameObject pacMan = GameObject.Find ("PacMan");
		pacMan.transform.GetComponent<SpriteRenderer> ().enabled = false;

		GameObject[] o = GameObject.FindGameObjectsWithTag ("Ghost");

		foreach (GameObject ghost in o) {

			ghost.transform.GetComponent<SpriteRenderer> ().enabled = false;
		}

		//- Blink Board
		shouldBlink = true;

		yield return new WaitForSeconds (delay);

		//- Restart the game at the next level
		shouldBlink = false;
		StartNextLevel ();
	}

	private void StartNextLevel () {

		StopAllCoroutines ();

		if (isPlayerOneUp) {

			ResetPelletsForPlayer (1);
			GameMenu.playerOnePelletsConsumed = 0;
			didSpawnBonusItem1_player1 = false;
			didSpawnBonusItem2_player1 = false;

		} else {

			ResetPelletsForPlayer (2);
			GameMenu.playerTwoPelletsConsumed = 0;
			didSpawnBonusItem1_player2 = false;
			didSpawnBonusItem2_player2 = false;
		}

		GameObject.Find ("Maze").transform.GetComponent<SpriteRenderer> ().sprite = mazeBlue;

		didIncrementLevel = false;

		StartCoroutine (ProcessStartNextLevel (1));
	}

	IEnumerator ProcessStartNextLevel (float delay) {

		playerText.transform.GetComponent<Text> ().enabled = true;
		readyText.transform.GetComponent<Text> ().enabled = true;

		if (isPlayerOneUp)
			StartCoroutine (StartBlinking (playerOneUp));
		else
			StartCoroutine (StartBlinking (playerTwoUp));

		RedrawBoard ();

		yield return new WaitForSeconds (delay);

		StartCoroutine (ProcessRestartShowObjects (1));
	}

	private void CheckShouldBlink () {

		if (shouldBlink) {

			if (blinkIntervalTimer < blinkIntervalTime) {

				blinkIntervalTimer += Time.deltaTime;
			
			} else {

				blinkIntervalTimer = 0;

				if (GameObject.Find ("Maze").transform.GetComponent<SpriteRenderer> ().sprite == mazeBlue) {

					GameObject.Find ("Maze").transform.GetComponent<SpriteRenderer> ().sprite = mazeWhite;

				} else {

					GameObject.Find ("Maze").transform.GetComponent<SpriteRenderer> ().sprite = mazeBlue;
				}
			}
		}
	}

	public void StartGame () {

		if (GameMenu.isOnePlayerGame) {

			playerTwoUp.GetComponent<Text> ().enabled = false;
			playerTwoScoreText.GetComponent<Text> ().enabled = false;

		} else {

			playerTwoUp.GetComponent<Text> ().enabled = true;
			playerTwoScoreText.GetComponent<Text> ().enabled = true;
		}

		if (isPlayerOneUp) {

			StartCoroutine (StartBlinking (playerOneUp));

		} else {

			StartCoroutine (StartBlinking (playerTwoUp));
		}

		//- Hide All Ghosts
		GameObject[] o = GameObject.FindGameObjectsWithTag ("Ghost");

		foreach (GameObject ghost in o) {

			ghost.transform.GetComponent<SpriteRenderer> ().enabled = false;
			ghost.transform.GetComponent<GhostView> ().canMove = false;
		}

		GameObject pacMan = GameObject.Find ("PacMan");
		pacMan.transform.GetComponent<SpriteRenderer> ().enabled = false;
		pacMan.transform.GetComponent<PacManView> ().canMove = false;

		StartCoroutine (ShowObjectsAfter (2.25f));
	}

	public void StartConsumed(GhostView consumedGhost)
	{
		if (!didStartConsumed)
		{
			didStartConsumed = true;

			GameObject[] o = GameObject.FindGameObjectsWithTag("Ghost");

			foreach (GameObject ghost in o)
			{
				var ghostView = ghost.GetComponent<GhostView>();
				if (ghostView != null)
					ghostView.canMove = false;
			}

			GameObject pacMan = GameObject.Find("PacMan");
			if (pacMan != null)
			{
				var pacView = pacMan.GetComponent<PacManView>();
				var sprite = pacMan.GetComponent<SpriteRenderer>();

				if (pacView != null) pacView.canMove = false;
				if (sprite != null) sprite.enabled = false;
			}

			Vector2 pos = consumedGhost.transform.position;
			Vector2 viewPortPoint = Camera.main.WorldToViewportPoint(pos);
			if (consumedGhostScoreText != null)
			{
				consumedGhostScoreText.GetComponent<RectTransform>().anchorMin = viewPortPoint;
				consumedGhostScoreText.GetComponent<RectTransform>().anchorMax = viewPortPoint;
				consumedGhostScoreText.text = ghostConsumedRunningScore.ToString();
				consumedGhostScoreText.GetComponent<Text>().enabled = true;
			}
			else
			{
				Debug.LogWarning("ConsumedGhostScoreText no está asignado en el Inspector.");
			}

			GetComponent<AudioSource>().Stop();
			GetComponent<AudioSource>().PlayOneShot(consumedGhostAudioClip);

			StartCoroutine(ProcessConsumedAfter(0.75f, consumedGhost));
		}
	}


	public void StartConsumedBonusItem (GameObject bonusItem, int scorevalue) {

		Vector2 pos = bonusItem.transform.position;
		Vector2 viewPortPoint = Camera.main.WorldToViewportPoint (pos);

		consumedGhostScoreText.GetComponent<RectTransform> ().anchorMin = viewPortPoint;
		consumedGhostScoreText.GetComponent<RectTransform> ().anchorMax = viewPortPoint;

		consumedGhostScoreText.text = scorevalue.ToString ();

		consumedGhostScoreText.GetComponent<Text> ().enabled = true;

		Destroy (bonusItem.gameObject);

		StartCoroutine (ProcessConsumedBonusItem (0.75f));
	}

	IEnumerator ProcessConsumedBonusItem (float delay) {

		yield return new WaitForSeconds (delay);

		consumedGhostScoreText.GetComponent<Text> ().enabled = false;
	}

	IEnumerator StartBlinking (Text blinkText) {

		yield return new WaitForSeconds (0.25f);

		blinkText.GetComponent<Text> ().enabled = !blinkText.GetComponent<Text> ().enabled;
		StartCoroutine (StartBlinking (blinkText));
	}

	IEnumerator ProcessConsumedAfter (float delay, GhostView consumedGhost) {

		yield return new WaitForSeconds (delay);

		//- Hide the score
		consumedGhostScoreText.GetComponent<Text> ().enabled = false;

		//- Show Pac-Man
		GameObject pacMan = GameObject.Find ("PacMan");
		pacMan.transform.GetComponent<SpriteRenderer> ().enabled = true;

		//- Show Consumed Ghost
		consumedGhost.transform.GetComponent<SpriteRenderer> ().enabled = true;

		//- Resume all ghosts
		GameObject[] o = GameObject.FindGameObjectsWithTag ("Ghost");

		foreach (GameObject ghost in o) {

			ghost.transform.GetComponent<GhostView> ().canMove = true;
		}

		//- Resume Pac-Man
		pacMan.transform.GetComponent<PacManView> ().canMove = true;

		//- Start Background Music
		transform.GetComponent<AudioSource> ().Play ();

		didStartConsumed = false;
	}

	IEnumerator ShowObjectsAfter (float delay) {

		yield return new WaitForSeconds (delay);

		GameObject[] o = GameObject.FindGameObjectsWithTag ("Ghost");

		foreach (GameObject ghost in o) {

			ghost.transform.GetComponent<SpriteRenderer> ().enabled = true;
		}

		GameObject pacMan = GameObject.Find ("PacMan");
		pacMan.transform.GetComponent<SpriteRenderer> ().enabled = true;

		playerText.transform.GetComponent<Text> ().enabled = false;

		StartCoroutine (StartGameAfter (2));
	}

	IEnumerator StartGameAfter (float delay) {

		yield return new WaitForSeconds (delay);

		GameObject[] o = GameObject.FindGameObjectsWithTag ("Ghost");

		foreach (GameObject ghost in o) {

			ghost.transform.GetComponent<GhostView> ().canMove = true;
		}

		GameObject pacMan = GameObject.Find ("PacMan");
		pacMan.transform.GetComponent<PacManView> ().canMove = true;

		readyText.transform.GetComponent<Text> ().enabled = false;

		transform.GetComponent<AudioSource> ().clip = backgroundAudioNormal;
		transform.GetComponent<AudioSource> ().Play ();
	}

	public void StartDeath()
	{
		if (!didStartDeath)
		{
			StopAllCoroutines();

			// Mostrar quién está jugando
			if (GameMenu.isOnePlayerGame)
			{
				playerOneUp.GetComponent<Text>().enabled = true;
			}
			else
			{
				playerOneUp.GetComponent<Text>().enabled = true;
				playerTwoUp.GetComponent<Text>().enabled = true;
			}

			// Eliminar ítem de bonus si existe
			GameObject bonusItem = GameObject.Find("bonusItem");
			if (bonusItem)
				Destroy(bonusItem.gameObject);

			didStartDeath = true;

			// Detener movimiento de fantasmas
			GameObject[] ghosts = GameObject.FindGameObjectsWithTag("Ghost");
			foreach (GameObject ghost in ghosts)
			{
				var ghostView = ghost.GetComponent<GhostView>();
				var ghostController = ghost.GetComponent<GhostController>();
				if (ghostView != null)
					ghostView.canMove = false;
				if (ghostController != null && ghostController.GetEntity() != null)
					ghostController.GetEntity().CanMove = false;
			}

			// Detener Pac-Man y reproducir animación de muerte
			GameObject pacMan = GameObject.Find("PacMan");
			if (pacMan != null)
			{
				var pacView = pacMan.GetComponent<PacManView>();
				var pacController = pacMan.GetComponent<PacManController>();
				if (pacView != null)
				{
					pacView.canMove = false;
					pacView.PlayDeath();
				}
				if (pacController != null && pacController.GetEntity() != null)
				{
					pacController.GetEntity().CanMove = false; 
				}
			}

			// Detener música de fondo
			GetComponent<AudioSource>().Stop();

			// Mostrar animación de muerte y continuar con reinicio
			StartCoroutine(ProcessDeathAnimation(1.9f));

		}
	}

	IEnumerator ProcessDeathAnimation (float delay) {

		GameObject pacMan = GameObject.Find ("PacMan");

		pacMan.transform.localScale = new Vector3 (1, 1, 1);
		pacMan.transform.localRotation = Quaternion.Euler (0, 0, 0);

		pacMan.transform.GetComponent<Animator> ().runtimeAnimatorController = pacMan.transform.GetComponent<PacManView> ().deathAnimation;
		pacMan.transform.GetComponent<Animator> ().enabled = true;

		transform.GetComponent<AudioSource> ().clip = backgroundAudioPacManDeath;
		transform.GetComponent<AudioSource> ().Play ();

		yield return new WaitForSeconds (delay);

		StartCoroutine (ProcessRestart (1));
	}


	IEnumerator ProcessRestart (float delay) {

		if (isPlayerOneUp)
			GameMenu.livesPlayerOne -= 1;
		else
			GameMenu.livesPlayerTwo -= 1;

		if (GameMenu.livesPlayerOne == 0 && GameMenu.livesPlayerTwo == 0) {

			playerText.transform.GetComponent<Text> ().enabled = true;

			readyText.transform.GetComponent<Text> ().text = "GAME OVER";
			readyText.transform.GetComponent<Text> ().color = Color.red;

			readyText.transform.GetComponent<Text> ().enabled = true;

			GameObject pacMan = GameObject.Find ("PacMan");
			pacMan.transform.GetComponent<SpriteRenderer> ().enabled = false;

			transform.GetComponent<AudioSource> ().Stop ();

			StartCoroutine (ProcessGameOver (2));

		} else if (GameMenu.livesPlayerOne == 0 || GameMenu.livesPlayerTwo == 0) {

			if (GameMenu.livesPlayerOne == 0) {

				playerText.transform.GetComponent<Text> ().text = "PLAYER 1";

			} else if (GameMenu.livesPlayerTwo == 0) {

				playerText.transform.GetComponent<Text> ().text = "PLAYER 2";
			}

			readyText.transform.GetComponent<Text> ().text = "GAME OVER";
			readyText.transform.GetComponent<Text> ().color = Color.red;

			readyText.transform.GetComponent<Text> ().enabled = true;
			playerText.transform.GetComponent<Text> ().enabled = true;

			GameObject pacMan = GameObject.Find ("PacMan");
			pacMan.transform.GetComponent<SpriteRenderer> ().enabled = false;

			transform.GetComponent<AudioSource> ().Stop ();

			yield return new WaitForSeconds (delay);

			if (!GameMenu.isOnePlayerGame)
				isPlayerOneUp = !isPlayerOneUp;

			if (isPlayerOneUp)
				StartCoroutine (StartBlinking (playerOneUp));
			else
				StartCoroutine (StartBlinking (playerTwoUp));

			RedrawBoard ();

			if (isPlayerOneUp)
				playerText.transform.GetComponent<Text> ().text = "PLAYER 1";
			else
				playerText.transform.GetComponent<Text> ().text = "PLAYER 2";

			readyText.transform.GetComponent<Text> ().text = "READY";
			readyText.transform.GetComponent<Text> ().color = new Color (240f / 255f, 207f / 255f, 101f / 255f);

			yield return new WaitForSeconds (delay);

			StartCoroutine (ProcessRestartShowObjects (2));


		} else {

			playerText.transform.GetComponent<Text> ().enabled = true;
			readyText.transform.GetComponent<Text> ().enabled = true;

			GameObject pacMan = GameObject.Find ("PacMan");
			pacMan.transform.GetComponent<SpriteRenderer> ().enabled = false;

			transform.GetComponent<AudioSource> ().Stop ();

			if (!GameMenu.isOnePlayerGame)
				isPlayerOneUp = !isPlayerOneUp;

			if (isPlayerOneUp)
				StartCoroutine (StartBlinking (playerOneUp));
			else
				StartCoroutine (StartBlinking (playerTwoUp));

			if (!GameMenu.isOnePlayerGame) {

				if (isPlayerOneUp)
					playerText.transform.GetComponent<Text> ().text = "PLAYER 1";
				else
					playerText.transform.GetComponent<Text> ().text = "PLAYER 2";
			}

			RedrawBoard ();

			yield return new WaitForSeconds (delay);

			StartCoroutine (ProcessRestartShowObjects (1));
		}
	}

	IEnumerator ProcessGameOver (float delay) {

		yield return new WaitForSeconds (delay);

		SceneManager.LoadScene ("GameMenu");
	}

	IEnumerator ProcessRestartShowObjects (float delay) {

		playerText.transform.GetComponent<Text> ().enabled = false;

		GameObject[] o = GameObject.FindGameObjectsWithTag ("Ghost");

		foreach (GameObject ghost in o) {

			ghost.transform.GetComponent<SpriteRenderer> ().enabled = true;
			ghost.transform.GetComponent<Animator> ().enabled = true;
			ghost.transform.GetComponent<GhostView> ().MoveToStartingPosition ();
		}

		GameObject pacMan = GameObject.Find ("PacMan");

		pacMan.transform.GetComponent<Animator> ().enabled = false;
		pacMan.transform.GetComponent<SpriteRenderer> ().enabled = true;
		// pacMan.transform.GetComponent<PacManView> ().MoveToStartingPosition ();

		yield return new WaitForSeconds (delay);

		Restart ();
	}

	public void Restart () {

		int playerLevel = 0;

		if (isPlayerOneUp)
			playerLevel = playerOneLevel;
		else
			playerLevel = playerTwoLevel;

		GameObject.Find ("PacMan").GetComponent<PacManView> ().SetDifficultyForLevel (playerLevel);

		GameObject[] obj = GameObject.FindGameObjectsWithTag ("Ghost");

		foreach (GameObject ghost in obj) {

			ghost.transform.GetComponent<GhostView> ().SetDifficultyForLevel (playerLevel);
		}

		readyText.transform.GetComponent<Text> ().enabled = false;



		GameObject pacMan = GameObject.Find ("PacMan");
		// pacMan.transform.GetComponent<PacManView> ().Restart ();

		var pacController = pacMan.GetComponent<PacManController>();
		var pacView = pacMan.GetComponent<PacManView>();
		if (pacController != null && pacController.GetEntity() != null)
		{
			var killPacManUseCase = new KillPacManUseCase();
			StartCoroutine(killPacManUseCase.Execute(pacController.GetEntity(), pacView));
			// pacController.GetEntity().CanMove = true;
		}

		GameObject[] o = GameObject.FindGameObjectsWithTag ("Ghost");

		foreach (GameObject ghost in o)
		{
			var ghostController = ghost.GetComponent<GhostController>();
			var ghostView = ghost.GetComponent<GhostView>();

			// 1. Resetea la lógica (posición y movimiento)
			if (ghostController != null && ghostController.GetEntity() != null)
			{
				ghostController.GetEntity().ResetToStart();
			}

			// 2. Resetea la vista (sprite, animación)
			if (ghostView != null)
			{
				ghostView.MoveToStartingPosition(); // Opcional: si quieres teletransportar visualmente
				ghostView.Restart();
			}
		}

		transform.GetComponent<AudioSource> ().clip = backgroundAudioNormal;
		transform.GetComponent<AudioSource> ().Play ();

		didStartDeath = false;
	}

	void ResetPelletsForPlayer (int playerNum) {

		Object[] objects = GameObject.FindObjectsOfType (typeof(GameObject));

		foreach (GameObject o in objects) {

			if (o.GetComponent<Tile> () != null) {

				if (o.GetComponent<Tile> ().isPellet || o.GetComponent<Tile> ().isSuperPellet) {

					if (playerNum == 1) {

						o.GetComponent<Tile> ().didConsumePlayerOne = false;

					} else {

						o.GetComponent<Tile> ().didConsumePlayerTwo = false;
					}
				}
			}
		}
	}

	void RedrawBoard () {

		Object[] objects = GameObject.FindObjectsOfType (typeof(GameObject));

		foreach (GameObject o in objects) {

			if (o.GetComponent<Tile> () != null) {

				if (o.GetComponent<Tile> ().isPellet || o.GetComponent<Tile> ().isSuperPellet) {

					if (isPlayerOneUp) {

						if (o.GetComponent<Tile> ().didConsumePlayerOne)
							o.GetComponent<SpriteRenderer> ().enabled = false;
						else
							o.GetComponent<SpriteRenderer> ().enabled = true;

					} else {

						if (o.GetComponent<Tile> ().didConsumePlayerTwo)
							o.GetComponent<SpriteRenderer> ().enabled = false;
						else
							o.GetComponent<SpriteRenderer> ().enabled = true;
					}
				}
			}
		}
	}

	#region IGameBoardGateway
	public int Width => boardWidth;

	public int Height => boardHeight;

	public GameObject GetTileAt(int x, int y)
	{
		return board[x, y];
	}

	public void SetTileAt(int x, int y, GameObject obj)
	{
		board[x, y] = obj;
	}

	#endregion
}