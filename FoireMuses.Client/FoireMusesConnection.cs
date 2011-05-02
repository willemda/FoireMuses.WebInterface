using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MindTouch.Dream;
using System.Net;
using MindTouch.Tasking;
using Newtonsoft.Json.Linq;
using MindTouch.Xml;
using FoireMuses.Client.Helpers;

namespace FoireMuses.Client
{
	public class FoireMusesConnection
	{
		private Plug theServiceUri;
		private ICredentials theCredentials;

		public FoireMusesConnection(XUri aServiceUri, string username, string password)
			: this(aServiceUri, new NetworkCredential(username, password))
		{
		}

		public FoireMusesConnection(XUri aServiceUri, ICredentials aCredentials)
		{
			theCredentials = aCredentials;
			theServiceUri = Plug.New(aServiceUri).WithCredentials(aCredentials);//with timeout
		}

		public FoireMusesConnection Impersonate(string aFullUserName)
		{
			theServiceUri = theServiceUri.WithHeader("FOIREMUSES-IMPERSONATE", aFullUserName);
			return this;
		}


		public Result<Play> GetPlay(string PlayId, Result<Play> aResult)
		{
			theServiceUri
				.At("plays", PlayId)
				.Get(new Result<DreamMessage>())
				.WhenDone(delegate(Result<DreamMessage> answer)
				{
					if (!answer.Value.IsSuccessful)
					{
						if (answer.Value.Status == DreamStatus.NotFound)
							aResult.Return((Play)null);
						else
							aResult.Throw(answer.Exception);
					}
					else
					{
						aResult.Return(new Play(JObject.Parse(answer.Value.ToText())));
					}
				}
				);
			return aResult;
		}


		public Result<Source> GetSource(string SourceId, Result<Source> aResult)
		{
			theServiceUri
				.At("sources", SourceId)
				.Get(new Result<DreamMessage>())
				.WhenDone(delegate(Result<DreamMessage> answer)
				{
					if (!answer.Value.IsSuccessful)
					{
						if (answer.Value.Status == DreamStatus.NotFound)
							aResult.Return((Source)null);
						else
							aResult.Throw(answer.Exception);
					}
					else
					{
						aResult.Return(new Source(JObject.Parse(answer.Value.ToText())));
					}
				}
				);
			return aResult;
		}


		public Result<Source> CreateSource(Source Source, Result<Source> aResult)
		{
			theServiceUri
				.At("sources")
				.Post(DreamMessage.Ok(MimeType.JSON, Source.ToString()), new Result<DreamMessage>())
				.WhenDone(delegate(Result<DreamMessage> answer)
				{
					if (!answer.Value.IsSuccessful)
					{
						if (answer.Value.Status == DreamStatus.NotFound)
							aResult.Return((Source)null);
						else
							aResult.Throw(answer.Exception);
					}
					else
					{
						aResult.Return(new Source(JObject.Parse(answer.Value.ToText())));
					}
				}
				);
			return aResult;
		}

		public Result<Source> EditSource(Source Source, Result<Source> aResult)
		{
			theServiceUri
				.At("sources")
                .With("id", Source.Id)
                .With("rev", Source.Rev)
				.Put(DreamMessage.Ok(MimeType.JSON, Source.ToString()), new Result<DreamMessage>())
				.WhenDone(delegate(Result<DreamMessage> answer)
				{
					if (!answer.Value.IsSuccessful)
					{
						if (answer.Value.Status == DreamStatus.NotFound)
							aResult.Return((Source)null);
						else
							aResult.Throw(answer.Exception);
					}
					else
					{
						aResult.Return(new Source(JObject.Parse(answer.Value.ToText())));
					}
				}
				);
			return aResult;
		}

		public Result<SearchResult<Source>> GetSources(int offset, int max, Result<SearchResult<Source>> aResult)
		{
			theServiceUri
				.At("sources")
				.With("offset", offset)
				.With("max", max)
				.Get(new Result<DreamMessage>())
				.WhenDone(delegate(Result<DreamMessage> answer)
				{
					if (!answer.Value.IsSuccessful)
					{
						if (answer.Value.Status == DreamStatus.NotFound)
							aResult.Return((SearchResult<Source>)null);
						else
							aResult.Throw(answer.Exception);
					}
					else
					{
						aResult.Return(new SearchResult<Source>(JObject.Parse(answer.Value.ToText())));
					}
				}
				);
			return aResult;
		}

		public Result<Score> GetScore(string scoreId, Result<Score> aResult)
		{
			theServiceUri
				.At("scores", scoreId)
				.Get(new Result<DreamMessage>())
				.WhenDone(delegate(Result<DreamMessage> answer)
					{
						if (!answer.Value.IsSuccessful)
						{
							if (answer.Value.Status == DreamStatus.NotFound)
								aResult.Return((Score)null);
							else
								aResult.Throw(answer.Exception);
						}
						else
						{
							aResult.Return(new Score(JObject.Parse(answer.Value.ToText())));
						}
					}
				);
			return aResult;
		}


