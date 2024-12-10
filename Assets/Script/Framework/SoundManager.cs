﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BizzyBeeGames
{
	public class SoundManager : SingletonComponent<SoundManager>
	{

        #region Classes

        [System.Serializable]
		private class SoundInfo
		{
			public string		id					= "";
			public AudioClip	audioClip			= null;
			public SoundType	type				= SoundType.SoundEffect;
			public bool			playAndLoopOnStart	= false;


			[Range(0, 1)] public float clipVolume = 1;
			public bool canDuplicate;
		}

		private class PlayingSound
		{
			public SoundInfo	soundInfo	= null;
			public AudioSource	audioSource	= null;
		}

		#endregion

		#region Enums

		public enum SoundType
		{
			SoundEffect,
			Music
		}
        
		#endregion

		#region Inspector Variables

		[SerializeField] private List<SoundInfo> soundInfos = null;

		#endregion

		#region Member Variables

		private List<PlayingSound> playingAudioSources;
		private List<PlayingSound> loopingAudioSources;

		#endregion

		#region Properties

		public bool IsMusicOn			{ get; private set; }
		public bool IsSoundEffectsOn	{ get; private set; }

		#endregion

		#region Unity Methods

		protected override void Awake()
		{
			base.Awake();
			IsMusicOn = true;
			IsSoundEffectsOn = true;
			playingAudioSources	= new List<PlayingSound>();
			loopingAudioSources	= new List<PlayingSound>();


		}

		private void Start()
		{

			for (int i = 0; i < soundInfos.Count; i++)
			{
				SoundInfo soundInfo = soundInfos[i];
                

				if (soundInfo.playAndLoopOnStart)
				{
					Play(soundInfo.id, true, 0);
				}
			}
		}

		private void Update()
		{
			for (int i = 0; i < playingAudioSources.Count; i++)
			{
				AudioSource audioSource = playingAudioSources[i].audioSource;

				// If the Audio Source is no longer playing then return it to the pool so it can be re-used
				if (!audioSource.isPlaying)
				{
					Destroy(audioSource.gameObject);
					playingAudioSources.RemoveAt(i);
					i--;
				}
			}
		}

		#endregion

		#region Public Methods
		public void SetThemeMusics(AudioClip background_music, AudioClip background_game, AudioClip win)
        {

			for (int i = 0; i < soundInfos.Count; i++)
			{
				SoundInfo soundInfo = soundInfos[i];


				if (soundInfo.playAndLoopOnStart)
				{
					Play(soundInfo.id, true, 0);
				}
			}

		}
		/// <summary>
		/// Plays the sound with the give id
		/// </summary>
		public void Play(string id)
		{
			 bool isPlay = false;

			foreach (PlayingSound i in playingAudioSources)
			{
				if (!i.soundInfo.canDuplicate&&(id != "like4" || id != "win"))
				{
					isPlay = true;
				}
                if (id == "like4" || id == "win")
                {
                    isPlay = false;
                   // Stop(i.soundInfo.id);
                }
            }

            if (!isPlay)
            {
				Play(id, false, 0);
			}
			
		}

		/// <summary>
		/// Plays the sound with the give id, if loop is set to true then the sound will only stop if the Stop method is called
		/// </summary>
		public void Play(string id, bool loop, float playDelay)
		{

			SoundInfo soundInfo = GetSoundInfo(id);

			if (soundInfo == null)
			{
				Debug.LogError("[SoundManager] There is no Sound Info with the given id: " + id);

				return;
			}

			if ((soundInfo.type == SoundType.Music && !IsMusicOn) ||
			    (soundInfo.type == SoundType.SoundEffect && !IsSoundEffectsOn))
			{
				return;
			}

			AudioSource audioSource = CreateAudioSource(id);

			audioSource.clip	= soundInfo.audioClip;
			audioSource.loop	= loop;
			audioSource.time	= 0;
			audioSource.volume	= soundInfo.clipVolume;

			if (playDelay > 0)
			{
				audioSource.PlayDelayed(playDelay);
			}
			else
			{
				audioSource.Play();
			}

			PlayingSound playingSound = new PlayingSound();

			playingSound.soundInfo		= soundInfo;
			playingSound.audioSource	= audioSource;

			if (loop)
			{
				loopingAudioSources.Add(playingSound);
			}
			else
			{
				playingAudioSources.Add(playingSound);
			}
		}

		/// <summary>
		/// Stops all playing sounds with the given id
		/// </summary>
		public void Stop(string id)
		{

			StopAllSounds(id, playingAudioSources);
			StopAllSounds(id, loopingAudioSources);
		}

		/// <summary>
		/// Stops all playing sounds with the given type
		/// </summary>
		public void Stop(SoundType type)
		{

			StopAllSounds(type, playingAudioSources);
			StopAllSounds(type, loopingAudioSources);
		}

		/// <summary>
		/// Sets the SoundType on/off
		/// </summary>
		public void SetSoundTypeOnOff(SoundType type, bool isOn)
		{
			switch (type)
			{
				case SoundType.SoundEffect:
					
					if (isOn == IsSoundEffectsOn)
					{
						return;
					}

					IsSoundEffectsOn = isOn;

					break;
				case SoundType.Music:
					
					if (isOn == IsMusicOn)
					{
						return;
					}

					IsMusicOn = isOn;

					break;
			}

			// If it was turned off then stop all sounds that are currently playing
			if (!isOn)
			{
				Stop(type);
			}
			// Else it was turned on so play any sounds that have playAndLoopOnStart set to true
			else
			{
				PlayAtStart(type);
			}
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Plays all sounds that are set to play on start and loop and are of the given type
		/// </summary>
		private void PlayAtStart(SoundType type)
		{
			for (int i = 0; i < soundInfos.Count; i++)
			{
				SoundInfo soundInfo = soundInfos[i];

				if (soundInfo.type == type && soundInfo.playAndLoopOnStart)
				{
					Play(soundInfo.id, true, 0);
				}
			}
		}
		public void StopAll()
		{
		
				
			
			for (int i = 0; i < loopingAudioSources.Count; i++)
			{
				PlayingSound playingSound = loopingAudioSources[i];

				
					playingSound.audioSource.Stop();
					Destroy(playingSound.audioSource.gameObject);
					loopingAudioSources.RemoveAt(i);
			}

			for (int i = 0; i < playingAudioSources.Count; i++)
			{
				PlayingSound playingSound = playingAudioSources[i];

					playingSound.audioSource.Stop();
					Destroy(playingSound.audioSource.gameObject);
					playingAudioSources.RemoveAt(i);
			}

		}

		/// <summary>
		/// Stops all sounds with the given id
		/// </summary>
		private void StopAllSounds(string id, List<PlayingSound> playingSounds)
		{
			for (int i = 0; i < playingSounds.Count; i++)
			{
				PlayingSound playingSound = playingSounds[i];

				if (id == playingSound.soundInfo.id)
				{
					playingSound.audioSource.Stop();
					Destroy(playingSound.audioSource.gameObject);
					playingSounds.RemoveAt(i);
					i--;
				}
			}
		}

		/// <summary>
		/// Stops all sounds with the given type
		/// </summary>
		private void StopAllSounds(SoundType type, List<PlayingSound> playingSounds)
		{
			for (int i = 0; i < playingSounds.Count; i++)
			{
				PlayingSound playingSound = playingSounds[i];

				if (type == playingSound.soundInfo.type)
				{
					playingSound.audioSource.Stop();
					Destroy(playingSound.audioSource.gameObject);
					playingSounds.RemoveAt(i);
					i--;
				}
			}
		}

		private SoundInfo GetSoundInfo(string id)
		{
			for (int i = 0; i < soundInfos.Count; i++)
			{
				if (id == soundInfos[i].id)
				{
					return soundInfos[i];
				}
			}

			return null;
		}

		private AudioSource CreateAudioSource(string id)
		{
			GameObject obj = new GameObject("sound_" + id);

			obj.transform.SetParent(transform);

			return obj.AddComponent<AudioSource>();;
		}


		#endregion

	}
}
