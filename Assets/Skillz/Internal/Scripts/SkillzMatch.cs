using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using SkillzSDK.Settings;
using SkillzSDK.Extensions;

using JSONDict = System.Collections.Generic.Dictionary<string, object>;
using System.Linq;
using SkillzSDK.Internal.Encryption;

namespace SkillzSDK
{
    /// <summary>
    /// A Skillz user.
    /// </summary>
    public class Player : IPlayer
    {
        /// <summary>
        /// The user's display name.
        /// </summary>
        public readonly string DisplayName;

        /// <summary>
        /// An ID unique to this user.
        /// </summary>
        public readonly UInt64? ID;

        /// <summary>
        /// A Tournament Player ID unique to this user.
        /// </summary>
        public UInt64? TournamentPlayerID { get; }

        /// <summary>
        /// A link to the user's avatar image.
        /// </summary>
        public readonly string AvatarURL;

        /// <summary>
        /// A link to the user's country's flag image.
        /// </summary>
        public readonly string FlagURL;

        /// <summary>
        /// This Player represents the current user if this is true.
        /// </summary>
        public bool IsCurrentPlayer { get; }

        /// <summary>
        /// This Player was already an New Paying User
        /// </summary>
        public readonly bool IsNewPayingUser;


        public Player(JSONDict playerJSON)
        {
            if (Application.isEditor)
            {
                ID = playerJSON.SafeGetUintValue("id");
                DisplayName = playerJSON.SafeGetStringValue("displayName");
                AvatarURL = playerJSON.SafeGetStringValue("avatarURL");
                FlagURL = playerJSON.SafeGetStringValue("flagURL");
                IsCurrentPlayer = (bool)playerJSON.SafeGetBoolValueDefaultFalse("isCurrentPlayer");
                TournamentPlayerID = playerJSON.SafeGetUintValue("tournamentPlayerId");
                IsNewPayingUser = (bool)playerJSON.SafeGetBoolValueDefaultFalse("isNewPayingUser");
            }
            else
            {
#if UNITY_IOS || UNITY_WEBGL
                ID = playerJSON.SafeGetUintValue("id");
                DisplayName = playerJSON.SafeGetStringValue("displayName");
                AvatarURL = playerJSON.SafeGetStringValue("avatarURL");
                FlagURL = playerJSON.SafeGetStringValue("flagURL");
                IsCurrentPlayer = (bool)playerJSON.SafeGetBoolValue("isCurrentPlayer");
                TournamentPlayerID = playerJSON.SafeGetUlongValue("playerMatchId");
                IsNewPayingUser = (bool)playerJSON.SafeGetBoolValueDefaultFalse("isNewPayingUser");
#elif UNITY_ANDROID
                ID = playerJSON.SafeGetUintValue("userId");
                DisplayName = playerJSON.SafeGetStringValue("userName");
                AvatarURL = playerJSON.SafeGetStringValue("avatarUrl");
                FlagURL = playerJSON.SafeGetStringValue("flagUrl");
                IsCurrentPlayer = (bool)playerJSON.SafeGetBoolValue("isCurrentPlayer");
                TournamentPlayerID = playerJSON.SafeGetUintValue("playerMatchId");
                IsNewPayingUser = (bool)playerJSON.SafeGetBoolValueDefaultFalse("isNewPayingUser");
#endif
            }
        }

        public override string ToString()
        {
            return "Player: " +
            " ID: [" + ID + "]" +
            " DisplayName: [" + DisplayName + "]" +
            " AvatarURL: [" + AvatarURL + "]" +
            " FlagURL: [" + FlagURL + "]" +
            " IsNewPayingUser: [" + IsNewPayingUser + "]";
        }
    }


    /// <summary>
    /// A Skillz match.
    /// </summary>
    public class Match : IMatch
    {
        /// <summary>
        /// Gets a value indicating whether this <see cref="T:SkillzSDK.Match"/> represents a custom synchronous match.
        /// </summary>
        /// <value>
        /// <c>true</c> if this is a custom synchronous match; otherwise, <c>false</c>.
        /// </value>
        public bool IsCustomSynchronousMatch
        {
            get
            {
                return CustomServerConnectionInfo != null;
            }
        }

        /// <summary>
        /// The name of this tournament type.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// The description of this tournament type.
        /// </summary>
        public readonly string Description;

