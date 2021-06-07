using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour 
{
	public static SoundManager instance;

	public AudioSource bgSoundManager, diamondSoundManager, jumpSoundManager;

	public Button musicBtn;
	public Sprite musicOnImg, musicOffImg;
	private bool playMusic;

	void Awake() 
	{
		MakeInstance();
	}
	
	void MakeInstance() 
	{
		if (instance == null) 
		{
			instance = this;
		}
	}

	public void MusicControl() 
	{
		if (playMusic) 
		{	
			playMusic = false;
			musicBtn.image.sprite = musicOnImg;

			bgSoundManager.Stop();
		} 
		else 
		{
			playMusic = true;
			musicBtn.image.sprite = musicOffImg;

			bgSoundManager.Play();
		}
	}

	public void PlayDiamondSound() 
	{
		diamondSoundManager.Play();
	}

	public void PlayJumpSound() 
	{
		jumpSoundManager.Play();
	}
}