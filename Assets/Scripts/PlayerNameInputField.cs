﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

namespace Com.MyCompany.MyGame
{
	/// <summary>
	/// Player name input field. Let the user input his name, will appear above the player in the game.null
	/// </summary>
	[RequireComponent(typeof(InputField))]
    public class PlayerNameInputField : MonoBehaviour
    {
		#region Private Constants
		// Store the PlayerPref key to avoid(방지) typos(오타)
		const string playerNamePrefkey = "PlayerName";

		#endregion
		
		#region MonoBehaviour CallBacks

		/// <summary>
		/// MonoBehaviour method called on GameObject by Unity during initailization phase.
		/// </summary>
		void Start(){

			string defaultName = string.Empty;
			InputField _inputField = this.GetComponent<InputField>();
			if (_inputField!= null){
				if (PlayerPrefs.HasKey(playerNamePrefkey)){
					defaultName = PlayerPrefs.GetString(playerNamePrefkey);
					_inputField.text = defaultName;
				}
			}

			PhotonNetwork.NickName = defaultName;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Set the name of the player, and save it in the PlayerPrefs for future sessions.
		/// </summary>
		/// <param name="value">The name of the Player</param>
		public void SetPlayerName(string value){
			// #Important
			if(string.IsNullOrEmpty(value)){
				Debug.LogError("Player Name is null or empty");
				return;
			}
			PhotonNetwork.NickName = value;
			
			PlayerPrefs.SetString(playerNamePrefkey, value);
		}

		#endregion
		
    }

}