using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace FoireMuses.Client
{
	public class TextualSource : SourceReference
	{
		public TextualSource():base()
		{
		}

		public TextualSource(JObject jobject)
			:base(jobject)
		{
		}

		public string PieceId
		{
			get
			{
				if (theJson["pieceId"] == null)
					return null;
				return theJson["pieceId"].Value<string>();
			}
			set
			{
				theJson["pieceId"] = value;
			}
		}

		public int? AirNumber
		{
			get
			{
				if (theJson["air"] == null)
					return null;
				return theJson["air"].Value<int?>();
			}
			set
			{
				theJson["air"] = value;
			}
		}

		public string Comment
		{
			get
			{
				if (theJson["comment"] == null)
					return null;
				return theJson["comment"].Value<string>();
			}
			set
			{
				theJson["comment"] = value;
			}
		}

		public int? ActNumber
		{
			get
			{
				if (theJson["actNumber"] == null)
					return null;
				return theJson["actNumber"].Value<int?>();
			}
			set
			{
				theJson["actNumber"] = value;
			}
		}

		public int? SceneNumber
		{
			get
			{
				if (theJson["sceneNumber"] == null)
					return null;
				return theJson["sceneNumber"].Value<int?>();
			}
			set
			{
				theJson["sceneNumber"] = value;
			}
		}

		
	}
}
