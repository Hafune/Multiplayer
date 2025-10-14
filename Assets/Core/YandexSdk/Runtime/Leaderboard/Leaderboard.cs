using System;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;

namespace Agava.YandexGames
{
    public static class Leaderboard
    {
        // Колбеки для первого лидерборда
        private static Action s_onSetScoreSuccessCallback;
        private static Action<string> s_onSetScoreErrorCallback;
        private static Action<LeaderboardGetEntriesResponse> s_onGetEntriesSuccessCallback;
        private static Action<string> s_onGetEntriesErrorCallback;
        private static Action<LeaderboardEntryResponse> s_onGetPlayerEntrySuccessCallback;
        private static Action<string> s_onGetPlayerEntryErrorCallback;

        // Колбеки для второго лидерборда
        private static Action s_onSetScoreSuccessCallback2;
        private static Action<string> s_onSetScoreErrorCallback2;
        private static Action<LeaderboardGetEntriesResponse> s_onGetEntriesSuccessCallback2;
        private static Action<string> s_onGetEntriesErrorCallback2;
        private static Action<LeaderboardEntryResponse> s_onGetPlayerEntrySuccessCallback2;
        private static Action<string> s_onGetPlayerEntryErrorCallback2;

        // Хранение имен лидербордов
        private static string s_firstLeaderboardName;

        #region SetScore
        public static void SetScore(string leaderboardName, int score, Action onSuccessCallback = null, Action<string> onErrorCallback = null, string extraData = "")
        {
            if (leaderboardName == null)
                throw new ArgumentNullException(nameof(leaderboardName));

            if (extraData == null)
                extraData = string.Empty;

            // Запоминаем первый лидерборд
            if (string.IsNullOrEmpty(s_firstLeaderboardName))
                s_firstLeaderboardName = leaderboardName;

            if (leaderboardName != s_firstLeaderboardName)
            {
                s_onSetScoreSuccessCallback2 = onSuccessCallback;
                s_onSetScoreErrorCallback2 = onErrorCallback;
                LeaderboardSetScore(leaderboardName, score, OnSetScoreSuccessCallback2, OnSetScoreErrorCallback2, extraData);
            }
            else
            {
                s_onSetScoreSuccessCallback = onSuccessCallback;
                s_onSetScoreErrorCallback = onErrorCallback;
                LeaderboardSetScore(leaderboardName, score, OnSetScoreSuccessCallback, OnSetScoreErrorCallback, extraData);
            }
        }

        [DllImport("__Internal")]
        private static extern void LeaderboardSetScore(string leaderboardName, int score, Action successCallback, Action<string> errorCallback, string extraData);

        [MonoPInvokeCallback(typeof(Action))]
        private static void OnSetScoreSuccessCallback()
        {
            if (YandexGamesSdk.CallbackLogging)
                Debug.Log($"{nameof(Leaderboard)}.{nameof(OnSetScoreSuccessCallback)} invoked");

            s_onSetScoreSuccessCallback?.Invoke();
        }

