using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using SkillzSDK.Internal.API;
using SkillzSDK;

// All classes are accessible as long as you are placing them under unity's asset folder.
// No need to import Unity Android and Unity iOS wrappers inside this file

/// <summary>
/// Use this Skillz class if you plan to launch your game in both iOS and Android App stores.
/// </summary>
public static class SkillzCrossPlatform
{
    private static IBridgedAPI BridgedAPI
    {
        get
        {
            if (bridgedAPI == null)
            {
                if (Application.isEditor)
                {
                    bridgedAPI = (IBridgedAPI)new SkillzSDK.Internal.API.UnityEditor.BridgedAPI();
                }
                else
                {
                    bridgedAPI = new NonEditorBasedBridgedAPI(new SkillzSDK.Internal.API.Dummy.BridgedAPI());
#if UNITY_ANDROID
                    bridgedAPI = new NonEditorBasedBridgedAPI(new SkillzSDK.Internal.API.Android.BridgedAPI());
#elif UNITY_IOS
                    bridgedAPI = new NonEditorBasedBridgedAPI(new SkillzSDK.Internal.API.iOS.BridgedAPI());
#elif UNITY_WEBGL
                    bridgedAPI = new NonEditorBasedBridgedAPI(new SkillzSDK.Internal.API.Web.BridgedAPI());
#endif
                }
            }

            return bridgedAPI;
        }
    }

    private static IBridgedAPI bridgedAPI;

    #region Standard API
    /// <summary>
    /// Starts up the Skillz UI. Should be used as soon as the player clicks your game's "Multiplayer" button.
    /// <param name="_matchDelegate"> This should be </param>
    /// </summary>
    [Obsolete("This method has been replaced with LaunchSkillz() the delegate has been replaced by the SkillzManager Component or Prefab")]
    public static void LaunchSkillz(SkillzMatchDelegate _matchDelegate)
    {
        SkillzDebug.Log(SkillzDebug.Type.SKILLZ, "SkillzCrossPlatform.LaunchSkillz() called");
        SkillzState.SetAsyncDelegate(_matchDelegate);

        BridgedAPI.LaunchSkillz();
    }

    /// <summary>
    /// Starts up the Skillz UI. Should be used as soon as the player clicks your game's "Multiplayer" button.
    /// Use this Launch Skillz function with the SkillzLauncher Component/Prefab
    /// </summary>
    public static void LaunchSkillz()
    {
        SkillzDebug.Log(SkillzDebug.Type.SKILLZ, "SkillzCrossPlatform.LaunchSkillz() called");
        SkillzState.SetAsyncDelegate(null);
        if (!SkillzManager.ExistsInProject())
        {
            SkillzDebug.LogError(SkillzDebug.Type.SKILLZ, "LaunchSkillz() called with no SkillzManager in scene");
            //throw exception to be caught when deeplinking
            throw new Exception("LaunchSkillz() called with no SkillzManager in scene");
        }
        BridgedAPI.LaunchSkillz();
    }

    /// <summary>
    /// Gets whether we are currently in a Skillz tournament.
    /// Use this method to have different logic in single player than in multiplayer(Skillz game).
    /// </summary>
    public static bool IsMatchInProgress()
    {
        SkillzDebug.Log(SkillzDebug.Type.SKILLZ, "SkillzCrossPlatform.IsMatchInProgress() called");
        return BridgedAPI.IsMatchInProgress;
    }

    /// <summary>
    /// Returns a Hashtable of the Match Rules that you set in Developer Portal
    /// You can set these rules in https://developers.skillz.com/dashboard and clicking on your game.
    /// </summary>
    public static Hashtable GetMatchRules()
    {
        SkillzDebug.Log(SkillzDebug.Type.SKILLZ, "SkillzCrossPlatform.GetMatchRules() called");
        return BridgedAPI.GetMatchRules();
    }