        /// <summary>
        /// The unique ID for this match.
        /// </summary>
        public ulong? ID { get; } 

        /// <summary>
        /// The unique ID for the tournament template this match is based on.
        /// </summary>
        public ulong? TemplateID { get; }

        /// <summary>
        /// If this game supports "Automatic Difficulty" (specified in the Developer Portal --
        /// https://www.developers.skillz.com/developer), this value represents the difficulty this game
        /// should have, from 1 to 10 (inclusive).
        /// Note that this value will only exist in Production, not Sandbox.
        /// </summary>
        public readonly uint? SkillzDifficulty;

        /// <summary>
        /// Is this match being played for real cash or for Z?
        /// </summary>
        public readonly bool? IsCash;

        /// <summary>
        /// If this tournament is being played for Z,
        /// this is the amount of Z required to enter.
        /// </summary>
        public readonly int? EntryPoints;
        /// <summary>
        /// If this tournament is being played for real cash,
        /// this is the amount of cash required to enter.
        /// </summary>
        public readonly float? EntryCash;

        /// <summary>
        /// If this tournament is Synchronous or Asynchronous?
        /// </summary>
        public readonly bool IsSynchronous;

        /// <summary>
        /// The user playing this match.
        /// </summary>
        public List<IPlayer> Players { get; }

        /// <summary>
        /// The connection info to a custom server that coordinates a real-time match.
        /// This will be <c>null</c> if the match is either asynchronous,
        /// or synchronous and coordinated by Skillz.
        /// </summary>
        public readonly CustomServerConnectionInfo CustomServerConnectionInfo;

        /// <summary>
        /// The custom parameters for this tournament type.
        /// Specified by the developer on the Skillz Developer Portal.
        /// </summary>
        public readonly Dictionary<string, string> GameParams;

        /// <summary>
        /// Defines the <see cref="T:SkillzSDK.Match"/>  as a tie-breaker.
        /// <summary>
        public readonly bool IsTieBreaker;

        /// <summary>
        /// Defines the <see cref="T:SkillzSDK.Match"/> as a bracket event match.
        /// <summary>
        public readonly bool IsBracket;

        /// <summary>
        /// The bracket round when the <see cref="T:SkillzSDK.Match"/> is a bracket <see cref="IsBracket"/>..
        /// <summary>
        public readonly int? BracketRound;

        /// <summary>
        /// Defines the <see cref="T:SkillzSDK.Match"/> as a video add entry event match.
        /// Entry fees should be ignore.
        /// <summary>
        public readonly bool? IsVideoAdEntry;

        public Match(JSONDict jsonData)
        {
            Description = jsonData.SafeGetStringValue("matchDescription");
            EntryCash = (float)jsonData.SafeGetDoubleValue("entryCash");
            EntryPoints = jsonData.SafeGetIntValue("entryPoints");
            ID = jsonData.SafeGetUlongValue("id");
            TemplateID = jsonData.SafeGetUlongValue("templateId");
            Name = jsonData.SafeGetStringValue("name");
            IsCash = jsonData.SafeGetBoolValue("isCash");
            IsSynchronous = (bool)jsonData.SafeGetBoolValueDefaultFalse("isSynchronous");
            IsTieBreaker = (bool)jsonData.SafeGetBoolValueDefaultFalse("isTieBreaker");
            IsBracket = (bool)jsonData.SafeGetBoolValueDefaultFalse("isBracket");
            BracketRound = jsonData.SafeGetIntValue("bracketRound");
            IsVideoAdEntry = jsonData.SafeGetBoolValue("isVideoAdEntry");

            object players = jsonData.SafeGetValue("players");
            Players = new List<IPlayer>();

            List<object> playerArray = (List<object>)players;
            foreach (object player in playerArray)
            {
                Players.Add(new Player((Dictionary<string, object>)player));
            }

            var connectionInfoJson = jsonData.SafeGetValue("connectionInfo") as JSONDict;

            // Check if connectionInfoJson is null or has null values.
            if (connectionInfoJson == null ||
                string.IsNullOrEmpty(connectionInfoJson.SafeGetValue("matchId") as string) ||
                string.IsNullOrEmpty(connectionInfoJson.SafeGetValue("serverIP") as string) ||
                string.IsNullOrEmpty(connectionInfoJson.SafeGetValue("matchToken") as string))
            {
                CustomServerConnectionInfo = null;
            }
            else
            {
                CustomServerConnectionInfo = new CustomServerConnectionInfo(connectionInfoJson);
            }

            if (Application.isEditor)
            {
                GameParams = new Dictionary<string, string>();
                SkillzDifficulty = initializeMockData(jsonData);

                bool? isCustomSync = jsonData.SafeGetBoolValueDefaultFalse("isCustomSynchronousMatch");
                if (!(isCustomSync.HasValue && isCustomSync.Value)) //If not marked as a custom syncronous match
                {
                    CustomServerConnectionInfo = null;
                }
            }
            else
            {
                GameParams = new Dictionary<string, string>();
#if UNITY_IOS
                SkillzDifficulty = initializeWebOrIOSData(jsonData);
#elif UNITY_WEBGL
                if (Debug.isDebugBuild)
                {   
                    SkillzDifficulty = initializeMockData(jsonData);
                }
                else
                {
                    SkillzDifficulty = initializeWebOrIOSData(jsonData);
                }
#elif UNITY_ANDROID
                GameParams = HashtableToDictionary(SkillzCrossPlatform.GetMatchRules());
                SkillzDifficulty = jsonData.SafeGetUintValue("skillzDifficulty");
#endif
            }
        }

