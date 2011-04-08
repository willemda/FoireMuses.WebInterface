using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoireMuses.Core.Interfaces
{
	public interface IPlay
	{

		string Id { get; }

		string Abstract { get; set; }
		string ActionLocation { get; set; }
		string Author { get; set; }
		string ContemporaryComments { get; set; }
		string CreationPlace { get; set; }
		string CreationYear { get; set; }
		string Critics { get; set; }
		string Decors { get; set; }
		string EntrepreneurName { get; set; }
		string Genre { get; set; }
		string Iconography { get; set; }
		string MusicianName { get; set; }
		string Resonances { get; set; }
		string SourceFolio { get; set; }
		string Title { get; set; }

		string SourceId { get; set; }
		int? SourceTome { get; set; }
		int? SourceVolume { get; set; }

		IEnumerable<string> Tags { get; }
		void AddTag(string tag);
		void RemoveTag(string tag);

		string CreatorId { get; }
		string LastModifierId { get; }

		IEnumerable<string> CollaboratorsId { get; }
		void AddCollaborator(string collab);
		void RemoveCollaborator(string collab);
	}
}