        [MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnSetScoreErrorCallback(string errorMessage)
        {
            if (YandexGamesSdk.CallbackLogging)
                Debug.Log($"{nameof(Leaderboard)}.{nameof(OnSetScoreErrorCallback)} invoked, {nameof(errorMessage)} = {errorMessage}");

            s_onSetScoreErrorCallback?.Invoke(errorMessage);
        }

        [MonoPInvokeCallback(typeof(Action))]
        private static void OnSetScoreSuccessCallback2()
        {
            if (YandexGamesSdk.CallbackLogging)
                Debug.Log($"{nameof(Leaderboard)}.{nameof(OnSetScoreSuccessCallback2)} invoked");

            s_onSetScoreSuccessCallback2?.Invoke();
        }

        [MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnSetScoreErrorCallback2(string errorMessage)
        {
            if (YandexGamesSdk.CallbackLogging)
                Debug.Log($"{nameof(Leaderboard)}.{nameof(OnSetScoreErrorCallback2)} invoked, {nameof(errorMessage)} = {errorMessage}");

            s_onSetScoreErrorCallback2?.Invoke(errorMessage);
        }
        #endregion

        #region GetEntries
        public static void GetEntries(string leaderboardName, Action<LeaderboardGetEntriesResponse> onSuccessCallback, Action<string> onErrorCallback = null, int topPlayersCount = 5, int competingPlayersCount = 5, bool includeSelf = true, ProfilePictureSize pictureSize = ProfilePictureSize.medium)
        {
            if (leaderboardName == null)
                throw new ArgumentNullException(nameof(leaderboardName));

            // Запоминаем первый лидерборд
            if (string.IsNullOrEmpty(s_firstLeaderboardName))
                s_firstLeaderboardName = leaderboardName;

            if (leaderboardName != s_firstLeaderboardName)
            {
                s_onGetEntriesSuccessCallback2 = onSuccessCallback;
                s_onGetEntriesErrorCallback2 = onErrorCallback;
                LeaderboardGetEntries(leaderboardName, OnGetEntriesSuccessCallback2, OnGetEntriesErrorCallback2, topPlayersCount, competingPlayersCount, includeSelf, pictureSize.ToString());
            }
            else
            {
                s_onGetEntriesSuccessCallback = onSuccessCallback;
                s_onGetEntriesErrorCallback = onErrorCallback;
                LeaderboardGetEntries(leaderboardName, OnGetEntriesSuccessCallback, OnGetEntriesErrorCallback, topPlayersCount, competingPlayersCount, includeSelf, pictureSize.ToString());
            }
        }

        [DllImport("__Internal")]
        private static extern void LeaderboardGetEntries(string leaderboardName, Action<string> successCallback, Action<string> errorCallback, int topPlayersCount, int competingPlayersCount, bool includeSelf, string pictureSize);

        [MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnGetEntriesSuccessCallback(string entriesResponseJson)
        {
            if (YandexGamesSdk.CallbackLogging)
                Debug.Log($"{nameof(Leaderboard)}.{nameof(OnGetEntriesSuccessCallback)} invoked, {nameof(entriesResponseJson)} = {entriesResponseJson}");

            LeaderboardGetEntriesResponse entriesResponse = JsonUtility.FromJson<LeaderboardGetEntriesResponse>(entriesResponseJson);
            s_onGetEntriesSuccessCallback?.Invoke(entriesResponse);
        }

        [MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnGetEntriesErrorCallback(string errorMessage)
        {
            if (YandexGamesSdk.CallbackLogging)
                Debug.Log($"{nameof(Leaderboard)}.{nameof(OnGetEntriesErrorCallback)} invoked, {nameof(errorMessage)} = {errorMessage}");

            s_onGetEntriesErrorCallback?.Invoke(errorMessage);
        }

        [MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnGetEntriesSuccessCallback2(string entriesResponseJson)
        {
            if (YandexGamesSdk.CallbackLogging)
                Debug.Log($"{nameof(Leaderboard)}.{nameof(OnGetEntriesSuccessCallback2)} invoked, {nameof(entriesResponseJson)} = {entriesResponseJson}");

            LeaderboardGetEntriesResponse entriesResponse = JsonUtility.FromJson<LeaderboardGetEntriesResponse>(entriesResponseJson);
            s_onGetEntriesSuccessCallback2?.Invoke(entriesResponse);
        }

        [MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnGetEntriesErrorCallback2(string errorMessage)
        {
            if (YandexGamesSdk.CallbackLogging)
                Debug.Log($"{nameof(Leaderboard)}.{nameof(OnGetEntriesErrorCallback2)} invoked, {nameof(errorMessage)} = {errorMessage}");

            s_onGetEntriesErrorCallback2?.Invoke(errorMessage);
        }
        #endregion

        #region GetPlayerEntry
        public static void GetPlayerEntry(string leaderboardName, Action<LeaderboardEntryResponse> onSuccessCallback, Action<string> onErrorCallback = null, ProfilePictureSize pictureSize = ProfilePictureSize.medium)
        {
            if (leaderboardName == null)
                throw new ArgumentNullException(nameof(leaderboardName));

            // Запоминаем первый лидерборд
            if (string.IsNullOrEmpty(s_firstLeaderboardName))
                s_firstLeaderboardName = leaderboardName;

            if (leaderboardName != s_firstLeaderboardName)
            {
                s_onGetPlayerEntrySuccessCallback2 = onSuccessCallback;
                s_onGetPlayerEntryErrorCallback2 = onErrorCallback;
                LeaderboardGetPlayerEntry(leaderboardName, OnGetPlayerEntrySuccessCallback2, OnGetPlayerEntryErrorCallback2, pictureSize.ToString());
            }
            else
            {
                s_onGetPlayerEntrySuccessCallback = onSuccessCallback;
                s_onGetPlayerEntryErrorCallback = onErrorCallback;
                LeaderboardGetPlayerEntry(leaderboardName, OnGetPlayerEntrySuccessCallback, OnGetPlayerEntryErrorCallback, pictureSize.ToString());
            }
        }

        [DllImport("__Internal")]
        private static extern void LeaderboardGetPlayerEntry(string leaderboardName, Action<string> successCallback, Action<string> errorCallback, string pictureSize);

        [MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnGetPlayerEntrySuccessCallback(string entryResponseJson)
        {
            if (YandexGamesSdk.CallbackLogging)
                Debug.Log($"{nameof(Leaderboard)}.{nameof(OnGetPlayerEntrySuccessCallback)} invoked, {nameof(entryResponseJson)} = {entryResponseJson}");

            LeaderboardEntryResponse entryResponse = entryResponseJson == "null" ? null : JsonUtility.FromJson<LeaderboardEntryResponse>(entryResponseJson);
            s_onGetPlayerEntrySuccessCallback?.Invoke(entryResponse);
        }

        [MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnGetPlayerEntryErrorCallback(string errorMessage)
        {
            if (YandexGamesSdk.CallbackLogging)
                Debug.Log($"{nameof(Leaderboard)}.{nameof(OnGetPlayerEntryErrorCallback)} invoked, {nameof(errorMessage)} = {errorMessage}");

            s_onGetPlayerEntryErrorCallback?.Invoke(errorMessage);
        }

        [MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnGetPlayerEntrySuccessCallback2(string entryResponseJson)
        {
            if (YandexGamesSdk.CallbackLogging)
                Debug.Log($"{nameof(Leaderboard)}.{nameof(OnGetPlayerEntrySuccessCallback2)} invoked, {nameof(entryResponseJson)} = {entryResponseJson}");

            LeaderboardEntryResponse entryResponse = entryResponseJson == "null" ? null : JsonUtility.FromJson<LeaderboardEntryResponse>(entryResponseJson);
            s_onGetPlayerEntrySuccessCallback2?.Invoke(entryResponse);
        }

        [MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnGetPlayerEntryErrorCallback2(string errorMessage)
        {
            if (YandexGamesSdk.CallbackLogging)
                Debug.Log($"{nameof(Leaderboard)}.{nameof(OnGetPlayerEntryErrorCallback2)} invoked, {nameof(errorMessage)} = {errorMessage}");

            s_onGetPlayerEntryErrorCallback2?.Invoke(errorMessage);
        }
        #endregion
    }
}