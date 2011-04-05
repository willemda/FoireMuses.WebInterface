using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace FoireMuses.WebInterface.Models
{
	public class JTextualSource : JObject
	{
		public JTextualSource()
		{
		}

		public JTextualSource(JObject jobject)
			: base(jobject)
		{
		}

		public string SourceId
		{
			get { return this["id"].Value<string>(); }
			set { this["id"] = value; }
		}


		public int? AirNumber
		{
			get
			{
				return this["air"].Value<int>();
			}
			set
			{
				this["air"] = value;
			}
		}

		public string Page
		{
			get { return this["page"].Value<string>(); }
			set { this["page"] = value; }
		}


		public string Comment
		{
			get
			{
				return this["comment"].Value<string>();
			}
			set
			{
				this["comment"] = value;
			}
		}

		public bool IsSuggested
		{
			get
			{
				return this["isSuggested"].Value<bool>();
			}
			set
			{
				this["isSuggested"] = value;
			}
		}

		public int? ActNumber
		{
			get
			{
				return this["actNumber"].Value<int?>();
			}
			set
			{
				this["actNumber"] = value;
			}
		}

		public int? SceneNumber
		{
			get
			{
				return this["sceneNumber"].Value<int?>();
			}
			set
			{
				this["sceneNumber"] = value;
			}
		}


		public string PieceId
		{
			get
			{
				return this["pieceId"].Value<string>();
			}
			set
			{
				this["pieceId"] = value;
			}
		}


		public int? Tome
		{
			get
			{
				return this["tome"].Value<int?>();
			}
			set
			{
				this["tome"] = value;
			}
		}

		public int? Volume
		{
			get
			{
				return this["volume"].Value<int?>();
			}
			set
			{
				this["volume"] = value;
			}
		}
	}
}