		public Result<Score> CreateScore(Score score, Result<Score> aResult)
		{
			theServiceUri
				.At("scores")
				.Post(DreamMessage.Ok(MimeType.JSON, score.ToString()), new Result<DreamMessage>())
				.WhenDone(delegate(Result<DreamMessage> answer)
					{
						if (!answer.Value.IsSuccessful)
						{
							if (answer.Value.Status != DreamStatus.Ok)
								aResult.Throw(new Exception());
						}
						else
						{
							aResult.Return(new Score(JObject.Parse(answer.Value.ToText())));
						}
					}
				);
			return aResult;
		}

		public Result<Score> CreateScoreWithXml(XDoc xdoc, Result<Score> aResult)
		{
			theServiceUri
				.At("scores", "musicxml")
				.Post(DreamMessage.Ok(MimeType.XML, xdoc), new Result<DreamMessage>())
				.WhenDone(delegate(Result<DreamMessage> answer)
				{
					if (!answer.Value.IsSuccessful)
					{
						if (answer.Value.Status != DreamStatus.Ok)
							aResult.Throw(new Exception());
					}
					else
					{
						aResult.Return(new Score(JObject.Parse(answer.Value.ToText())));
					}
				}
				);
			return aResult;
		}

		public Result<Score> UpdateScoreWithXml(string id, string rev, XDoc xdoc, bool overwrite, Result<Score> aResult)
		{
			theServiceUri
				.At("scores", "musicxml")
				.With("overwrite", overwrite)
				.With("id", id)
				.With("rev", rev)
				.Put(DreamMessage.Ok(MimeType.XML, xdoc), new Result<DreamMessage>())
				.WhenDone(delegate(Result<DreamMessage> answer)
				{
					if (!answer.Value.IsSuccessful)
					{
						if (answer.Value.Status != DreamStatus.Ok)
							aResult.Throw(new Exception());
					}
					else
					{
						aResult.Return(new Score(JObject.Parse(answer.Value.ToText())));
					}
				}
				);
			return aResult;
		}

		public Result<Score> EditScore(Score score, Result<Score> aResult)
		{
			theServiceUri
				.At("scores")
                .With("Id",score.Id)
                .With("Rev",score.Rev)
				.Put(DreamMessage.Ok(MimeType.JSON, score.ToString()), new Result<DreamMessage>())
				.WhenDone(delegate(Result<DreamMessage> answer)
				{
					if (!answer.Value.IsSuccessful)
					{
						if (answer.Value.Status == DreamStatus.NotFound)
							aResult.Return((Score)null);
						else
							aResult.Throw(answer.Exception);
					}
					else
					{
						aResult.Return(new Score(JObject.Parse(answer.Value.ToText())));
					}
				}
				);
			return aResult;
		}

		public Result<SearchResult<Score>> GetScores(int offset, int max, Result<SearchResult<Score>> aResult)
		{
			theServiceUri
				.At("scores")
				.With("offset", offset)
				.With("max", max)
				.Get(new Result<DreamMessage>())
				.WhenDone(delegate(Result<DreamMessage> answer)
					{
						if (!answer.Value.IsSuccessful)
						{
							if (answer.Value.Status == DreamStatus.NotFound)
								aResult.Return((SearchResult<Score>)null);
							else
								aResult.Throw(answer.Exception);
						}
						else
						{
							aResult.Return(new SearchResult<Score>(JObject.Parse(answer.Value.ToText())));
						}
					}
				);
			return aResult;
		}

		public Result<SearchResult<Play>> GetPlaysFromSource(string sourceId, int offset, int max, Result<SearchResult<Play>> aResult)
		{
			theServiceUri
				.At("plays", "source", sourceId)
				.With("offset", offset)
				.With("max", max)
				.Get(new Result<DreamMessage>())
				.WhenDone(delegate(Result<DreamMessage> answer)
				{
					if (!answer.Value.IsSuccessful)
					{
						if (answer.Value.Status == DreamStatus.NotFound)
							aResult.Return((SearchResult<Play>)null);
						else
							aResult.Throw(answer.Exception);
					}
					else
					{
						aResult.Return(new SearchResult<Play>(JObject.Parse(answer.Value.ToText())));
					}
				}
				);
			return aResult;
		}



		public Result<SearchResult<ScoreSearchItem>> SearchScore(int offset, int max, string title, string editor, string composer, string verses, string music, bool? isMaster, Result<SearchResult<ScoreSearchItem>> aResult)
		{
			theServiceUri
				.At("scores", "search")
				.With("offset", offset)
				.With("max", max)
				.WithCheck("title", title)
				.WithCheck("editor", editor)
				.WithCheck("composer", composer)
				.WithCheck("verses", verses)
				.WithCheck("music", music)
                .WithCheck("isMaster",isMaster)
				.Get(new Result<DreamMessage>())
				.WhenDone(delegate(Result<DreamMessage> answer)
				{
					if (!answer.Value.IsSuccessful)
					{
						if (answer.Value.Status == DreamStatus.NotFound)
							aResult.Return((SearchResult<ScoreSearchItem>)null);
						else
							aResult.Throw(answer.Exception);
					}
					else
					{
						aResult.Return(new SearchResult<ScoreSearchItem>(JObject.Parse(answer.Value.ToText())));
					}
				}
				);
			return aResult;
		}
	}
}

