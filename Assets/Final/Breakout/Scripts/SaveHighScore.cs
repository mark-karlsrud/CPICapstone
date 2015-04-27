using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
using System;

//public class SaveHighScore : MonoBehaviour {
public class SerializeINI : MonoBehaviour
{

	public static void Serialize()
	{
		System.Text.StringBuilder sb = new System.Text.StringBuilder();
		//saves breakout high scores and names
		for(int i=0; i<10; i++)
		{
			sb.AppendLine(Controller.highscore[i].ToString());
		}
		for(int i=0; i<10; i++)
		{
			sb.AppendLine(Controller.names[i]);
		}

		//saves muscle march high scores and names
		for(int i=0; i<10; i++)
		{
			sb.AppendLine(Controller.muscleTimes[i].ToString());
		}
		for(int i=0; i<10; i++)
		{
			sb.AppendLine(Controller.muscleNames[i]);
		}

		//saves ClockTower high scores and names
		for(int i=0; i<10; i++)
		{
			sb.AppendLine(Controller.clockTimes[i].ToString());
		}
		for(int i=0; i<10; i++)
		{
			sb.AppendLine(Controller.clockNames[i]);
		}

		//saves simon says high scores and names
		for(int i=0; i<10; i++)
		{
			sb.AppendLine(Controller.simonHighscore[i].ToString());
		}
		for(int i=0; i<10; i++)
		{
			sb.AppendLine(Controller.simonNames[i]);
		}
		System.IO.StreamWriter writer = new System.IO.StreamWriter(Controller.GamePath);
		writer.Write(sb.ToString());
		writer.Close();
	}
		
	public static void DeSerialize()
	{
		if(File.Exists(Controller.GamePath))
		{
			System.IO.StreamReader reader = new System.IO.StreamReader(Controller.GamePath);
			string line;
			//reads breakout high scores and names
			for(int i=0; i<10; i++)
			{
				line = reader.ReadLine();
			 	Controller.highscore[i] = Convert.ToInt32(line);
			}
			for(int i=0; i<10; i++)
			{
				line = reader.ReadLine();
				Controller.names[i] = line;
			}

			//reads muscle march high scores and names
			for(int i=0; i<10; i++)
			{
				line = reader.ReadLine();
				Controller.muscleTimes[i] = Convert.ToInt32(line);
			}
			for(int i=0; i<10; i++)
			{
				line = reader.ReadLine();
				Controller.muscleNames[i] = line;
			}

			//reads ClockTower high scores and names
			for(int i=0; i<10; i++)
			{
				line = reader.ReadLine();
				Controller.clockTimes[i] = Convert.ToInt32(line);
			}
			for(int i=0; i<10; i++)
			{
				line = reader.ReadLine();
				Controller.clockNames[i] = line;
			}

			//reads simon says high scores and names
			for(int i=0; i<10; i++)
			{
				line = reader.ReadLine();
				Controller.simonHighscore[i] = Convert.ToInt32(line);
			}
			for(int i=0; i<10; i++)
			{
				line = reader.ReadLine();
				Controller.simonNames[i] = line;
			}
			reader.Close();
		}
		else
		{
			for(int i=0; i<10; i++)
			{
				Controller.highscore[i] = 0;
				Controller.names[i] = "aaa";

				Controller.muscleTimes[i] = 1500;
				Controller.muscleNames[i] = "aaa";

				Controller.clockTimes[i] = 1500;
				Controller.clockNames[i] = "aaa";

				Controller.simonHighscore[i] = 0;
				Controller.simonNames[i] = "aaa";
			}
		}
	}
}
