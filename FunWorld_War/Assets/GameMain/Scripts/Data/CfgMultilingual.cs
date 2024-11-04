using System;
using System.Collections.Generic;
using UnityEngine;

using Newtonsoft.Json;

namespace Config.JsonConfig
{
	public class CfgMultilingual: ConfigJsonBase
	{
		public int ID;
		public string Chinese;
		public string TraditionalChinese;
		public string English;
	}
}
