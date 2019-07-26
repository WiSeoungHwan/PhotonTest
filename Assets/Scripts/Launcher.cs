using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace Com.Mycompany.MyGame
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
		#region Private Serializable Fields

		#endregion

		#region Private Fields

		/// <summary>
		/// This client's version number. User are separated from each other by gameVersion(which allows you make breaking changes)
		/// </summary>
		string gameVersion = "1";

		#endregion

		#region MonoBehaviourPunCallbacks Callbacks

		public override void OnConnectedToMaster(){
			Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN");
			PhotonNetwork.JoinRandomRoom();
		}

		public override void OnDisconnected(DisconnectCause cause){
			Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
		}

		#endregion

		#region MonoBehaviour Callbacks

		/// <summary>
		/// MonoBehaviour method called on GameObject by Unity during early initialization phase(단계).
		/// </summary>
		void Awake(){
			// #Critical
			// this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically 
			PhotonNetwork.AutomaticallySyncScene = true;
		}

		/// <summary>
		/// MonoBehaviour method called on GameObject by Unity during initialization phase(단계).
		/// </summary>
		void Start(){
			Connect();
		}

		#endregion
        
		#region Public Methods

		/// <summary>
		/// Start the connection process 
		/// - if already connected, we attempt joining a random room
		/// - if not yet connected, Connect this application instance to Photon Cloud Network
		/// </summary>

		public void Connect(){

			// we check if we are connected or not, we join if we are, else we initiate the connection to the server.
			if(PhotonNetwork.IsConnected){
				// #Critical we need at this point to attempt joining a Ramdom Room. If it fails, we'll get notified in OnJoinRandomFailed() and we'll create one
				PhotonNetwork.JoinRandomRoom(); // 네트워크에 연결되면 랜덤방에 입장.
			}else{
				// #Critical we must first and foremost(맨앞에 위치한) connect to Photon Online Server.
				PhotonNetwork.GameVersion = gameVersion;
				PhotonNetwork.ConnectUsingSettings();
			}
		}

		#endregion
    }
}