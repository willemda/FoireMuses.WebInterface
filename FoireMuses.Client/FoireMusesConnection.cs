using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MindTouch.Dream;
using System.Net;
using MindTouch.Tasking;
using Newtonsoft.Json.Linq;

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


		public Result<Play> GetPlay(string playId, Result<Play> aResult)
		{
			theServiceUri
				.At("plays", playId)
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


		public Result<Source> GetSource(string sourceId, Result<Source> aResult)
		{
			theServiceUri
				.At("sources", sourceId)
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


		public Result<Source> CreateSource(Source source, Result<Source> aResult)
		{
			theServiceUri
				.At("sources")
				.Post(DreamMessage.Ok(MimeType.JSON, source.ToString()), new Result<DreamMessage>())
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

		public Result<Source> EditSource(Source source, Result<Source> aResult)
		{
			theServiceUri
				.At("sources")
				.Put(DreamMessage.Ok(MimeType.JSON, source.ToString()), new Result<DreamMessage>())
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

		public Result<SearchResult<Source>> GetSource(int offset, int max, Result<SearchResult<Source>> aResult)
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
				.Post(DreamMessage.Ok(MimeType.JSON,score.ToString()),new Result<DreamMessage>())
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

		public Result<Score> EditScore(Score score, Result<Score> aResult)
		{
			theServiceUri
				.At("scores")
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
				.With("offset",offset)
				.With("max",max)
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
	}
}