    /// <summary>
    /// Returns a Match object that has details regarding the specific match the user is in
    /// </summary>
    public static SkillzSDK.Match GetMatchInfo()
    {
        SkillzDebug.Log(SkillzDebug.Type.SKILLZ, "SkillzCrossPlatform.GetMatchInfo() called");
        return BridgedAPI.GetMatchInfo();
    }

    /// <summary>
    /// Call this method to make the player forfeit the game, returning him to the Skillz portal.
    /// </summary>
    public static void AbortMatch()
    {
        SkillzDebug.Log(SkillzDebug.Type.SKILLZ, "SkillzCrossPlatform.AbortMatch() called");
        BridgedAPI.AbortMatch();
    }

    /// <summary>
    /// Call this method to make the player forfeit the game versus a synchronous game bot. 
    /// This will report the bot's score to the Skillz server and return them to the Skillz portal.
    /// This can accept a string, a float, or an int.
    /// </summary>
    ///
    /// <param name="botScore">The bot's current score as a string.</param>
    public static void AbortBotMatch(string botScore)
    {
        SkillzDebug.Log(SkillzDebug.Type.SKILLZ, $"SkillzCrossPlatform.AbortMatch() called, botscore: '{botScore}'");
        BridgedAPI.AbortBotMatch(botScore);
    }

    /// <summary>
    /// Call this method to make the player forfeit the game versus a synchronous game bot. 
    /// This will report the bot's score to the Skillz server and return them to the Skillz portal.
    /// This can accept a string, a float, or an int.
    /// </summary>
    ///
    /// <param name="botScore">The bot's current score as an int.</param>
    public static void AbortBotMatch(int botScore)
    {
        SkillzDebug.Log(SkillzDebug.Type.SKILLZ, $"SkillzCrossPlatform.AbortMatch() called, botscore: '{botScore}'");
        BridgedAPI.AbortBotMatch(botScore);
    }

    /// <summary>
    /// Call this method to make the player forfeit the game versus a synchronous game bot. 
    /// This will report the bot's score to the Skillz server and return them to the Skillz portal.
    /// This can accept a string, a float, or an int.
    /// </summary>
    ///
    /// <param name="botScore">The bot's current score as a float.</param>
    public static void AbortBotMatch(float botScore)
    {
        SkillzDebug.Log(SkillzDebug.Type.SKILLZ, $"SkillzCrossPlatform.AbortMatch() called, botscore: '{botScore}'");
        BridgedAPI.AbortBotMatch(botScore);
    }

    /// <summary>
    /// Call this method every time the player's score changes during a Skillz match.
    /// This adds important anti-cheating functionality to your game.
    /// This can accept a string, a float, or an int.
    /// </summary>
    ///
    /// <param name="score">The player's current score as a string.</param>
    public static void UpdatePlayersCurrentScore(string score)
    {
        SkillzDebug.Log(SkillzDebug.Type.SKILLZ, $"SkillzCrossPlatform.UpdatePlayersCurrentScore() called, score: '{score}'");
        BridgedAPI.UpdatePlayersCurrentScore(score);
    }

    /// <summary>
    /// Call this method every time the player's score changes during a Skillz match.
    /// This adds important anti-cheating functionality to your game.
    /// This can accept a string, a float, or an int.
    /// </summary>
    ///
    /// <param name="score">The player's current score as an int.</param>
    public static void UpdatePlayersCurrentScore(int score)
    {
        SkillzDebug.Log(SkillzDebug.Type.SKILLZ, $"SkillzCrossPlatform.UpdatePlayersCurrentScore() called, score: '{score}'");
        BridgedAPI.UpdatePlayersCurrentScore(score);
    }

    /// <summary>
    /// Call this method every time the player's score changes during a Skillz match.
    /// This adds important anti-cheating functionality to your game.
    /// This can accept a string, a float, or an int.
    /// </summary>
    ///
    /// <param name="score">The player's current score as a float.</param>
    public static void UpdatePlayersCurrentScore(float score)
    {
        SkillzDebug.Log(SkillzDebug.Type.SKILLZ, $"SkillzCrossPlatform.UpdatePlayersCurrentScore() called, score: '{score}'");
        BridgedAPI.UpdatePlayersCurrentScore(score);
    }

