using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace FoireMuses.Client
{
	public class TextualSource : SourceReference
	{
		public TextualSource()
		{
		}

		public TextualSource(JObject aJobject)
			:base(aJobject)
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

	public class CompleteTextualSource : TextualSource
	{
		public CompleteTextualSource(JObject anObject):base(anObject){}
		public CompleteTextualSource(TextualSource aTextualSource):base(aTextualSource.theJson){}

		public Source Source { get; set; }
		public Play Play { get; set; }
	}
}
