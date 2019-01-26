﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TwitterAPI;

public class ScreenshootScript : MonoBehaviour
{
    static string consumerKey = "ni1Pa2Rla9ezK2wFZh3saWWdr";
    static string consumerSecretKey = "Fu6dZlndgvyNhrTVwsmdzhOVbDnWnwYczBqdKrjpWntpS9yzef";

    static string accessKey = "1060652958038327296-8iLdLBkqQPRQeSdZ8hs1gw58ORCL3e";
    static string secretAccessKey = "tKgdegPHsOH5iVZym2LJmubhYGX7GAPsBFjDsKjUzOvuT";

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
        ScreenCapture.CaptureScreenshot(dataPath + "/photo2.png");

        yield return new WaitForSeconds(2f);
        var response = twitter.PublishToTwitter("This is the home of " + name, dataPath + "/photo2.png");

        Debug.Log(response);
    }
}
