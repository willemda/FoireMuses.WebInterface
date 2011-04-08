using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MindTouch.Dream;
using System.Net;
using MindTouch.Tasking;
using Newtonsoft.Json.Linq;
using FoireMuses.Core.Interfaces;

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


		public Result<IPlay> GetPlay(string IPlayId, Result<IPlay> aResult)
		{
			theServiceUri
				.At("plays", IPlayId)
				.Get(new Result<DreamMessage>())
				.WhenDone(delegate(Result<DreamMessage> answer)
				{
					if (!answer.Value.IsSuccessful)
					{
						if (answer.Value.Status == DreamStatus.NotFound)
							aResult.Return((IPlay)null);
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


		public Result<ISource> GetSource(string ISourceId, Result<ISource> aResult)
		{
			theServiceUri
				.At("sources", ISourceId)
				.Get(new Result<DreamMessage>())
				.WhenDone(delegate(Result<DreamMessage> answer)
				{
					if (!answer.Value.IsSuccessful)
					{
						if (answer.Value.Status == DreamStatus.NotFound)
							aResult.Return((ISource)null);
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


		public Result<ISource> CreateSource(ISource ISource, Result<ISource> aResult)
		{
			theServiceUri
				.At("sources")
				.Post(DreamMessage.Ok(MimeType.JSON, ISource.ToString()), new Result<DreamMessage>())
				.WhenDone(delegate(Result<DreamMessage> answer)
				{
					if (!answer.Value.IsSuccessful)
					{
						if (answer.Value.Status == DreamStatus.NotFound)
							aResult.Return((ISource)null);
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

		public Result<ISource> EditSource(ISource ISource, Result<ISource> aResult)
		{
			theServiceUri
				.At("sources")
				.Put(DreamMessage.Ok(MimeType.JSON, ISource.ToString()), new Result<DreamMessage>())
				.WhenDone(delegate(Result<DreamMessage> answer)
				{
					if (!answer.Value.IsSuccessful)
					{
						if (answer.Value.Status == DreamStatus.NotFound)
							aResult.Return((ISource)null);
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

		public Result<SearchResult<ISource>> GetSources(int offset, int max, Result<SearchResult<ISource>> aResult)
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
							aResult.Return((SearchResult<ISource>)null);
						else
							aResult.Throw(answer.Exception);
					}
					else
					{
						aResult.Return(new SearchResult<ISource>(x=>new Source(x),JObject.Parse(answer.Value.ToText())));
					}
				}
				);
			return aResult;
		}

		public Result<IScore> GetScore(string scoreId, Result<IScore> aResult)
		{
			theServiceUri
				.At("scores", scoreId)
				.Get(new Result<DreamMessage>())
				.WhenDone(delegate(Result<DreamMessage> answer)
					{
						if (!answer.Value.IsSuccessful)
						{
							if (answer.Value.Status == DreamStatus.NotFound)
								aResult.Return((IScore)null);
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


		public Result<IScore> CreateScore(IScore score, Result<IScore> aResult)
		{
			theServiceUri
				.At("scores")
				.Post(DreamMessage.Ok(MimeType.JSON,score.ToString()),new Result<DreamMessage>())
				.WhenDone(delegate(Result<DreamMessage> answer)
					{
						if (!answer.Value.IsSuccessful)
						{
							if (answer.Value.Status == DreamStatus.NotFound)
								aResult.Return((IScore)null);
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

		public Result<IScore> EditScore(IScore score, Result<IScore> aResult)
		{
			theServiceUri
				.At("scores")
				.Put(DreamMessage.Ok(MimeType.JSON, score.ToString()), new Result<DreamMessage>())
				.WhenDone(delegate(Result<DreamMessage> answer)
				{
					if (!answer.Value.IsSuccessful)
					{
						if (answer.Value.Status == DreamStatus.NotFound)
							aResult.Return((IScore)null);
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

		public Result<SearchResult<IScore>> GetScores(int offset, int max, Result<SearchResult<IScore>> aResult)
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
								aResult.Return((SearchResult<IScore>)null);
							else
								aResult.Throw(answer.Exception);
						}
						else
						{
							aResult.Return(new SearchResult<IScore>(x=>new Score(x),JObject.Parse(answer.Value.ToText())));
						}
					}
				);
			return aResult;
		}

		
	}
}

