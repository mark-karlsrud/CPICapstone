using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
using System;

//public class SaveHighScore : MonoBehaviour {
public class SerializeINI
{

	public static void Serialize()
	{
		System.Text.StringBuilder sb = new System.Text.StringBuilder();
		for(int i=0; i<10; i++)
		{
			sb.AppendLine(Controller.highscore[i].ToString());
		}
		for(int i=0; i<10; i++)
		{
			sb.AppendLine(Controller.names[i]);
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
			//while((line = reader.ReadLine()) != null)
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
			reader.Close();
		}
		else
		{
			for(int i=0; i<10; i++)
			{
				Controller.highscore[i] = 0;
				Controller.names[i] = "aaa";
			}
		}
	}
}
