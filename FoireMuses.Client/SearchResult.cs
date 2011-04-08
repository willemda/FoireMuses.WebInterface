using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace FoireMuses.Client
{

	public class SearchResult<T>
	{
		public JObject json { get; private set; }
		private Func<JObject, T> cFunc;
		public int Offset { get { return json["offset"].Value<int>(); } }
		public int Max { get { return json["max"].Value<int>(); } }
		public int TotalCount { get { return json["total_rows"].Value<int>(); } }


		public SearchResult(Func<JObject,T> func, JObject jo)
		{
			json = jo;
			cFunc = func;
		}

		public IList<T> Rows
		{
			get{
				JArray test = json["rows"].Value<JArray>();
				IList<T> tlist = new List<T>();
				foreach (JObject jsonT in test.Values<JObject>())
				{
					var obj = cFunc(jsonT);
					tlist.Add(obj);
				}
				return tlist;
			}
		}
	}
}
