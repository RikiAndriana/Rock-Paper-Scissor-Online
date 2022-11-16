using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.SceneManagement;

public class ConnectManager : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_InputField usernameInput;
    [SerializeField] TMP_Text feedbackText;

    private void Start()
    {
        Application.runInBackground = true;
        usernameInput.text = PlayerPrefs.GetString(PropertyNames.Player.NickName, "");
    }

    public void ClickConnect()
    {
        if (usernameInput.text.Length < 3)
        {
            feedbackText.text = "Username min 3 characters";
            return;
        }
        // simpan username
        PlayerPrefs.SetString(PropertyNames.Player.NickName, usernameInput.text);
        PhotonNetwork.NickName = usernameInput.text;
        PhotonNetwork.AutomaticallySyncScene = true;

        // connect ke server
        PhotonNetwork.ConnectUsingSettings();
        feedbackText.text = "Connecting...";
    }

    public void ClickBack()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // dijalankan ketika sudah connect
    public override void OnConnectedToMaster()
    {
        feedbackText.text = "Connected";
        SceneManager.LoadScene("Lobby");
        StartCoroutine(LoadSceneAfterConnectedAndReady());
    }
    IEnumerator LoadSceneAfterConnectedAndReady()
    {
        while (PhotonNetwork.IsConnectedAndReady == false)
            yield return null;
    }
}
