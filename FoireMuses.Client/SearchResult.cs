using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace FoireMuses.Client
{

	public class SearchResult<T> where T : SearchResultItem, new()
	{
		private IList<T> theRows;

		public JObject json { get; private set; }
		public int Offset { get { return json["offset"].Value<int>(); } }
		public int Max { get { return json["max"].Value<int>(); } }
		public int TotalCount { get { return json["total_rows"].Value<int>(); } }

		public SearchResult(JObject jo)
		{
			json = jo;
		}

		public IEnumerable<T> Rows
		{
			get{
				if (theRows == null)
				{
					JArray test = json["rows"].Value<JArray>();
					theRows = new List<T>();
					ConstructorInfo ctor = typeof (T).GetConstructor(new Type[] {typeof (JObject)});
					foreach (T obj in test.Values<JObject>().Select(jsonT => new T {Json = jsonT}))
					{
						theRows.Add(obj);
					}
				}
				return theRows;
			}
		}
	}
}
