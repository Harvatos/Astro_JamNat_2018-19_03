using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TwitterAPI;

public class ScreenshootScript : MonoBehaviour
{
	public bool enableTwitterBot = true;	

    static string consumerKey = "7KrwL7PLvZiOxEDPsssmWnMPE";
    static string consumerSecretKey = "55uVPAI2FWBSUHA9yrmSjvhnoAllX100oRO5LHWG7raXhGmygv";

    static string accessKey = "1089562345565835266-qzbVZmh6wfOAonczBUEJqh9YDIHSxc";
    static string secretAccessKey = "vKslCcrMN0yDtIDZv0B5ykgDgnOvxT17CK5JdT7EMV4yw";

    static string dataPath;
    Twitter twitter;
    public void Start()
    {

        dataPath = Application.persistentDataPath;
        Debug.Log(dataPath);
        ScreenCapture.CaptureScreenshot(dataPath + "/photo2.png");


        twitter = new Twitter(consumerKey, consumerSecretKey, accessKey, secretAccessKey);


    }

    /// <summary>
    /// Dont forget to desactivate previous camera. Might cause problems otherwise.
    /// </summary>
    public void TakeTweet(string name)
    {
        gameObject.SetActive(true);

        StartCoroutine(TakeScreenshot(name));
    }

    IEnumerator TakeScreenshot(string name)
    {
        yield return new WaitForSeconds(2f);
		if(enableTwitterBot)
		{
			 ScreenCapture.CaptureScreenshot(dataPath + "/chez_" + name + ".png");
		}
       

        yield return new WaitForSeconds(2f);
		if(enableTwitterBot)
		{
			var response = twitter.PublishToTwitter("Bienvenue chez " + name, dataPath + "/chez_"+ name +".png");
			Debug.Log(response);
		}
    }
}
