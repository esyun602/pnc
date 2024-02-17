using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
// 참고 링크: https://steamworks.github.io/
public class SteamScript : MonoBehaviour
{
    void Start()
    {
        if (SteamManager.Initialized)
        {
            SteamFriends.ActivateGameOverlayRemotePlayTogetherInviteDialog(SteamFriends.GetFriendByIndex(0, EFriendFlags.k_EFriendFlagImmediate));
            string name = SteamFriends.GetFriendPersonaName(SteamFriends.GetFriendByIndex(0, EFriendFlags.k_EFriendFlagImmediate));
            Debug.Log(name);
            //SteamFriends.ActivateGameOverlay("Friends");
        }
    }
}
