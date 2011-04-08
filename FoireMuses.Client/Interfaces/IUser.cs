using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace FoireMuses.Core.Interfaces
{
	public interface IUser
	{
		string Id { get; set; }
		string Rev { get; set; }

		string Password { get; set; }
		string Email { get; set; }
		IEnumerable<string> Groups { get; }
		void AddGroup(string group);
		void RemoveGroup(string group);
	}
}
