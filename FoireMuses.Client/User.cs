using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace FoireMuses.Client
{
	public class User : JObject
	{

		public User()
		{

			this.Add("otype", "user");
		}

		public User(JObject jobject)
			: base(jobject)
		{
			JToken type;
			if (this.TryGetValue("otype", out type))
			{
				if (type.Value<string>() != "user")
					throw new Exception("Bad object type");
			}
			else
			{
				this.Add("otype", "user");
			}
		}

		public string Id
		{
			get { return this["_id"].Value<string>(); }
		}

		public string Password
		{
			get { return this["password"].Value<string>(); }
			set { this["password"] = value; }
		}

		public string Email
		{
			get { return this["email"].Value<string>(); }
			set { this["email"] = value; }
		}

		public IEnumerable<string> Groups
		{
			get { return this["groups"].Values<string>(); }
		}

		public void AddGroup(string group)
		{
			if (!Groups.Contains(group))
			{
				JArray temp = this["groups"].Value<JArray>();
				temp.Add(group);
				this["groups"] = temp;
			}
		}

		public void RemoveGroup(string group)
		{
			this["groups"] = this["groups"].Value<JArray>().Remove(group);
		}


		static Regex goodCharsForUsernameRegex = new Regex("\\w{5,20}");
		static Regex goodCharsForPasswordRegex = new Regex("\\w{5,20}");

		private void validate()
		{
			CheckUsername();
			CheckPassword();
		}

		private void CheckUsername()
		{
			if (String.IsNullOrEmpty(Id))
			{
				throw new ArgumentException("username");
			}
			if (DoesContainBadCharacters(goodCharsForUsernameRegex, Id))
			{
				throw new ArgumentException("username");
			}
		}

		private void CheckPassword()
		{
			if (String.IsNullOrEmpty(Password))
			{
				throw new ArgumentException("password");
			}
			if (DoesContainBadCharacters(goodCharsForPasswordRegex, Password))
			{
				throw new ArgumentException("password");
			}
		}
		


		private bool DoesContainBadCharacters(Regex theGoodCharsRegex, string theString)
		{
			if (!theGoodCharsRegex.IsMatch(theString))
			{
				return true;
			}
			return false;
		}
	}
}
