﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Photon.Pun;
using Photon.Realtime;


namespace Com.MyCompany.MyGame
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
		#region Public Fields
		public static GameManager Instance;
		[Tooltip("The prefab to use for representing the player")]
		public GameObject playerPrefab;

		#endregion



		#region MonoBehaviour Callbacks
		void Start(){
			Instance = this;
			if(playerPrefab == null){
				Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'GameManager'");
			}else{
				if (PlayerManager.LocalPlayerInstance == null){
					Debug.LogFormat("We are Instanting LocalPlayer from{0}", Application.loadedLevelName);
					// we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
					PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f,5f,0f), Quaternion.identity,0);
				}else{
					Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
				}
			}
		}

		void Update(){
			// Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);
		}
		#endregion

		#region Photon Callbacks

		public override void OnPlayerEnteredRoom(Player newPlayer){
			Debug.LogFormat("OnPlayerEnteredRoom {0}", newPlayer.NickName); // not seen if you're the player connecting

			if (PhotonNetwork.IsMasterClient){
				Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom

				LoadArena();
			}
		}

		public override void OnPlayerLeftRoom(Player otherPlayer){
			Debug.LogFormat("OnPlayerLeftRoom() {0}", otherPlayer.NickName); // seen when other player disconnects

			if (PhotonNetwork.IsMasterClient){
				Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom

				LoadArena();
			}
		}

		/// <summary>
		/// Called when the local player left the romm. We need to load the launcher scene.
		/// </summary>
		public override void OnLeftRoom(){
			SceneManager.LoadScene(0);
		}

		#endregion

		#region Public Methods 
		
		public void LeaveRoom(){
			PhotonNetwork.LeaveRoom();
		}

		#endregion

		#region Private Methods
		void LoadArena(){
			if(!PhotonNetwork.IsMasterClient){
				Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
			}
			Debug.LogFormat("PhotonNetwork : Loading Level : {0}", PhotonNetwork.CurrentRoom.PlayerCount);
			PhotonNetwork.LoadLevel("Room for " + PhotonNetwork.CurrentRoom.PlayerCount);
		}
		#endregion
    }
}

