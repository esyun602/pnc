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
            string name = SteamFriends.GetPersonaName();
            Debug.Log(name);
            SteamFriends.ActivateGameOverlay("Friends");
        }
    }
}
