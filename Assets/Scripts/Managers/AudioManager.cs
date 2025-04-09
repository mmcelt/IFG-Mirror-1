using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
   #region Fields & Properties

   [SerializeField] AudioMixer masterMixer;
   public AudioSource[] Sources;

   #endregion

   #region Unity Callbacks

   //protected override void Awake()
   //{
   //   base.Awake();
   //   DontDestroyOnLoad(gameObject);
   //}
   #endregion

   public void PlaySound(int index, float v = 1f)
   {
      //StopAllSources();
      Sources[index].volume = v;
      Sources[index].Play();
   }

   public void StopAllSources()
   {
      foreach (var source in Sources)
      {
         source.Stop();
      }
   }
}

