using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies.Models;
using Unity.Services.Lobbies;
using UnityEngine;
using System.Threading.Tasks;
using TMPro;
using System.Collections.Generic;
using Unity.Services.Relay.Models;
using Unity.Services.Relay;
using Unity.Networking.Transport.Relay;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;

public class LobbyManeger2 : MonoBehaviour
{
    public TMP_InputField PlaynemeInput, LobbycoldInput;
    public GameObject introLobby, LobbyPanel;
    public TMP_Text[] PlayerNemeText;
    public TMP_Text LobbyCold;

    public GameObject StatGameButtom;

    bool StardtGmae = false;
    Lobby HostLobby, JoinedLobby;


    async void Start()
    {
        await UnityServices.InitializeAsync();

    }

    async Task Autenticaite()
    {
        if (AuthenticationService.Instance.IsSignedIn) return;

        AuthenticationService.Instance.ClearSessionToken();

        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        Debug.Log("Usuario Logado como " + AuthenticationService.Instance.PlayerId);
    }

    async public void CreatLobby()
    {
        await Autenticaite();

        CreateLobbyOptions options = new CreateLobbyOptions
        {
            Player = GetPlayer(),
            Data = new Dictionary<string, DataObject>
            {
                { "StarGame", new DataObject(DataObject.VisibilityOptions.Member, "0") }
            }
        };

        HostLobby = await Lobbies.Instance.CreateLobbyAsync("Lobby", 4, options);
        JoinedLobby = HostLobby;
        LobbyCold.text = HostLobby.LobbyCode;
        InvokeRepeating("SendLobbyHearBeat", 5, 5);
        UpdateLobby();
        ShowPlayers();
        introLobby.SetActive(false);
        LobbyPanel.SetActive(true);
        StatGameButtom.SetActive(true);
    }

    void checkForUpdat()
    {
        if (JoinedLobby == null || StardtGmae) return;

        UpdateLobby();
        ShowPlayers();

        if (JoinedLobby.Data["StarGame"].Value != "0")
        {
            if (HostLobby == null)
            {
                JoinRelay(JoinedLobby.Data["StarGame"].Value);
            }
            StardtGmae = true;
        }
    }

    async public void JoinLobbyBayCold()
    {
        await Autenticaite();

        JoinLobbyByCodeOptions options = new JoinLobbyByCodeOptions
        {
            Player = GetPlayer()

        };

        JoinedLobby = await Lobbies.Instance.JoinLobbyByCodeAsync(LobbycoldInput.text, options);
        Debug.Log("Entrou no lobby " + JoinedLobby.LobbyCode);
        ShowPlayers();
        LobbyCold.text = JoinedLobby.LobbyCode;
        introLobby.SetActive(false);
        LobbyPanel.SetActive(true);
        InvokeRepeating("checkForUpdat", 3,3);
    }


    Player GetPlayer()
    {
        Player player = new Player
        {
            Data = new Dictionary<string, PlayerDataObject>
            {
                { "name", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Public, PlaynemeInput.text) }
            }
        };
        return player;
    }

    async void SendLobbyHearBeat()
    {
        if (HostLobby == null) return;

        await LobbyService.Instance.SendHeartbeatPingAsync(HostLobby.Id);
        ShowPlayers();
        Debug.Log("AutoricoU O LOBBY");
    }

    void ShowPlayers()
    {
        for (int i = 0; i < JoinedLobby.Players.Count; i++)
        {
            PlayerNemeText[i].text = JoinedLobby.Players[i].Data["name"].Value;
        }
    }

    async void UpdateLobby()
    {
        if (JoinedLobby == null) return;

        JoinedLobby = await LobbyService.Instance.GetLobbyAsync(JoinedLobby.Id);
    }

    async Task<string> CreateRely()
    {
        Allocation allocation = await RelayService.Instance.CreateAllocationAsync(4);

        string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

        RelayServerData relayServerData = new RelayServerData(allocation, "dtls");

        NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);
        NetworkManager.Singleton.StartHost();

        return joinCode;
    }

    public async void StartGame()
    {
        string relayCode = await CreateRely();

        Lobby lobby = await Lobbies.Instance.UpdateLobbyAsync(JoinedLobby.Id, new UpdateLobbyOptions
        {
            Data = new Dictionary<string, DataObject>
            {
                { "StarGame", new DataObject(DataObject.VisibilityOptions.Member, relayCode) }
            }

        });

        JoinedLobby = lobby;

        LobbyPanel.SetActive(false);
    }

    async void JoinRelay(string joincode)
    {
        JoinAllocation allocation = await RelayService.Instance.JoinAllocationAsync(joincode);

        RelayServerData relayServerData = new RelayServerData(allocation, "dtls");

        NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);
        NetworkManager.Singleton.StartClient();

        LobbyPanel.SetActive(false);
    }
}
