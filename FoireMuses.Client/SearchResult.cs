using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace FoireMuses.Client
{

	public class SearchResult<T> : JObject where T : JObject
	{
		public int Offset { get { return this["offset"].Value<int>(); } }
		public int Max { get { return this["max"].Value<int>(); } }
		public int TotalCount { get { return this["total_rows"].Value<int>(); } }


		public SearchResult(JObject jo):base(jo)
		{
		}

		public IList<T> Rows
		{
			get{
				JArray test = this["rows"].Value<JArray>();
				IList<T> tlist = new List<T>();
				ConstructorInfo ctor = typeof(T).GetConstructor(new Type[] { typeof(JObject) });
				foreach (JObject jsonT in test.Values<JObject>())
				{
					T obj = ctor.Invoke(new object[] { jsonT}) as T;
					tlist.Add(obj);
				}
				return tlist;
			}
		}
	}
}
