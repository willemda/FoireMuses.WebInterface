using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace FoireMuses.Client
{
	public abstract class SearchResult
	{
		protected JObject Json { get; private set; }
		public int Offset { get { return Json["offset"].Value<int>(); } }
		public int Max { get { return Json["max"].Value<int>(); } }
		public int TotalCount { get { return Json["total_rows"].Value<int>(); } }

		protected SearchResult(JObject aJobject)
		{
			Json = aJobject;
		}
	}

	public class SearchResult<T> : SearchResult where T : SearchResultItem, new()	
	{
		private IList<T> theRows;

		public SearchResult(JObject aJObject)
			:base(aJObject)
		{}

		public IEnumerable<T> Rows
		{
			get{
				if (theRows == null)
				{
					JArray test = Json["rows"].Value<JArray>();
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
