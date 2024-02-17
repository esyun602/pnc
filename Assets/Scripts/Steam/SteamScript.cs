using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
// 참고 링크: https://steamworks.github.io/
public class SteamScript : MonoBehaviour
{
    private ulong lobbyId = 0;

    private SteamScript instance;
    
    private void Start()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowRemotePlayOverlay()
    {
        if (SteamManager.Initialized)
        {
            if(lobbyId == 0)
            {
                Callback<LobbyCreated_t>.Create(OnLobbyCreated);
                var lobby = SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypePrivate, 2);
            }
            else
            {
                SteamFriends.ActivateGameOverlayRemotePlayTogetherInviteDialog((CSteamID)lobbyId);
            }
        }
    }

    private void OnLobbyCreated(LobbyCreated_t lobbyCreated_t)
    {
        if (lobbyCreated_t.m_eResult == EResult.k_EResultOK)
        {
            lobbyId = lobbyCreated_t.m_ulSteamIDLobby;
            SteamFriends.ActivateGameOverlayRemotePlayTogetherInviteDialog((CSteamID)lobbyCreated_t.m_ulSteamIDLobby);
        }
    }

    private void Update()
    {
        if (SteamManager.Initialized)
        {
            SteamAPI.RunCallbacks();
        }
    }
}
