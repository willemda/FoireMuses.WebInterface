using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace FoireMuses.Client
{
	public class ScoreFix
	{

		public JObject json { get; set; }

		public string Id { get; private set; }

		public string Title { get; set; }
		public string Code1 { get; set; }
		public string Code2 { get; set; }
		public string Coirault { get; set; }
		public string Composer { get; set; }
		public string CoupeMetrique { get; set; }
		public string Verses { get; set; }
		public string Delarue { get; set; }
		public string Comments { get; set; }
		public string Editor { get; set; }
		public string RythmSignature { get; set; }
		public string OtherTitles { get; set; }
		public string Stanza { get; set; }
		public string ScoreType { get; set; }

		public ScoreFix(JObject jo)
		{
			this.Id = ExtractStringHelper("_id", jo);
			this.Title = ExtractStringHelper("title", jo);
			this.Code1 = ExtractStringHelper("code1", jo);
			this.Code2 = ExtractStringHelper("code2", jo);
			this.Coirault = ExtractStringHelper("coirault", jo);
			this.Composer = ExtractStringHelper("composer", jo);
			this.CoupeMetrique = ExtractStringHelper("coupeMetrique", jo);
			this.Verses = ExtractStringHelper("verse", jo);
			this.Delarue = ExtractStringHelper("delarue", jo);
			this.Comments = ExtractStringHelper("comments", jo);
			this.Editor = ExtractStringHelper("editor", jo);
			this.RythmSignature = ExtractStringHelper("rythmSignature", jo);
			this.OtherTitles = ExtractStringHelper("otherTitles", jo);
			this.Stanza = ExtractStringHelper("stanza", jo);
			this.ScoreType = ExtractStringHelper("type", jo);
		}

		private string ExtractStringHelper(string valueName, JObject jo)
		{
			if (jo[valueName] == null)
				return null;
			return jo[valueName].Value<string>();
		}

		private int? ExtractIntHelper(string valueName, JObject jo)
		{
			if (jo[valueName] == null)
				return null;
			return jo[valueName].Value<int?>();
		}
	}
}