        private uint? initializeMockData(JSONDict jsonData)
        {
            uint? skillzDiffFetched = jsonData.SafeGetUintValue("skillzDifficulty");

            object simulatedParameters = jsonData.SafeGetValue("gameParameters");

            if (simulatedParameters != null)
            {
                foreach (object pairs in ((List<object>)simulatedParameters))
                {
                    string key = (string)((JSONDict)pairs)["key"];
                    string value = (string)((JSONDict)pairs)["value"];

                    if (value == null || key == null)
                    {
                        continue;
                    }
                    GameParams.Add(key, value);
                }
            }
            return skillzDiffFetched;
        }

        private uint? initializeWebOrIOSData(JSONDict jsonData)
        {
            uint? skillzDiffFetched = 0;
            object parameters = jsonData.SafeGetValue("gameParameters");
            if (parameters != null && parameters.GetType() == typeof(JSONDict))
            {
                foreach (KeyValuePair<string, object> kvp in (JSONDict)parameters)
                {
                    if (kvp.Value == null)
                    {
                        continue;
                    }

                    string val = kvp.Value.ToString();
                    if (kvp.Key == "skillz_difficulty")
                    {
                        skillzDiffFetched = Helpers.SafeUintParse(val);
                    }
                    else
                    {
                        GameParams.Add(kvp.Key, val);
                    }
                }
            }

            return skillzDiffFetched;
        }

        public override string ToString()
        {
            string paramStr = "";

            foreach (KeyValuePair<string, string> entry in GameParams)
            {
                paramStr += " " + entry.Key + ": " + entry.Value;
            }

            var stringValue = "Match: " +
            " ID: [" + ID + "]" +
            " Name: [" + Name + "]" +
            " Description: [" + Description + "]" +
            " TemplateID: [" + TemplateID + "]" +
            " SkillzDifficulty: [" + SkillzDifficulty + "]" +
            " IsCash: [" + IsCash + "]" +
            " IsSynchronous: [" + IsSynchronous + "]" +
            " IsTieBreaker: [" + IsTieBreaker + "]" +
            " IsBracket: [" + IsBracket + "]" +
            " BracketRound: [" + BracketRound + "]" +
            " IsVideoAdEntry: [" + IsVideoAdEntry + "]" +
            " IsCustomSynchronousMatch: [" + IsCustomSynchronousMatch + "]" +
            " EntryPoints: [" + EntryPoints + "]" +
            " EntryCash: [" + EntryCash + "]" +
            " GameParams: [" + paramStr + "]" +
                " Player: [" + Players + "]";

            if (CustomServerConnectionInfo != null)
            {
                stringValue = string.Concat(stringValue, string.Format(" ConnectionInfo: [{0}]", CustomServerConnectionInfo));
            }

            return stringValue;
        }

        private static Dictionary<string, string> HashtableToDictionary(Hashtable gameParamsHashTable)
        {
            Dictionary<string, string> gameParamsdict = new Dictionary<string, string>();
            foreach (DictionaryEntry entry in gameParamsHashTable)
            {
                gameParamsdict.Add((string)entry.Key, (string)entry.Value);
            }

            return gameParamsdict;
        }
    }
}