    /// <summary>
    /// Call this method when a player finishes a multiplayer game. This will report the result of the game
    /// to the Skillz server, and return the player to the Skillz portal.
    /// This can accept a string, a float, or an int.
    /// </summary>
    ///
    /// <param name="score">A string representing the score a player achieved in the game.</param>
    [Obsolete("This method has been renamed to DisplayTournamentResultsWithScore")]
    public static void ReportFinalScore(string score)
    {
        BridgedAPI.DisplayTournamentResultsWithScore(score);
    }

    /// <summary>
    /// Call this method when a player finishes a multiplayer game. This will report the result of the game
    /// to the Skillz server, and return the player to the Skillz portal.
    /// This can accept a string, a float, or an int.
    /// </summary>
    ///
    /// <param name="score">An int representing the score a player achieved in the game.</param>
    [Obsolete("This method has been renamed to DisplayTournamentResultsWithScore")]
    public static void ReportFinalScore(int score)
    {
        BridgedAPI.DisplayTournamentResultsWithScore(score);
    }

    /// <summary>
    /// Call this method when a player finishes a multiplayer game. This will report the result of the game
    /// to the Skillz server, and return the player to the Skillz portal.
    /// This can accept a string, a float, or an int.
    /// </summary>
    ///
    /// <param name="score">A float representing the score a player achieved in the game.</param>
    [Obsolete("This method has been renamed to DisplayTournamentResultsWithScore")]
    public static void ReportFinalScore(float score)
    {
        BridgedAPI.DisplayTournamentResultsWithScore(score);
    }

    /// <summary>
    /// Call this method when a player finishes a multiplayer game. This will report the result of the game
    /// to the Skillz server, and return the player to the Skillz portal.
    /// This can accept a string, a float, or an int.
    /// </summary>
    ///
    /// <param name="score">A string representing the score a player achieved in the game.</param>
    public static void DisplayTournamentResultsWithScore(string score)
    {
        SkillzDebug.Log(SkillzDebug.Type.SKILLZ, $"SkillzCrossPlatform.DisplayTournamentResultsWithScore() called, score: '{score}'");
        BridgedAPI.DisplayTournamentResultsWithScore(score);
    }

    /// <summary>
    /// Call this method when a player finishes a multiplayer game. This will report the result of the game
    /// to the Skillz server, and return the player to the Skillz portal.
    /// This can accept a string, a float, or an int.
    /// </summary>
    ///
    /// <param name="score">An int representing the score a player achieved in the game.</param>
    public static void DisplayTournamentResultsWithScore(int score)
    {
        SkillzDebug.Log(SkillzDebug.Type.SKILLZ, $"SkillzCrossPlatform.DisplayTournamentResultsWithScore() called, score: '{score}'");
        BridgedAPI.DisplayTournamentResultsWithScore(score);
    }

    /// <summary>
    /// Call this method when a player finishes a multiplayer game. This will report the result of the game
    /// to the Skillz server, and return the player to the Skillz portal.
    /// This can accept a string, a float, or an int.
    /// </summary>
    ///
    /// <param name="score">A float representing the score a player achieved in the game.</param>
    public static void DisplayTournamentResultsWithScore(float score)
    {
        SkillzDebug.Log(SkillzDebug.Type.SKILLZ, $"SkillzCrossPlatform.DisplayTournamentResultsWithScore() called, score: '{score}'");
        BridgedAPI.DisplayTournamentResultsWithScore(score);
    }

    /// <summary>
    /// Call this method when a player finishes a game against a synchronous game bot.
    /// This will report the player's score and the bot's score to the Skillz server and return 
    /// them to the Skillz portal.
    /// This can accept a pair of strings, a pair of floats, or a pair of ints.
    /// </summary>
    ///
    /// <param name="playerScore">The bot's current score as a string.</param>
    /// <param name="botScore">The bot's current score as a string.</param>
    public static void ReportFinalScoreForBotMatch(string playerScore, string botScore)
    {
        SkillzDebug.Log(SkillzDebug.Type.SKILLZ, $"SkillzCrossPlatform.ReportFinalScoreForBotMatch() called, player score: '{playerScore}', bot score: '{botScore}'");
        BridgedAPI.ReportFinalScoreForBotMatch(playerScore, botScore);
    }

