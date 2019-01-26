﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	public static GameController instance;

	public GameObject playerObject;
	public CharacterInteraction playerInteraction;
	public ScreenshootScript twitterCam;

	public float temperature = 50f;
	private float effectiveTemperature;
	public float temperatureLossSpeed = 1f;
	public float temperatureSmoothTime = 1f;
	private float smoothVelRef;

	public float burnHeatBoost = 30f;

	[Range(1, 5)]public int gameDuration = 1;
	public float gameTimer;

	public CanvasGroup fadeScreen;
	public Image fadeScreenImage;
	public Color quitFadeColor = Color.black;
	public Color winFadeColor = Color.white;
	public Color coldFadeColor = Color.white;
	public Color fireFadeColor = Color.red;
	public float fadeSpeed = 0.5f;

    private void Start()
    {
		instance = this;
		effectiveTemperature = temperature;
		gameTimer = gameDuration * 60f;

		fadeScreen.alpha = 1f;
    }

	public void BoostHeat()
	{
		temperature += burnHeatBoost;
	}

	private enum GAMESTATE { Game, Quit, Win, Cold, Burn };
	private GAMESTATE gameState = GAMESTATE.Game;

	private void Update()
    {
		switch (gameState)
		{
			case GAMESTATE.Game: GameUpdate();
				break;
			case GAMESTATE.Quit: QuitUpdate();
				break;
			case GAMESTATE.Win: WinUpdate();
				break;
			case GAMESTATE.Cold: ColdDeathUpdate();
				break;
			case GAMESTATE.Burn: FireDeathUpdate();
				break;
			default:
				break;
		}
	}

	private void GameUpdate()
	{
		fadeScreen.alpha -= fadeSpeed * Time.deltaTime;

		temperature -= temperatureLossSpeed * Time.deltaTime;
		effectiveTemperature = Mathf.SmoothDamp(effectiveTemperature, temperature, ref smoothVelRef, temperatureSmoothTime);
		if (effectiveTemperature <= 0f)
			{ EndGame_ColdDeath(); return; }
		
		if (effectiveTemperature >= 100f)
			{ EndGame_FireDeath(); return; }

		gameTimer = Mathf.Max(0f, gameTimer - Time.deltaTime);
		if (gameTimer < 0.1f)
			{ EndGame_Win(); return; }
		
	}
	private void QuitUpdate()
	{
		fadeScreen.alpha += Time.deltaTime * fadeSpeed;
		if (fadeScreen.alpha <= 0.01f)
		{
			//back to main menu
		}
	}
	private void WinUpdate()
	{
		
	}
	private void ColdDeathUpdate()
	{
		fadeScreen.alpha += Time.deltaTime * fadeSpeed;
		if (fadeScreen.alpha <= 0.01f)
		{
			//back to main menu
		}
	}
	private void FireDeathUpdate()
	{
		fadeScreen.alpha += Time.deltaTime * fadeSpeed;
		if (fadeScreen.alpha <= 0.01f)
		{
			//back to main menu
		}
	}

	public void EndGame_Quit()
	{
		Debug.Log("Quit game triggered");
		gameState = GAMESTATE.Quit;

		fadeScreenImage.color = quitFadeColor;
		//fade to black
		//Load main menu
	}
	public void EndGame_Win()
	{
		Debug.Log("Win triggered");
		gameState = GAMESTATE.Win;

		fadeScreenImage.color = winFadeColor;

		twitterCam.TakeTweet("TestName");
		playerObject.SetActive(false);
	}
	public void EndGame_ColdDeath()
	{
		Debug.Log("Cold death triggered");
		gameState = GAMESTATE.Cold;

		fadeScreenImage.color = coldFadeColor;

		//Fade to white

		//Show cold death message
	}
	public void EndGame_FireDeath()
	{
		Debug.Log("Fire death triggered");
		gameState = GAMESTATE.Burn;

		fadeScreenImage.color = fireFadeColor;

		//fade to red

		//Show fire death message
	}
}
