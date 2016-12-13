using UnityEngine;

namespace Assets.Scripts.Behaviours
{
	[RequireComponent(typeof(AudioSource))]
	class SoundEffectFireAndForget : MonoBehaviour
	{
		private AudioSource source;
		public AudioClip[] clipsToPlay;

		public void Start()
		{
			source = GetComponent<AudioSource>();
			source.clip = clipsToPlay[Random.Range(0, clipsToPlay.Length)];
			source.Play();
		}

		void Update()
		{
			if (!source.isPlaying)
			{
				GameObject.Destroy(gameObject);
			}
		}
	}
}