    /// <summary>
    /// Call this method when a player finishes a game against a synchronous game bot.
    /// This will report the player's score and the bot's score to the Skillz server and return 
    /// them to the Skillz portal.
    /// This can accept a pair of strings, a pair of floats, or a pair of ints.
    /// </summary>
    ///
    /// <param name="playerScore">The bot's current score as an int.</param>
    /// <param name="botScore">The bot's current score as an int.</param>
    public static void ReportFinalScoreForBotMatch(int playerScore, int botScore)
    {
        SkillzDebug.Log(SkillzDebug.Type.SKILLZ, $"SkillzCrossPlatform.ReportFinalScoreForBotMatch() called, player score: '{playerScore}', bot score: '{botScore}'");
        BridgedAPI.ReportFinalScoreForBotMatch(playerScore, botScore);
    }

    /// <summary>
    /// Call this method when a player finishes a game against a synchronous game bot.
    /// This will report the player's score and the bot's score to the Skillz server and return 
    /// them to the Skillz portal.
    /// This can accept a pair of strings, a pair of floats, or a pair of ints.
    /// </summary>
    ///
    /// <param name="playerScore">The bot's current score as a float.</param>
    /// <param name="botScore">The bot's current score as a float.</param>
    public static void ReportFinalScoreForBotMatch(float playerScore, float botScore)
    {
        SkillzDebug.Log(SkillzDebug.Type.SKILLZ, $"SkillzCrossPlatform.ReportFinalScoreForBotMatch() called, player score: '{playerScore}', bot score: '{botScore}'");
        BridgedAPI.ReportFinalScoreForBotMatch(playerScore, botScore);
    }

    /// <summary>
    /// Call this method when a player's score is finalized to report their score to the Skillz server.
    /// This methods does not return control to the Skillz SDK.
    /// </summary>
    ///
    /// <param name="score">The player's score as a string.</param>
    /// <param name="successCallback">A callback function that is invoked when the score submit completes successfully</param>
    /// <param name="failureCallback">A callback function that is invoked when the score submit fails. It is invoked with an error message parameter.</param>
    public static void SubmitScore(string score, Action successCallback, Action<string> failureCallback)
    {
        SkillzDebug.Log(SkillzDebug.Type.SKILLZ, $"SkillzCrossPlatform.SubmitScore() called, score: '{score}'");
        BridgedAPI.SubmitScore(score, successCallback, failureCallback);
    }

    /// <summary>
    /// Call this method when a player's score is finalized to report their score to the Skillz server.
    /// This methods does not return control to the Skillz SDK.
    /// </summary>
    ///
    /// <param name="score">The player's score as an integer.</param>
    /// <param name="successCallback">A callback function that is invoked when the score submit completes successfully</param>
    /// <param name="failureCallback">A callback function that is invoked when the score submit fails. It is invoked with an error message parameter.</param>
    public static void SubmitScore(int score, Action successCallback, Action<string> failureCallback)
    {
        SkillzDebug.Log(SkillzDebug.Type.SKILLZ, $"SkillzCrossPlatform.SubmitScore() called, score: '{score}'");
        BridgedAPI.SubmitScore(score, successCallback, failureCallback);
    }

    /// <summary>
    /// Call this method when a player's score is finalized to report their score to the Skillz server.
    /// This methods does not return control to the Skillz SDK.
    /// </summary>
    ///
    /// <param name="score">The player's score as a float.</param>
    /// <param name="successCallback">A callback function that is invoked when the score submit completes successfully</param>
    /// <param name="failureCallback">A callback function that is invoked when the score submit fails. It is invoked with an error message parameter.</param>
    public static void SubmitScore(float score, Action successCallback, Action<string> failureCallback)
    {
        SkillzDebug.Log(SkillzDebug.Type.SKILLZ, $"SkillzCrossPlatform.SubmitScore() called, score: '{score}'");
        BridgedAPI.SubmitScore(score, successCallback, failureCallback);
    }

