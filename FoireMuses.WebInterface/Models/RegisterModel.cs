using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoireMuses.Webinterface.Models
{
	public class RegisterModel
	{
		public string Username { get; set; }
		public string Password { get; set; }
		public string PasswordConfirmation { get; set; }
		public string Email { get; set; }
		public bool IsAdmin { get; set; }
	}
}