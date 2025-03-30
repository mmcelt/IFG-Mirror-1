using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character Selection/Character")]
public class Character : ScriptableObject
{
   [SerializeField] string characterName;
   [SerializeField] GameObject characterPreviewPrefab;
   [SerializeField] GameObject gameplayCharacterPrefab;

   public string CharacterName => characterName;
   public GameObject CharacterPreviewPrefab => characterPreviewPrefab;
   public GameObject GameplayCharacterPrefab => gameplayCharacterPrefab;
}
