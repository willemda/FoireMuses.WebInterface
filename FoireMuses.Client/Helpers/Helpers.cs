using System;
using System.Text;
using MindTouch.Dream;
using Newtonsoft.Json.Linq;

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

		public static Plug WithCheck(this Plug plug, string fieldName, bool? fieldValue)
		{
			if (fieldValue != null && fieldValue.HasValue)
				return plug.With(fieldName, fieldValue.Value);
			return plug;
		}

		public static Plug WithCheck(this Plug plug, string fieldName, object fieldValue)
		{
			if (fieldValue == null || fieldName == null || String.IsNullOrWhiteSpace(fieldName))
				return plug;
			if (fieldValue is string && !String.IsNullOrWhiteSpace(fieldValue as string))
			{
				return plug.With(fieldName, fieldValue as string);
			}
			if (fieldValue is bool? && (fieldValue as bool?).HasValue)
			{
				return plug.With(fieldName, (fieldValue as bool?).Value);
			}
			return plug;
		}
	}

	public static class JObjectHelper
	{

		public static void AddCheck(this JObject jo, string fieldName, string fieldValue)
		{
			if (fieldValue != null)
				jo[fieldName] = fieldValue;
			else
				jo.Remove(fieldName);
		}

		public static void AddCheck(this JObject jo, string fieldName, int? fieldValue)
		{
			if (fieldValue != null)
				jo[fieldName] = fieldValue;
			else
				jo.Remove(fieldName);
		}

		public static void AddCheck(this JObject jo, string fieldName, bool? fieldValue)
		{
			if (fieldValue != null)
				jo[fieldName] = fieldValue;
			else
				jo.Remove(fieldName);
		}


		public static string RetrieveStringCheck(this JObject jo, string fieldName)
		{
			if (jo[fieldName] != null)
				return jo[fieldName].Value<string>();
			return null;
		}

		public static bool? RetrieveBoolCheck(this JObject jo, string fieldName)
		{
			if (jo[fieldName] != null)
				return jo[fieldName].Value<bool?>();
			return null;
		}

		public static int? RetrieveIntCheck(this JObject jo, string fieldName)
		{
			if (jo[fieldName] != null)
				return jo[fieldName].Value<int?>();
			return null;
		}
	}
}