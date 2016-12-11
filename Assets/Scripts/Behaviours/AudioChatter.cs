using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Plays a random amount of clips and then waits again to play another random amount. This simulates it being "chatty".
/// </summary>
public class AudioChatter : MonoBehaviour
{

	[Header ("Clips in sequence")]
	public int MinInSequence = 1;
	public int MaxInSequence = 5;


	[Header ("Delay between effects")]
	public float MinBetweenEffects = 0.0f;
	public float MaxBetweenEffects = 0.5f;


	[Header ("Delay between sequence")]
	public float MinBetweenSequence = 1.0f;
	public float MaxBetweenSequence = 3.0f;

	[Header ("Audio")]
	public AudioSource Source;
	public List<AudioClip> Clips;

	private List<AudioClip> Sequence = new List<AudioClip> ();
	private float? nextAttempt;

	// Use this for initialization
	void Start ()
	{
		nextAttempt = null;	
		Source.loop = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Clips.Count == 0 || Clips.Any (x => x == null)) {
			Debug.LogWarning ("No audioclips");
			return;
		}

		if (nextAttempt != null && nextAttempt > Time.time) {
			// Not yet time for next attempt
			return;
		}

		if (Sequence.Count == 0) {
			// No sequence prepared. Generate a sequence and return.
			GenerateSequence ();
			nextAttempt = Time.time + Random.Range (MinBetweenSequence, MaxBetweenSequence);
			return;
		}

		// Get next clip and remove it from the sequence.
		var next_clip = Sequence.First ();
		Sequence.RemoveAt (0);

		Source.clip = next_clip;
		Source.Play ();

		nextAttempt = Time.time + Random.Range (MinBetweenEffects, MaxBetweenEffects) + next_clip.length;
	}

	void GenerateSequence ()
	{
		// Play the next sound sample
		var clips_in_sequence = Mathf.RoundToInt (Random.Range (MinInSequence, MaxInSequence));

		var random_order = Sequence.OrderBy (x => Random.value).ToArray ();

		var index = 0;
		for (int i = 0; i < clips_in_sequence; ++i) {
			// Increase index
			index += Mathf.RoundToInt (Random.value * 32);
			index = index % Clips.Count;
			Sequence.Add (Clips [index]);
		}
	}
}
