using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace FoireMuses.Client
{
	public class TextualSource
	{
		internal JObject json { get; private set; }

		public TextualSource()
		{
			json = new JObject();
		}

		public TextualSource(JObject jobject)
		{
			json = jobject;
		}

		public string SourceId
		{
			get {
				if (json["id"] == null)
					return null;
				return json["id"].Value<string>(); }
			set { json["id"] = value; }
		}

		public string Page
		{
			get
			{
				if (json["page"] == null)
					return null;
				return json["page"].Value<string>();
			}
			set { json["page"] = value; }
		}

		public string PieceId
		{
			get
			{
				if (json["pieceId"] == null)
					return null;
				return json["pieceId"].Value<string>();
			}
			set
			{
				json["pieceId"] = value;
			}
		}

		public int? AirNumber
		{
			get
			{
				if (json["air"] == null)
					return null;
				return json["air"].Value<int?>();
			}
			set
			{
				json["air"] = value;
			}
		}

		public string Comment
		{
			get
			{
				if (json["comment"] == null)
					return null;
				return json["comment"].Value<string>();
			}
			set
			{
				json["comment"] = value;
			}
		}

		public int? ActNumber
		{
			get
			{
				if (json["actNumber"] == null)
					return null;
				return json["actNumber"].Value<int?>();
			}
			set
			{
				json["actNumber"] = value;
			}
		}

		public int? SceneNumber
		{
			get
			{
				if (json["sceneNumber"] == null)
					return null;
				return json["sceneNumber"].Value<int?>();
			}
			set
			{
				json["sceneNumber"] = value;
			}
		}

		
	}
}
