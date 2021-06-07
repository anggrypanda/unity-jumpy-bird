using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class GameData 
{
	private int diamondScore;
	private int bestScore;

	private bool[] birds;
	private int selectedIndex;

	public int DiamondScore 
	{
		get 
		{ 
			return diamondScore;
		}
		set 
		{
			diamondScore = value;
		}
	}

	public int BestScore 
	{
		get 
		{ 
			return bestScore;
		}
		set 
		{ 
			bestScore = value;
		}
	}

	public bool[] Birds 
	{
		get 
		{ 
			return birds; 
		}
		set 
		{
			birds = value;
		}
	}

	public int SelectedIndex 
	{
		get 
		{ 
			return selectedIndex;
		}
		set 
		{ 
			selectedIndex = value;
		}
	}
}