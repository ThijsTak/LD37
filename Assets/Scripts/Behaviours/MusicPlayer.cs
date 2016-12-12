using Core;
using UnityEngine;

namespace Assets.Scripts.Behaviours
{
	public class MusicPlayer : MonoBehaviour
	{
		private AudioSource source;

		public void Play()
		{
			source.Play();
		}

		public void Pause()
		{
			source.Pause();
		}

		void Start()
		{
			source = GetComponent<AudioSource>();
		}

		void Update()
		{
			if (!source.isPlaying)
			{
				NextNumber();
			}
		}

		void NextNumber()
		{
			source.clip =
				GlobalManager.Instance.MusicBuffer.AmbientMudicClips[
					Random.Range(0, GlobalManager.Instance.MusicBuffer.AmbientMudicClips.Length)];
			source.Play();
		}

		void NextBattleNumber()
		{
			source.clip =
				GlobalManager.Instance.MusicBuffer.BattleMusicClips[
					Random.Range(0, GlobalManager.Instance.MusicBuffer.BattleMusicClips.Length)];
			source.Play();
		}
	}
}
