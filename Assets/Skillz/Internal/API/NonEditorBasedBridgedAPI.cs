using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SkillzSDK.Internal.API
{
	/// <summary>
	/// Thin <see cref="IBridgedAPI"/> wrapper. This checks if
	/// the wrapped bridged API is being initialized when the
	/// game is running from the Unity editor. If so, a dialog
	/// is displayed to tell the developer to switch the platform to
	/// PC, Mac, and Linux in order to test their game's workflow with
	/// Skillz from the Unity editor.
	/// </summary>
	internal sealed class NonEditorBasedBridgedAPI : IBridgedAPI
	{
		public IRandom Random
		{
			get
			{
				return ((IAsyncAPI)actualAPI).Random;
			}
		}

		public bool IsMatchInProgress
		{
			get
			{
				return actualAPI.IsMatchInProgress;
			}
		}

		public float SkillzMusicVolume
		{
			get
			{
				return actualAPI.SkillzMusicVolume;
			}
			set
			{
				actualAPI.SkillzMusicVolume = value;
			}
		}

		public float SoundEffectsVolume
		{
			get
			{
				return actualAPI.SoundEffectsVolume;
			}
			set
			{
				actualAPI.SoundEffectsVolume = value;
			}
		}

		private readonly IBridgedAPI actualAPI;

		public NonEditorBasedBridgedAPI(IBridgedAPI actualAPI)
		{
			this.actualAPI = actualAPI;
		}

		public void AbortMatch()
		{
			actualAPI.AbortMatch();
		}

		public void AbortBotMatch(string botScore)
		{
			actualAPI.AbortBotMatch(botScore);
		}

		public void AbortBotMatch(int botScore)
		{
			actualAPI.AbortBotMatch(botScore);
		}

		public void AbortBotMatch(float botScore)
		{
			actualAPI.AbortBotMatch(botScore);
		}

		public void AddMetadataForMatchInProgress(string metadataJson, bool forMatchInProgress)
		{
			actualAPI.AddMetadataForMatchInProgress(metadataJson, forMatchInProgress);
		}

		public Match GetMatchInfo()
		{
			return actualAPI.GetMatchInfo();
		}

		public Hashtable GetMatchRules()
		{
			return actualAPI.GetMatchRules();
		}

		public Player GetPlayer()
		{
			return actualAPI.GetPlayer();
		}

		public void Initialize(int gameID, Environment environment, Orientation orientation)
		{
#if UNITY_EDITOR
			if (Application.isEditor)
			{
				EditorUtility.DisplayDialog(
					"Skillz Workflow Cannot Be Tested",
					"Skillz cannot be tested from the Unity editor with the currently selected platform.\r\n\r\nTo test Skillz in the Unity editor, please change your platform to \"PC, Mac, and Linux Standalone\".",
					"OK"
				);
			}
#endif

			actualAPI.Initialize(gameID, environment, orientation);
		}

		public void InitializeSimulatedMatch(string matchInfoJson, int randomSeed)
		{
			// Default implementation (can be empty)
			actualAPI.InitializeSimulatedMatch(matchInfoJson, randomSeed);
		}

		public void LaunchSkillz()
		{
			actualAPI.LaunchSkillz();
		}

		public void DisplayTournamentResultsWithScore(string score)
		{
			actualAPI.DisplayTournamentResultsWithScore(score);
		}

		public void DisplayTournamentResultsWithScore(int score)
		{
			actualAPI.DisplayTournamentResultsWithScore(score);
		}

		public void DisplayTournamentResultsWithScore(float score)
		{
			actualAPI.DisplayTournamentResultsWithScore(score);
		}

		public void ReportFinalScoreForBotMatch(string playerScore, string botScore)
		{
			actualAPI.ReportFinalScoreForBotMatch(playerScore, botScore);
		}

		public void ReportFinalScoreForBotMatch(int playerScore, int botScore)
		{
			actualAPI.ReportFinalScoreForBotMatch(playerScore, botScore);
		}

		public void ReportFinalScoreForBotMatch(float playerScore, float botScore)
		{
			actualAPI.ReportFinalScoreForBotMatch(playerScore, botScore);
		}

		public void SubmitScore(string score, Action successCallback, Action<string> failureCallback)
		{
			actualAPI.SubmitScore(score, successCallback, failureCallback);
		}

		public void SubmitScore(int score, Action successCallback, Action<string> failureCallback)
		{
			actualAPI.SubmitScore(score, successCallback, failureCallback);
		}

		public void SubmitScore(float score, Action successCallback, Action<string> failureCallback)
		{
			actualAPI.SubmitScore(score, successCallback, failureCallback);
		}

		public bool EndReplay()
		{
			return actualAPI.EndReplay();
		}

		public bool ReturnToSkillz()
		{
			return actualAPI.ReturnToSkillz();
		}

		public string SDKVersionShort()
		{
			return actualAPI.SDKVersionShort();
		}

		public void SetSkillzBackgroundMusic(string fileName)
		{
			actualAPI.SetSkillzBackgroundMusic(fileName);
		}

		public void UpdatePlayersCurrentScore(string score)
		{
			actualAPI.UpdatePlayersCurrentScore(score);
		}

		public void UpdatePlayersCurrentScore(int score)
		{
			actualAPI.UpdatePlayersCurrentScore(score);
		}

		public void UpdatePlayersCurrentScore(float score)
		{
			actualAPI.UpdatePlayersCurrentScore(score);
		}

		public void GetProgressionUserData(string progressionNamespace, List<string> userDataKeys, Action<Dictionary<string, ProgressionValue>> successCallback, Action<string> failureCallback)
		{
			actualAPI.GetProgressionUserData(progressionNamespace, userDataKeys, successCallback, failureCallback);
		}

		public void UpdateProgressionUserData(string progressionNamespace, Dictionary<string, object> userDataUpdates, Action successCallback, Action<string> failureCallback)
		{
			actualAPI.UpdateProgressionUserData(progressionNamespace, userDataUpdates, successCallback, failureCallback);
		}

		public void GetCurrentSeason(Action<Season> successCallback, Action<string> failureCallback)
		{
			actualAPI.GetCurrentSeason(successCallback, failureCallback);
		}

		public void GetPreviousSeasons(int count, Action<List<Season>> successCallback, Action<string> failureCallback)
		{
			actualAPI.GetPreviousSeasons(count, successCallback, failureCallback);
		}

		public void GetNextSeasons(int count, Action<List<Season>> successCallback, Action<string> failureCallback)
		{
			actualAPI.GetNextSeasons(count, successCallback, failureCallback);
		}
	}
}