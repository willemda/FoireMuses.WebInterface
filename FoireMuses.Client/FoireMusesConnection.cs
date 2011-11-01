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
		private Dictionary<string, Source> theSourcesCache = new Dictionary<string, Source>();

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
						if (answer.Value.Status == DreamStatus.NotFound)
							aResult.Return((User)null);
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

		/// <summary>
		/// Create Source Pages from Zip file of facsimilés
		/// </summary>
		/// <param name="sourceId">Is of the source</param>
		/// <param name="file">Zip File name</param>
		/// <param name="aResult">Async management Result</param>
		/// <returns></returns>
		public Result<bool> CreateSourcePagesFromFacsimileZipFile(string sourceId, Stream file, Result<bool> aResult)
		{
			theServiceUri
				.At("sources",sourceId,"pages")
				.Post(DreamMessage.Ok(new MimeType("application/zip"), file.Length, file),new Result<DreamMessage>())
				.WhenDone(delegate(Result<DreamMessage> answer)
				{
					if (!answer.Value.IsSuccessful)
					{
						if (answer.Value.Status == DreamStatus.BadRequest)
							aResult.Return(false);
						else
							aResult.Throw(answer.Exception);
					}
					else
					{
						aResult.Return(true);
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
						aResult.Return(new Play {Json = JObject.Parse(answer.Value.ToText())});
					}
				}
				);
			return aResult;
		}

		public Result<Source> GetSource(string aSourceId, Result<Source> aResult)
		{
			if (theSourcesCache.ContainsKey(aSourceId))
			{
				aResult.Return(theSourcesCache[aSourceId]);
			}

			theServiceUri
				.At("sources", aSourceId)
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
						Source src = new Source(JObject.Parse(answer.Value.ToText()));
						lock(theSourcesCache)
						{
							if (theSourcesCache.ContainsKey(aSourceId))
								theSourcesCache.Remove(aSourceId);
							theSourcesCache.Add(aSourceId,src);
						}
						aResult.Return(src);
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

		public Result<Source> UpdateSource(Source Source, Result<Source> aResult)
		{
			lock (theSourcesCache)
			{
				if (theSourcesCache.ContainsKey(Source.Id))
					theSourcesCache.Remove(Source.Id);
			}

			theServiceUri
				.At("sources",Source.Id)
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

		public Result<Stream> GetConvertedScore(string aScoreId, string aFileName, Result<Stream> aResult)
		{
			theServiceUri
				.At("scores", aScoreId, XUri.Encode(aFileName))
				.Get(new Result<DreamMessage>())
				.WhenDone(delegate(Result<DreamMessage> anAnswer)
				{
					if (!anAnswer.Value.IsSuccessful)
					{
						if (anAnswer.Value.Status == DreamStatus.NotFound)
							aResult.Return((Stream)null);
						else
							aResult.Throw(anAnswer.Exception);
					}
					else
					{
						aResult.Return(anAnswer.Value.ToStream());
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
				.At("scores")
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
				.At("scores", id)
				.With("overwrite", overwrite)
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

		public Result<Score> UpdateScore(Score score, Result<Score> aResult)
		{
			 theServiceUri
				.At("scores", score.Id)
				.With("Rev", score.Rev)
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

		public Result<Play> CreatePlay(Play aPlay, Result<Play> aResult)
		{
			theServiceUri
				.At("plays")
				.Post(DreamMessage.Ok(MimeType.JSON, aPlay.ToString()), new Result<DreamMessage>())
				.WhenDone(delegate(Result<DreamMessage> answer)
				{
					if (!answer.Value.IsSuccessful)
					{
						if (answer.Value.Status != DreamStatus.Ok)
							aResult.Throw(new Exception());
					}
					else
					{
						aResult.Return(new Play {Json = JObject.Parse(answer.Value.ToText())});
					}
				}
				);
			return aResult;
		}

		public Result<Play> UpdatePlay(Play aPlay, Result<Play> aResult)
		{
			theServiceUri.At("plays", aPlay.Id).With("Rev", aPlay.Rev).Put(DreamMessage.Ok(MimeType.JSON, aPlay.ToString()),
			                                                               new Result<DreamMessage>())
				.WhenDone(delegate(Result<DreamMessage> answer)
				          	{
				          		if (!answer.Value.IsSuccessful)
				          		{
				          			if (answer.Value.Status == DreamStatus.NotFound)
				          				aResult.Return((Play) null);
				          			else
				          				aResult.Throw(answer.Exception);
				          		}
				          		else
				          		{
				          			aResult.Return(new Play{Json = JObject.Parse(answer.Value.ToText())});
				          		}
				          	}
				);

			return aResult;
		}

		public Result<SearchResult<ScoreSearchItem>> GetScores(int anOffset, int aMax, Result<SearchResult<ScoreSearchItem>> aResult)
		{
			theServiceUri
				.At("scores")
				.With("offset", anOffset)
				.With("max", aMax)
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
							SearchResult<ScoreSearchItem> searchResult =
								new SearchResult<ScoreSearchItem>(JObject.Parse(answer.Value.ToText()));

							foreach(ScoreSearchItem item in searchResult.Rows)
							{
								if(item.TextualSource != null && !String.IsNullOrEmpty(item.TextualSource.SourceId))
								{
									Source src = GetSource(item.TextualSource.SourceId, new Result<Source>()).Wait();

									item.TextualSource.Source = src;
									if(!String.IsNullOrEmpty(item.TextualSource.PieceId))
									{
										item.TextualSource.Play = GetPlay(item.TextualSource.PieceId, new Result<Play>()).Wait();
									}
								}
								if(item.MusicalSource != null && !String.IsNullOrEmpty(item.MusicalSource.SourceId))
								{
									item.MusicalSource.Source = GetSource(item.MusicalSource.SourceId, new Result<Source>()).Wait();
								}
							}

							aResult.Return(searchResult);
						}
					}
				);
			return aResult;
		}

		public Result AddAttachment(string aScoreId, string aFileName, Result aResult)
		{
			theServiceUri
				.At("scores", aScoreId, "attachments", Path.GetFileName(aFileName))
				.Post(DreamMessage.FromFile(aFileName),new Result<DreamMessage>())
				.WhenDone(delegate(Result<DreamMessage> answer)
				{
					if (!answer.Value.IsSuccessful)
					{
						if (answer.Value.Status != DreamStatus.Ok)
							aResult.Throw(new Exception());
					}
					else
					{
						aResult.Return();
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

		public Result<SourcePage> CreateSourcePage(string aSourceId, SourcePage aSourcePage, Result<SourcePage> aResult)
		{
			theServiceUri
				.At("sources", aSourceId, "pages")
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

		public Result<SourcePage> UpdateSourcePage(string aSourceId, SourcePage aSourcePage, Result<SourcePage> aResult)
		{
			theServiceUri
				.At("sources", aSourceId, "pages", aSourcePage.Id)
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

		public Result<SourcePage> GetSourcePage(string sourceId, string sourcePageId, Result<SourcePage> aResult)
		{
			theServiceUri
				.At("sources", sourceId, "pages", sourcePageId)
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
			Plug temp = theServiceUri.At("scores", "search");

			foreach (KeyValuePair<string, object> pair in parameters)
			{
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
					SearchResult<ScoreSearchItem> searchResult =
	new SearchResult<ScoreSearchItem>(JObject.Parse(answer.Value.ToText()));

					foreach (ScoreSearchItem item in searchResult.Rows)
					{
						if (item.TextualSource != null && !String.IsNullOrEmpty(item.TextualSource.SourceId))
						{
							Source src = GetSource(item.TextualSource.SourceId, new Result<Source>()).Wait();

							item.TextualSource.Source = src;
							if (!String.IsNullOrEmpty(item.TextualSource.PieceId))
							{
								item.TextualSource.Play = GetPlay(item.TextualSource.PieceId, new Result<Play>()).Wait();
							}
						}
						if (item.MusicalSource != null && !String.IsNullOrEmpty(item.MusicalSource.SourceId))
						{
							item.MusicalSource.Source = GetSource(item.MusicalSource.SourceId, new Result<Source>()).Wait();
						}
					}

					aResult.Return(searchResult);
				}
			}
			);
			return aResult;
		}

		public Result<User> CreateUser(User user, Result<User> aResult)
		{
			theServiceUri
				.At("users")
				.Post(DreamMessage.Ok(MimeType.JSON, user.ToString()), new Result<DreamMessage>())
				.WhenDone(delegate(Result<DreamMessage> answer)
				{
					if (!answer.Value.IsSuccessful)
					{
						if (answer.Value.Status == DreamStatus.NotFound)
							aResult.Return((User)null);
						else
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
	}
}

