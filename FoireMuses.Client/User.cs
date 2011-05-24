using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace FoireMuses.Client
{
	public class User
	{

		private JObject json { get;  set; }

		public User()
		{
			json = new JObject();
			json.Add("otype", "user");
		}

		public User(JObject jobject)
		{
			json = jobject;
			JToken type;
			if (json.TryGetValue("otype", out type))
			{
				if (type.Value<string>() != "user")
					throw new Exception("Bad object type");
			}
			else
			{
				json.Add("otype", "user");
			}
		}

		public string Id
		{
			get { return json["_id"].Value<string>(); }
			set { json["_id"] = value; }
		}

		public string Password
		{
			get { return json["password"].Value<string>(); }
			set { json["password"] = value; }
		}

		public string Email
		{
			get { return json["email"].Value<string>(); }
			set { json["email"] = value; }
		}

		public bool IsAdmin
		{
			get {
                if (json["isAdmin"] == null)
                    return false;
                return json["isAdmin"].Value<bool>(); }
			set { json["isAdmin"] = value; }
		}

		public IEnumerable<string> Groups
		{
			get { return json["groups"].Values<string>(); }
		}

		public void AddGroup(string group)
		{
			if (!Groups.Contains(group))
			{
				JArray temp = json["groups"].Value<JArray>();
				temp.Add(group);
				json["groups"] = temp;
			}
		}

		public void RemoveGroup(string group)
		{
			json["groups"] = json["groups"].Value<JArray>().Remove(group);
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

        public override string ToString()
        {
            return json.ToString();
        }
	}
}
