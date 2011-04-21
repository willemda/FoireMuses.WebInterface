using System;
using System.Text;
using MindTouch.Dream;

namespace FoireMuses.Client.Helpers
{
	public static class PlugHelpers
	{
		public static Plug WithCheck(this Plug plug, string fieldName, string fieldValue)
		{
			if (fieldValue != null)
				return plug.With(fieldName, fieldValue);
			return plug;
		}
	}
}