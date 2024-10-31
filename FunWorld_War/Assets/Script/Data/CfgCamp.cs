using System;
using System.Collections.Generic;
using UnityEngine;

namespace Config.TextConfig
{
	public class CfgCamp : ConfigTextBase
	{
		public int CampID_1;
		public int CampID_2;
		public int CampID_3;
		public int CampID_4;
		public int CampID_5;
		public int CampID_6;
		public int CampID_7;
		public int CampID_8;
		public int CampID_9;
		public int CampID_10;
		public int CampID_11;
		public int CampID_12;
		public int CampID_13;
		public int CampID_14;
		public int CampID_15;


		public override void Write(int i, string value)
		{
			switch (i)
			{
				case 0:
					ID = ParseInt(value);
					break;
				case 1:
					CampID_1 = ParseInt(value);
					break;
				case 2:
					CampID_2 = ParseInt(value);
					break;
				case 3:
					CampID_3 = ParseInt(value);
					break;
				case 4:
					CampID_4 = ParseInt(value);
					break;
				case 5:
					CampID_5 = ParseInt(value);
					break;
				case 6:
					CampID_6 = ParseInt(value);
					break;
				case 7:
					CampID_7 = ParseInt(value);
					break;
				case 8:
					CampID_8 = ParseInt(value);
					break;
				case 9:
					CampID_9 = ParseInt(value);
					break;
				case 10:
					CampID_10 = ParseInt(value);
					break;
				case 11:
					CampID_11 = ParseInt(value);
					break;
				case 12:
					CampID_12 = ParseInt(value);
					break;
				case 13:
					CampID_13 = ParseInt(value);
					break;
				case 14:
					CampID_14 = ParseInt(value);
					break;
				case 15:
					CampID_15 = ParseInt(value);
					break;
				default:
					UnityEngine.Debug.LogError(GetType().Name + "src i:" + i);
					break;
			}
		}
	}
}
