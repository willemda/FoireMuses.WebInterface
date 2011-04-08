using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoireMuses.Core.Interfaces
{
	public interface IGroup
	{
		string Name { get; set; }

		bool IsPublic { get; set; }
		string CreatorId { get; set; }
	}
}
