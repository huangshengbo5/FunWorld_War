using System;
using System.Collections.Generic;
using UnityEngine;

using Newtonsoft.Json;

namespace Config.JsonConfig
{
	public class CfgNPC: ConfigJsonBase
	{
		public int ID;
		public int nameid;
		public string icon;
		public int Type;
		public int quality;
		public int camp;
		public int sex;
		public int level;
		public int vocation;
		public int model;
		public int fee;
		public string avalue_base;
		public string avalue_senior;
		public int skill;
		public int skill_passive;
		public int build_time;
		public int next;
		public int upgrade_gold;
		public string upgrade_item;
	}
}