    /// <summary>
    /// Call this function to end replay capturing after submitting the score to Skillz.
    ///
    /// This should be used in cases when your game displays a progression screen after the match
    /// that is not directly relevant to the player's match. In that case, this method should be
    /// called after displaying the score results to the player (if your game does this) but before
    /// displaying the progression screen.
    ///
    /// This function cannot be called before you call one of the submit score or abort game methods,
    /// and will return false if you try.
    ///
    /// Replays will also be ended automatically when returning to Skillz, so if your game doesn't
    /// display a progression screen, you can safely ignore calling this method.
    ///
    /// This method returns true if the replay capture was successfully ended or if no replay was being recorded
    /// </summary>
    public static bool EndReplay()
    {
        SkillzDebug.Log(SkillzDebug.Type.SKILLZ, "SkillzCrossPlatform.EndReplay() called");
        return BridgedAPI.EndReplay();
    }

    /// <summary>
    /// This method returns a boolean value indicating if the user is able to be returned to the Skillz portal, 
    /// and then returns the player to the Skillz portal if possible.
    /// A score must be submitted if this is called while inside of a match.
    /// </summary> 
    public static bool ReturnToSkillz()
    {
        SkillzDebug.Log(SkillzDebug.Type.SKILLZ, "SkillzCrossPlatform.ReturnToSkillz() called");
        return BridgedAPI.ReturnToSkillz();
    }

    /// <summary>
    /// This method returns what SDK version your user is on.
    /// </summary>
    public static string SDKVersionShort()
    {
        SkillzDebug.Log(SkillzDebug.Type.SKILLZ, "SkillzCrossPlatform.SDKVersionShort() called");
        return BridgedAPI.SDKVersionShort();
    }

    //	/// <summary>
    //	/// This returns the current user's display name in case you want to use it in the game
    //	/// </summary>
    //	[Obsolete("This method will be removed in a future release, instead use the get Player function, which will return an instance of Player for the current user")]
    //	public static string CurrentUserDisplayName()
    //	{
    //#if UNITY_ANDROID
    //			return Skillz.CurrentUserDisplayName();
    //#elif UNITY_IOS
    //			return SkillzSDK.Api.GetPlayer().DisplayName;
    //#endif
    //		return null;
    //	}

    /// <summary>
    /// Use this Player class to grab information about your current user.
    /// </summary>
    public static SkillzSDK.Player GetPlayer()
    {
        SkillzDebug.Log(SkillzDebug.Type.SKILLZ, "SkillzCrossPlatform.GetPlayer() called");
        SkillzSDK.Match match = null;
        if (IsMatchInProgress())
        {
            match = GetMatchInfo();
        }

        if (match != null)
        {
            foreach (SkillzSDK.Player player in match.Players)
            {
                if (player.IsCurrentPlayer)
                {
                    SkillzDebug.Log(SkillzDebug.Type.SKILLZ, $"Player: '{player.ToString()}'");
                    return player;
                }
            }
        }

        return BridgedAPI.GetPlayer();
    }


    /// <summary>
    /// Call this method if you want to add meta data for the match.
    /// </summary>
    /// <param name="metadataJson">A string representing the meta data in a json string.</param>
    /// <param name="forMatchInProgress">A boolean to check whether the user is in a Skillz game.</param>
    public static void AddMetadataForMatchInProgress(string metadataJson, bool forMatchInProgress)
    {
        SkillzDebug.Log(SkillzDebug.Type.SKILLZ, "SkillzCrossPlatform.AddMetadataForMatchInProgres() called");
        BridgedAPI.AddMetadataForMatchInProgress(metadataJson, forMatchInProgress);
    }

    #endregion // Standard API

    #region Audio API

