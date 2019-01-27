using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
	public static GameController instance;

	public GameObject playerObject;
	public CharacterInteraction playerInteraction;
	public ScreenshootScript twitterCam;

    public TextMeshProUGUI subtitlesDisplay;

	public float temperature = 50f;
	private float effectiveTemperature;
	public float temperatureLossSpeed = 1f;
	public float temperatureSmoothTime = 1f;
	private float smoothVelRef;

	public float burnHeatBoost = 30f;

	public FireStrength fireFx;

	public CanvasGroup iceOverlay;
	public CanvasGroup fireOverlay;

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

		iceOverlay.alpha = Mathf.InverseLerp(40f, 0f, effectiveTemperature);
		fireOverlay.alpha = Mathf.InverseLerp(70f, 100f, effectiveTemperature);
		fireFx.fireStrength = Mathf.InverseLerp(0f, 100f, effectiveTemperature) * 2f;
		//AkSoundEngine.SetRTPCValue("Temperature", effectiveTemperature * 0.01f);

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

		iceOverlay.alpha = 0f;
		fireOverlay.alpha = 0f;

		playerInteraction.objectNameDisplay.text = "";
		fadeScreenImage.color = quitFadeColor;
		//Load main menu
	}
	public void EndGame_Win()
	{
		Debug.Log("Win triggered");
		gameState = GAMESTATE.Win;

		playerInteraction.objectNameDisplay.text = "";
		fadeScreenImage.color = winFadeColor;

		iceOverlay.alpha = 0f;
		fireOverlay.alpha = 0f;

		twitterCam.TakeTweet("TestName");
		playerObject.SetActive(false);
	}
	public void EndGame_ColdDeath()
	{
		Debug.Log("Cold death triggered");
		gameState = GAMESTATE.Cold;

		playerInteraction.objectNameDisplay.text = "";
		fadeScreenImage.color = coldFadeColor;

		//Show cold death message
	}
	public void EndGame_FireDeath()
	{
		Debug.Log("Fire death triggered");
		gameState = GAMESTATE.Burn;

		playerInteraction.objectNameDisplay.text = "";
		fadeScreenImage.color = fireFadeColor;


		//Show fire death message
	}


    public void UpdateSubtitle(string subtitle)
    {
        StartCoroutine(PrintSubtitle(subtitle));
    }

    IEnumerator PrintSubtitle(string subtitle)
    {
        subtitlesDisplay.text = subtitle;
        yield return new WaitForSeconds(2f);
        subtitlesDisplay.text = "";
    }

}
