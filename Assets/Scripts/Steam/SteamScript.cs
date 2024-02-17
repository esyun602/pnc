using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
// 참고 링크: https://steamworks.github.io/
public class SteamScript : MonoBehaviour
{
    public void ShowRemotePlayOverlay()
    {
        if (SteamManager.Initialized)
        {
            Callback<LobbyCreated_t>.Create(OnLobbyCreated);
            var lobby = SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypePrivate, 2);
        }
    }

    private void OnLobbyCreated(LobbyCreated_t lobbyCreated_t)
    {
        if (lobbyCreated_t.m_eResult == EResult.k_EResultOK)
        {
            SteamFriends.ActivateGameOverlayRemotePlayTogetherInviteDialog((CSteamID)lobbyCreated_t.m_ulSteamIDLobby);
        }
    }

    private void Update()
    {
        SteamAPI.RunCallbacks();
    }
}