    /// <summary>
    /// Call this method to set background music to be played inside our Skillz Lobby.
    /// This will be continuously playing throughout a user's time in our SDK.
    /// </summary>
    ///
    /// <param name="fileName">The name of the music file inside of the StreamingAssets folder, e.g. "game_music.mp3" .</param>
    public static void setSkillzBackgroundMusic(string fileName)
    {
        SkillzDebug.Log(SkillzDebug.Type.SKILLZ, "SkillzCrossPlatform.setSkillzBackgroundMusic() called, fileName: " + fileName);
        BridgedAPI.SetSkillzBackgroundMusic(fileName);
    }

    /// <summary>
    /// Call this method to get the background music
    /// volume the user set or the default one
    /// to calibrate on your preferred volume sliders.
    /// </summary>
    public static float getSkillzMusicVolume()
    {
        SkillzDebug.Log(SkillzDebug.Type.SKILLZ, "SkillzCrossPlatform.getSkillzMusicVolume() called");
        return BridgedAPI.SkillzMusicVolume;
    }

    /// <summary>
    /// Call this method to set the background music volume the user sets on
    /// your volume control. This value will be saved to be used as the volume
    /// inside the Skillz framework for our volume sliders as well.
    /// </summary>
    ///
    /// <param name="volume">The volume of the background music.</param>
    public static void setSkillzMusicVolume(float volume)
    {
        SkillzDebug.Log(SkillzDebug.Type.SKILLZ, $"SkillzCrossPlatform.setSkillzMusicVolume() called, volume: {volume} ");
        BridgedAPI.SkillzMusicVolume = volume;
    }

    /// <summary>
    /// Call this method to get the SFX volume the user set or the default one
    /// to calibrate on your preferred volume sliders.
    /// </summary>
    public static float getSFXVolume()
    {
        SkillzDebug.Log(SkillzDebug.Type.SKILLZ, "SkillzCrossPlatform.getSFXVolume() called");
        return BridgedAPI.SoundEffectsVolume;
    }

    /// <summary>
    /// Call this method to set the SFX volume the user sets on
    /// your volume control. This value will be saved to be used as the volume
    /// inside the Skillz framework for our volume sliders as well.
    /// </summary>
    ///
    /// <param name="volume">The volume of the SFX sound.</param>
    public static void setSFXVolume(float volume)
    {
        SkillzDebug.Log(SkillzDebug.Type.SKILLZ, $"SkillzCrossPlatform.setSFXVolume() called, volume: {volume} ");
        BridgedAPI.SoundEffectsVolume = volume;
    }

    #endregion // Audio API

    #region Progression API

    /// <summary>
    /// Call this method to request progression data for the currently logged in user.
    /// This will invoke one of the given callbacks when the attmpted request is completed.
    /// </summary>
    ///
    /// <param name="progressionNamespace">The namespace to fetch the progression data from.</param>
    /// <param name="userDataKeys">The list of user progression data elements to fetch.</param>
    /// <param name="successCallback">The callback that will be invoked with the successfullly retrieved user data.</param>
    /// <param name="failureCallback">The callback that will be invoked with an error message if the request cannot be completed.</param>
    public static void GetProgressionUserData(string progressionNamespace, List<string> userDataKeys, Action<Dictionary<string, ProgressionValue>> successCallback, Action<string> failureCallback)
    {
        SkillzDebug.Log(SkillzDebug.Type.SKILLZ, $"SkillzCrossPlatform.GetProgressionUserData() called, progressionNamespace: '{progressionNamespace}' userDataKeys: '{SkillzDebug.Format(userDataKeys)}'");
        bridgedAPI.GetProgressionUserData(progressionNamespace, userDataKeys, successCallback, failureCallback);
    }

