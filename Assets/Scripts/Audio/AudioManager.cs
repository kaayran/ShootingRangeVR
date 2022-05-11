using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        [SerializeField] private AudioMixer _mixer;

        private void Start()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance == this)
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);

            Init();
        }

        private void Init()
        {
        }

        public void MixerHelmetOn(float volume)
        {
            var helmetSnap = _mixer.FindSnapshot("HelmetOn");
            helmetSnap.TransitionTo(.1f);
            helmetSnap.audioMixer.SetFloat("Volume", volume);
        }

        public void MixerDefault()
        {
            var defaultSnap = _mixer.FindSnapshot("Default");
            defaultSnap.TransitionTo(.1f);
            defaultSnap.audioMixer.SetFloat("Volume", 0f);
        }
    }
}