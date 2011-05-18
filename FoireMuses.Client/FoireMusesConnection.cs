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
using System.IO;

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

		public FoireMusesConnection(XUri aServiceUri)
		{
			theServiceUri = Plug.New(aServiceUri);//with timeout
		}

		public void Impersonate(string username)
		{
			theServiceUri = theServiceUri.WithHeader("FoireMusesImpersonate", username);
		}


		public Result<User> GetUser(string username, Result<User> aResult)
		{
			theServiceUri.At("users", username)
				.Get(new Result<DreamMessage>())
				.WhenDone(delegate(Result<DreamMessage> answer)
				{
					if (!answer.Value.IsSuccessful)
					{
						aResult.Throw(answer.Exception);
					}
					else
					{
						aResult.Return(new User(JObject.Parse(answer.Value.ToText())));
					}
				}
				);
			return aResult;
		}

		public Result<User> Login(string username, string password, Result<User> aResult)
		{
			theServiceUri.At("users", "login")
				.WithCheck("username", username)
				.WithCheck("password", password)
				.Post(new Result<DreamMessage>())
				.WhenDone(delegate(Result<DreamMessage> answer)
				{
					if (!answer.Value.IsSuccessful)
					{
						aResult.Throw(answer.Exception);
					}
					else
					{
						aResult.Return(new User(JObject.Parse(answer.Value.ToText())));
					}
				}
				);
			return aResult;

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

		public Result<SearchResult<SourceSearchItem>> GetSources(int offset, int max, Result<SearchResult<SourceSearchItem>> aResult)
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
							aResult.Return((SearchResult<SourceSearchItem>)null);
						else
							aResult.Throw(answer.Exception);
					}
					else
					{
						aResult.Return(new SearchResult<SourceSearchItem>(JObject.Parse(answer.Value.ToText())));
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

		public Result<SearchResult<SourcePageSearchItem>> GetSourcePagesFromSource(string sourceId, int max, int offset, Result<SearchResult<SourcePageSearchItem>> aResult)
		{
			theServiceUri
				.At("sources", sourceId, "pages")
				.With("max", max)
				.With("offset", offset)
				.Get(new Result<DreamMessage>())
				.WhenDone(delegate(Result<DreamMessage> answer)
				{
					if (!answer.Value.IsSuccessful)
					{
						if (answer.Value.Status == DreamStatus.NotFound)
							aResult.Return((SearchResult<SourcePageSearchItem>)null);
						else
							aResult.Throw(answer.Exception);
					}
					else
					{
						aResult.Return(new SearchResult<SourcePageSearchItem>(JObject.Parse(answer.Value.ToText())));
					}
				}
				);
			return aResult;
		}

		public Result<Stream> GetConvertedScore(string scoreId, string fileName, Result<Stream> aResult)
		{
			theServiceUri
				.At("scores", scoreId, XUri.Encode(fileName))
				.Get(new Result<DreamMessage>())
				.WhenDone(delegate(Result<DreamMessage> answer)
				{
					if (!answer.Value.IsSuccessful)
					{
						if (answer.Value.Status == DreamStatus.NotFound)
							aResult.Return((Stream)null);
						else
							aResult.Throw(answer.Exception);
					}
					else
					{
						aResult.Return(answer.Value.ToStream());
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
				.Post(DreamMessage.Ok(MimeType.XML, xdoc.ToString()), new Result<DreamMessage>()) //WORKAROUND
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
				.Put(DreamMessage.Ok(MimeType.XML, xdoc.ToString()), new Result<DreamMessage>()) //WORKAROUND
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

		public Result<SearchResult<ScoreSearchItem>> GetScores(int offset, int max, Result<SearchResult<ScoreSearchItem>> aResult)
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

		

		public Result<Stream> GetAttachements(string scoreId, string fileName, Result<Stream> aResult)
		{
			theServiceUri
				.At("scores", scoreId, "attachments", fileName)
				.Get(new Result<DreamMessage>())
				.WhenDone(delegate(Result<DreamMessage> answer)
				{
					if (!answer.Value.IsSuccessful)
					{
						if (answer.Value.Status != DreamStatus.Ok)
							aResult.Throw(new Exception());
					}
					else
					{
						aResult.Return(answer.Value.ToStream());
					}
				}
				);
			return aResult;
		}

		public Result<SourcePage> CreateSourcePage(SourcePage aSourcePage, Result<SourcePage> aResult)
		{
			theServiceUri
				.At("sources","pages")
				.Post(DreamMessage.Ok(MimeType.JSON, aSourcePage.ToString()), new Result<DreamMessage>())
				.WhenDone(delegate(Result<DreamMessage> answer)
				{
					if (!answer.Value.IsSuccessful)
					{
						if (answer.Value.Status != DreamStatus.Ok)
							aResult.Throw(new Exception());
					}
					else
					{
						aResult.Return(new SourcePage(JObject.Parse(answer.Value.ToText())));
					}
				}
				);
			return aResult;
		}

		public Result<SourcePage> EditSourcePage(SourcePage aSourcePage, Result<SourcePage> aResult)
		{
			theServiceUri
				.At("sources", aSourcePage.SourceId, "pages")
				.With("sourcePageId", aSourcePage.Id)
				.With("sourcePageRev", aSourcePage.Rev)
				.Put(DreamMessage.Ok(MimeType.JSON, aSourcePage.ToString()), new Result<DreamMessage>())
				.WhenDone(delegate(Result<DreamMessage> answer)
				{
					if (!answer.Value.IsSuccessful)
					{
						if (answer.Value.Status != DreamStatus.Ok)
							aResult.Throw(new Exception());
					}
					else
					{
						aResult.Return(new SourcePage(JObject.Parse(answer.Value.ToText())));
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


		public Result<SourcePage> GetSourcePage(string sourcePageId, Result<SourcePage> aResult)
		{
			theServiceUri
				.At("sources","pages",sourcePageId)
				.Get(new Result<DreamMessage>())
				.WhenDone(delegate(Result<DreamMessage> answer)
				{
					if (!answer.Value.IsSuccessful)
					{
						if (answer.Value.Status == DreamStatus.NotFound)
							aResult.Return((SourcePage)null);
						else
							aResult.Throw(answer.Exception);
					}
					else
					{
						aResult.Return(new SourcePage(JObject.Parse(answer.Value.ToText())));
					}
				}
				);
			return aResult;

		}


		public Result<SearchResult<ScoreSearchItem>> SearchScore(int offset, int max, Dictionary<string, object> parameters, Result<SearchResult<ScoreSearchItem>> aResult)
		{
			Plug temp = theServiceUri
				.At("scores", "search");
			foreach(KeyValuePair<string, object> pair in parameters){
				temp = temp.WithCheck(pair.Key, pair.Value);
			}
				temp
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