    /// <summary>
    /// Call this method to update progression data for the currently logged in user.
    /// This must be called with no more than 25 elements and will invoke one of the given callbacks
    /// when the attempted update is completed.
    /// </summary>
    ///
    /// <param name="progressionNamespace">The namespace to fetch the progression data from.</param>
    /// <param name="userDataUpdates">The dictionary of keys and values to set as the user data.</param>
    /// <param name="successCallback">The callback that will be invoked when the update is completed successfully.</param>
    /// <param name="failureCallback">The callback that will be invoked with an error message if the update cannot be completed.</param>
    public static void UpdateProgressionUserData(string progressionNamespace, Dictionary<string, object> userDataUpdates, Action successCallback, Action<string> failureCallback)
    {
        SkillzDebug.Log(SkillzDebug.Type.SKILLZ, $"SkillzCrossPlatform.UpdateProgressionUserData() called, progressionNamespace: '{progressionNamespace}' userDataUpdates: '{SkillzDebug.Format(userDataUpdates)}'");
        bridgedAPI.UpdateProgressionUserData(progressionNamespace, userDataUpdates, successCallback, failureCallback);
    }

    /// <summary>
    ///	Call this method to fetch the current active season for progression.
    /// The given callback will be invoked with the fetched season. If no season is active,
    /// the callback will be invoked with null.
    /// </summary>
    ///
    /// <param name="successCallback">A callback to be invoked with the current season. If no season is active, it will be invoked with null.</param>
    /// <param name="failureCallback">A callback to be invoked with an error message if the attempt to fetch the current season fails.</param>
    public static void GetCurrentSeason(Action<Season> successCallback, Action<string> failureCallback)
    {
        SkillzDebug.Log(SkillzDebug.Type.SKILLZ, "SkillzCrossPlatform.GetCurrentSeason() called");
        bridgedAPI.GetCurrentSeason(successCallback, failureCallback);
    }

    /// <summary>
    /// Call this method to fetch the most recent previously completed seasons in chronological order.
    /// The given callback will always be invoked with a List of previous seasons, up to the given count.
    /// If no seasons have completed it will be invoked with an empty list.
    /// </summary>
    ///
    /// <param name="count">The number of previously ended seasons to fetch. This must be at least 1.</param>
    /// <param name="successCallback">A callback to be invoked with the most recent completed seasons, up to the given count.</param>
    /// <param name="failureCallback">A callback to be invoked with an error message if the attempt to fetch the current season fails.</param>
    public static void GetPreviousSeasons(int count, Action<List<Season>> successCallback, Action<string> failureCallback)
    {
        SkillzDebug.Log(SkillzDebug.Type.SKILLZ, $"SkillzCrossPlatform.GetPreviousSeason() called, getting previous {count} seasons");
        bridgedAPI.GetPreviousSeasons(count, successCallback, failureCallback);
    }

    /// <summary>
    /// Call this method to fetch upcoming seasons in chronological order.
    /// The given callback will always be invoked with a List of the next unstarted seasons, up to the given count.
    /// If no seasons are upcoming it will be invoked with an empty list.
    /// </summary>
    ///
    /// <param name="count">The number of upcoming seasons to fetch. This must be at least 1.</param>
    /// <param name="successCallback">A callback to be invoked with the next upcoming seasons, up to the given count.</param>
    /// <param name="failureCallback">A callback to be invoked with an error message if the attempt to fetch the current season fails.</param>
    public static void GetNextSeasons(int count, Action<List<Season>> successCallback, Action<string> failureCallback)
    {
        SkillzDebug.Log(SkillzDebug.Type.SKILLZ, $"SkillzCrossPlatform.GetNextSeason() called, getting next {count} seasons");
        bridgedAPI.GetNextSeasons(count, successCallback, failureCallback);
    }

    #endregion // Progression API

    internal static void Initialize(int gameID, SkillzSDK.Environment environment, SkillzSDK.Orientation orientation)
    {
        BridgedAPI.Initialize(gameID, environment, orientation);
    }

