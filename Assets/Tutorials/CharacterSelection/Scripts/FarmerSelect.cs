using Mirror;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FarmerSelect : NetworkBehaviour
{
   [SerializeField] GameObject characterSelectDisplay;
   [SerializeField] Transform characterPreviewParent;
   [SerializeField] TMP_Text characterNameText;
   [SerializeField] float turnSpeed = 90f;
   [SerializeField] Character[] characters;

   int currentCharacterIndex = 0;
   List<GameObject> characterInstances = new List<GameObject>();

   #region Mirror Callbacks
   public override void OnStartClient()
   {
      if (characterPreviewParent.childCount == 0)
      {
         foreach (var character in characters)
         {
            GameObject characterInstance = Instantiate(character.CharacterPreviewPrefab, characterPreviewParent);

            characterInstance.SetActive(false);

            characterInstances.Add(characterInstance);
         }
      }

      characterInstances[currentCharacterIndex].SetActive(true);
      characterNameText.text = characters[currentCharacterIndex].CharacterName;

      characterSelectDisplay.SetActive(true);
   }
   #endregion

   void Update()
   {
      characterPreviewParent.RotateAround(
          characterPreviewParent.position,
          characterPreviewParent.up,
          turnSpeed * Time.deltaTime);
   }

   public void Select()
   {
      CmdSelect(currentCharacterIndex);
      characterSelectDisplay.SetActive(false);
   }

   [Command(requiresAuthority = false)]
   public void CmdSelect(int characterIndex, NetworkConnectionToClient sender = null)
   {
      GameObject characterInstance = Instantiate(characters[characterIndex].GameplayCharacterPrefab);

      NetworkServer.Spawn(characterInstance, sender);
   }

   public void Right()
   {
      characterInstances[currentCharacterIndex].SetActive(false);

      currentCharacterIndex = (currentCharacterIndex + 1) % characterInstances.Count;

      characterInstances[currentCharacterIndex].SetActive(true);
      characterNameText.text = characters[currentCharacterIndex].CharacterName;
   }

   public void Left()
   {
      characterInstances[currentCharacterIndex].SetActive(false);

      currentCharacterIndex--;

      if (currentCharacterIndex < 0)
      {
         currentCharacterIndex += characterInstances.Count;
      }

      characterInstances[currentCharacterIndex].SetActive(true);
      characterNameText.text = characters[currentCharacterIndex].CharacterName;
   }
}
