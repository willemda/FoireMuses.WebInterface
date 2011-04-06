using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace FoireMuses.Client
{
	public class TextualSource : JObject
	{
		public TextualSource()
		{
		}

		public TextualSource(JObject jobject)
			: base(jobject)
		{
		}

		public string SourceId
		{
			get {
				if (this["id"] == null)
					return null;
				return this["id"].Value<string>(); }
			set { this["id"] = value; }
		}


		public int? AirNumber
		{
			get
			{
				if (this["air"] == null)
					return null;
				return this["air"].Value<int>();
			}
			set
			{
				this["air"] = value;
			}
		}

		public string Page
		{
			get {
				if (this["page"] == null)
					return null;
				return this["page"].Value<string>(); }
			set { this["page"] = value; }
		}


		public string Comment
		{
			get
			{
				if (this["comment"] == null)
					return null;
				return this["comment"].Value<string>();
			}
			set
			{
				this["comment"] = value;
			}
		}

		public bool? IsSuggested
		{
			get
			{
				if (this["isSuggested"] == null)
					return null;
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
				if (this["actNumber"] == null)
					return null;
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
				if (this["sceneNumber"] == null)
					return null;
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
				if (this["pieceId"] == null)
					return null;
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
				if (this["tome"] == null)
					return null;
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
				if (this["volume"] == null)
					return null;
				return this["volume"].Value<int?>();
			}
			set
			{
				this["volume"] = value;
			}
		}
	}
}