    internal static void InitializeSimulatedMatch(string matchInfoJson, int randomSeed)
    {
#if UNITY_WEBGL
        if (!Debug.isDebugBuild)
        {
            SkillzDebug.LogWarning(SkillzDebug.Type.SKILLZ, $"Called SkillzCrossPlatform.InitializeSimulatedMatch() from a non-Debug web build.");
        }
#else
        if (!Application.isEditor)
        {
            SkillzDebug.LogWarning(SkillzDebug.Type.SKILLZ, $"Called SkillzCrossPlatform.InitializeSimulatedMatch() from other than the Unity editor!");
            return;
        }
#endif

        IBridgedAPI bridgedApi = BridgedAPI;

        if (bridgedApi == null)
        {
            SkillzDebug.LogWarning(SkillzDebug.Type.SKILLZ,
                $"Expected a BridgedAPI instance for initializing a simulated match!");
            return;
        }

        bridgedApi.InitializeSimulatedMatch(matchInfoJson, randomSeed);
    }

    /// <summary>
    /// This is the Random class that you can use to implement fairness in your game
    /// Use this Random function for variables that can affect gameplay.
    /// </summary>
    public static class Random
    {

        /**
        * Value from Skillz random (if a Skillz game), or Unity random (if not a Skillz game)
        **/
        public static float Value()
        {

            if (IsMatchInProgress())
            {
                return ((IAsyncAPI)BridgedAPI).Random.Value();
            }

            return UnityEngine.Random.value;
        }

        /**
        * Find a point inside the unit sphere using Value()
        **/
        public static Vector3 InsideUnitSphere()
        {
            float r = Value();
            float phi = Value() * Mathf.PI;
            float theta = Value() * Mathf.PI * 2;

            float x = r * Mathf.Cos(theta) * Mathf.Sin(phi);
            float y = r * Mathf.Sin(theta) * Mathf.Sin(phi);
            float z = r * Mathf.Cos(phi);

            return new Vector3(x, y, z);
        }

        /**
        * Find a point inside the unit circle using Value()
        **/
        public static Vector2 InsideUnitCircle()
        {
            float radius = 1.0f;
            float rand = Value() * 2 * Mathf.PI;
            Vector2 val = new Vector2();

            val.x = radius * Mathf.Cos(rand);
            val.y = radius * Mathf.Sin(rand);

            return val;
        }

        /**
        * Hybrid rejection / trig method to generate points on a sphere using Value()
        **/
        public static Vector3 OnUnitSphere()
        {
            Vector3 val = new Vector3();
            float s;

            do
            {
                val.x = 2 * (float)Value() - 1;
                val.y = 2 * (float)Value() - 1;
                s = Mathf.Pow(val.x, 2) + Mathf.Pow(val.y, 2);
            }
            while (s > 1);

            float r = 2 * Mathf.Sqrt(1 - s);

            val.x *= r;
            val.y *= r;
            val.z = 2 * s - 1;

            return val;
        }

        /**
        * Quaternion random using Value()
        **/
        public static Quaternion RotationUniform()
        {
            float u1 = Value();
            float u2 = Value();
            float u3 = Value();

            float u1sqrt = Mathf.Sqrt(u1);
            float u1m1sqrt = Mathf.Sqrt(1 - u1);
            float x = u1m1sqrt * Mathf.Sin(2 * Mathf.PI * u2);
            float y = u1m1sqrt * Mathf.Cos(2 * Mathf.PI * u2);
            float z = u1sqrt * Mathf.Sin(2 * Mathf.PI * u3);
            float w = u1sqrt * Mathf.Cos(2 * Mathf.PI * u3);

            return new Quaternion(x, y, z, w);
        }

        /**
        * Quaternion random using Value()
        **/
        public static Quaternion Rotation()
        {
            return RotationUniform();
        }

        /**
        * Ranged random float using Value()
        **/
        public static float Range(float min, float max)
        {
            float rand = Value();
            return min + (rand * (max - min));
        }

        /**
        * Ranged random int using Value()
        **/
        public static int Range(int min, int max)
        {
            float rand = Value();
            return min + (int)(rand * (max - min));
        }
    }

    private static AndroidJavaClass GetSkillz()
    {
        return new AndroidJavaClass("com.skillz.Skillz");
    }
}