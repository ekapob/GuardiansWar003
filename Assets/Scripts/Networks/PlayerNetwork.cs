using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections;
using UnityEngine.UI;

public class PlayerNetwork : MonoBehaviour {

	public static PlayerNetwork Instance;
	public string PlayerName{ get; private set;}
	private PhotonView PhotonView;
	public int PlayersInGame = 0;
	private ExitGames.Client.Photon.Hashtable m_playerCustomProperties = new ExitGames.Client.Photon.Hashtable ();
	private PlayerMovement CurrentPlayer;
	private Coroutine m_pingCoroutine;
	public int joinRoomNum;
	public bool changeStat;

	// Use this for initialization
	private void Awake () {
		Instance = this;
		PlayersInGame = 0;
		PhotonView = GetComponent<PhotonView> ();
		PlayerName = MotherScript.Instance.inGameName;
		PhotonNetwork.sendRate = 60;
		PhotonNetwork.sendRateOnSerialize = 30;
		SceneManager.sceneLoaded += OnSceneFinishedLoading;
		changeStat = false;
		joinRoomNum = 0;
	}


	//Move player in room to game scene
	private void OnSceneFinishedLoading(Scene scene,LoadSceneMode mode){
		if (scene.name == "Game") {
			if (PhotonNetwork.isMasterClient) {
				MasterLoadedGame();
			} else {
				NonMasterLoadedGame();
			}
		}	
		if (scene.name == "Gameplay") {
			if (PhotonNetwork.isMasterClient) {
				MasterLoadedGamePlay ();
			} else {
				NonMasterLoadedGamePlay ();
			}
		}
		//test TD
		if (scene.name == "GameplayTest") {
			if (PhotonNetwork.isMasterClient) {
				MasterLoadedGamePlay ();
			} else {
				NonMasterLoadedGamePlay ();
			}
		}
	}

	private void Update(){
		Debug.Log ("Status : " + PhotonNetwork.connectionStateDetailed.ToString() + " Player : " + PlayersInGame);
		if (changeStat) {
			PlayerName = MotherScript.Instance.inGameName;
			changeStat = false;
		}


	}
	//Scene 3
	private void MasterLoadedGame(){
		PhotonView.RPC ("RPC_LoadedGameScene", PhotonTargets.MasterClient,PhotonNetwork.player);
		PhotonView.RPC ("RPC_LoadGameOther", PhotonTargets.Others);
	}

	//Scene 4
	private void MasterLoadedGamePlay(){
		PhotonView.RPC ("RPC_LoadedGamePlayScene", PhotonTargets.MasterClient,PhotonNetwork.player);
		PhotonView.RPC ("RPC_LoadGamePlayOther", PhotonTargets.Others);
	}
	//Scene 3
	private void NonMasterLoadedGame(){
		PhotonView.RPC ("RPC_LoadedGameScene", PhotonTargets.MasterClient,PhotonNetwork.player);
	}
	//Scene 4
	private void NonMasterLoadedGamePlay(){
		PhotonView.RPC ("RPC_LoadedGamePlayScene", PhotonTargets.MasterClient,PhotonNetwork.player);
	}


	//Bring all player to game 3
	[PunRPC]
	private void RPC_LoadGameOther(){
		PhotonNetwork.LoadLevel (3);
	}
	//Bring all player to game 4
	[PunRPC]
	private void RPC_LoadGamePlayOther(){
		PhotonNetwork.LoadLevel (4);
	}
		
	//Scene 3
	[PunRPC]
	private void RPC_LoadedGameScene(PhotonPlayer photonPlayer){
		PlayersInGame++;
		if (PlayersInGame == PhotonNetwork.playerList.Length) {
			print ("All player are in the game scene.");
			PhotonView.RPC ("RPC_CreatePlayer",PhotonTargets.All);
		}
	}
	//Scene 4
	[PunRPC]
	private void RPC_LoadedGamePlayScene(PhotonPlayer photonPlayer){
		PlayersInGame++;
		if (PlayersInGame == PhotonNetwork.playerList.Length) {
			print ("All player are in the game scene.");
			//create player cam
			PhotonView.RPC ("RPC_CreatePlayerCam",PhotonTargets.All);
		}
	}

	//Spawn player controlable unit
	[PunRPC]
	private void RPC_CreatePlayer(){
		GameObject obj =  PhotonNetwork.Instantiate (Path.Combine ("Prefabs", "PlayerSoul"), CanvasGameButton.Instance.spawner1[joinRoomNum-1].transform.position, Quaternion.identity, 0);
		PlayersInGame = 0;
	}
	//Spawn player controlable unit
	[PunRPC]
	private void RPC_CreatePlayerCam(){
		Quaternion rotate = Quaternion.Euler(new Vector3(75, 0, 0));
		GameObject obj =  PhotonNetwork.Instantiate (Path.Combine ("Prefabs", "PlayerCamera"), PauseAndExitButton.Instance.camSpawnPoint.transform.position,rotate, 0);
	}

	private IEnumerator C_SetPing(){
		while(PhotonNetwork.connected){
			m_playerCustomProperties ["Ping"] = PhotonNetwork.GetPing ();
			PhotonNetwork.player.SetCustomProperties (m_playerCustomProperties);

			yield return new WaitForSeconds (5f);
		}

		yield break;
	}

	private void OnConnectedToMaster(){
		if (m_pingCoroutine != null) {
			StopCoroutine (m_pingCoroutine);
		}
		m_pingCoroutine = StartCoroutine (C_SetPing ());
	}

	public void setLogText(int pos,string name,string tw1lv1,string tw1lv2,string tw1lv3,string tw2lv1,string tw2lv2,string tw2lv3,string tw3lv1,string tw3lv2,string tw3lv3,string unit1,string unit2,string unit3,string unit4){
		PhotonView.RPC ("RPC_SetLogTxt",PhotonTargets.All,pos,name,tw1lv1,tw1lv2,tw1lv3,tw2lv1,tw2lv2,tw2lv3,tw3lv1,tw3lv2,tw3lv3,unit1,unit2,unit3,unit4);
	}
		
	[PunRPC]
	public void RPC_SetLogTxt(int pos,string name,string tw1lv1,string tw1lv2,string tw1lv3,string tw2lv1,string tw2lv2,string tw2lv3,string tw3lv1,string tw3lv2,string tw3lv3,string unit1,string unit2,string unit3,string unit4){
		BattleLogTxt playerLog = MotherLogScript.Instance.battleLogScript [pos];
		playerLog.SetTxt(0,name);
		playerLog.SetTxt(1,tw1lv1);
		playerLog.SetTxt(2,tw1lv2);
		playerLog.SetTxt(3,tw1lv3);
		playerLog.SetTxt(4,tw2lv1);
		playerLog.SetTxt(5,tw2lv2);
		playerLog.SetTxt(6,tw2lv3);
		playerLog.SetTxt(7,tw3lv1);
		playerLog.SetTxt(8,tw3lv2);
		playerLog.SetTxt(9,tw3lv3);
		playerLog.SetTxt(10,unit1);
		playerLog.SetTxt(11,unit2);
		playerLog.SetTxt(12,unit3);
		playerLog.SetTxt(13,unit4);
	}
}
