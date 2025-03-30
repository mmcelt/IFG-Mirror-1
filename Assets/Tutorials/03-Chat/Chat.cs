using Mirror;
using System;
using TMPro;
using UnityEngine;

public class Chat : NetworkBehaviour
{
   #region Fields & Properties

   [SerializeField] GameObject chatUI;
   [SerializeField] TMP_Text chatText;
   [SerializeField] TMP_InputField chatInput;

   public static event Action<string> OnMessage;

   #endregion

   #region Mirror Callbacks

   public override void OnStartAuthority()
   {
      chatUI.SetActive(true);

      OnMessage += HandleNewMessage;
   }

   public override void OnStopAuthority()
   {
      OnMessage -= HandleNewMessage;
   }

   #endregion

   #region Client

   void HandleNewMessage(string message)
   {
      chatText.text += message;
   }

   [Client]
   public void Send(string message)
   {
      if (!Input.GetKeyDown(KeyCode.Return)) return;
      if (string.IsNullOrWhiteSpace(message)) return;

      CmdSendMessage(message);

      chatInput.text = string.Empty;
   }

   [ClientRpc]
   void RpcHandleMessage(string message)
   {
      OnMessage?.Invoke($"\n{message}");
   }
   #endregion

   #region Server

   [Command]
   void CmdSendMessage(string message)
   {
      RpcHandleMessage($"[{connectionToClient.connectionId}]: {message}");
   }
   #endregion
}

