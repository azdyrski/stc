using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using HtmlAgilityPack;

namespace StcDataScraper
{
	class Program
	{
		static void Main(string[] args)
		{
			DataScraper ds = new DataScraper();
			//var pageDomain = "http://sharktanksuccess.blogspot.com";
			var pageGetter = new HtmlWeb();
			string fileText = string.Empty;

			//Dictionary<int,

			#region Season 1
			//season 1 season and episodes
			var tvDbSeason1Page = pageGetter.Load("http://thetvdb.com/?tab=season&seriesid=100981&seasonid=83461&lid=7");
			var tableList = tvDbSeason1Page.GetElementbyId("listtable").Descendants("tr").ToList();
			var seasonNum = 1;
			DataScraper.Season season1 = new DataScraper.Season() { SeasonNumber = seasonNum, SeasonDate = DateTime.Parse("2009-08-09") };
			fileText += ds.SeasonInsertString(season1);
			for (int i = 1; i < tableList.Count; i++) //avoid headers at row 0
			{
				var rowData = tableList[i];
				DataScraper.Episode episode = new DataScraper.Episode();
				episode.EpisodeName = rowData.Descendants("a").ToList()[1].InnerText.Replace("'","''");
				episode.SeasonId = seasonNum;
				episode.EpisodeNumber = i;
				episode.EpisodeDate = DateTime.Parse(rowData.Descendants("td").ToList()[2].InnerText);

				fileText += ds.EpisodeInsertString(episode);

				//season 1 products
				var productPage = pageGetter.Load("http://sharktanksuccess.blogspot.com/2012/10/shark-tank-season-1-15-episodes-68.html"); //use for partial product name completion
				var divList = productPage.DocumentNode.Descendants("div").ToList();
				foreach (var divElement in divList)
				{
					if (divElement.Attributes["class"] != null && divElement.Attributes["class"].Value == "post-body entry-content")
					{

						//Console.Out.WriteLine("Found content div");
						var episodeElementList = divElement.Descendants("ul").ToList();
						//fileText += ds.SeasonInsertString(1, new DateTime(2009, 8, 9));
						var episodeElement = episodeElementList[i-1];
						//Console.Out.WriteLine("Episode " + episodeNum.ToString());
						//http://thetvdb.com/?tab=season&seriesid=100981&seasonid=83461&lid=7 possibly use for episode names and dates?
						//fileText += ds.EpisodeInsertString("",episodeNum,null,1);
						var productList = episodeElement.Descendants("li").ToList();
						foreach (var productElement in productList)
						{
							DataScraper.Product product = new DataScraper.Product() { EpisodeId = i };
							var linkProductList = productElement.Descendants("a").ToList();
							var linkProduct = linkProductList.Count == 0 ? null : linkProductList[0];
							if (productElement.InnerText != string.Empty)
							{ 
								if (linkProduct == null)
								{
									//Console.Out.WriteLine("   " + productElement.InnerText);
									string nameString = WebUtility.HtmlDecode(productElement.InnerText).Replace("'", "''");
									int dashPos = nameString.IndexOf("-");
									product.ProductName = WebUtility.HtmlDecode(productElement.InnerText).Replace("'", "''").Substring(0,dashPos-1).Trim();
								}
								else
								{
									//Console.Out.WriteLine("   " + linkProduct.InnerText);
									product.ProductName = WebUtility.HtmlDecode(linkProduct.InnerText).Replace("'", "''").Trim();
								}
							}
							if (!string.IsNullOrEmpty(product.ProductName))
								fileText += ds.ProductInsertString(product);
						}
					}
				}
			}
			#endregion

			#region Season 2
			//season 2 season and episodes
			var tvDbSeason2Page = pageGetter.Load("http://thetvdb.com/?tab=season&seriesid=100981&seasonid=444001&lid=7");
			tableList = tvDbSeason2Page.GetElementbyId("listtable").Descendants("tr").ToList();
			seasonNum = 2;
			DataScraper.Season season2 = new DataScraper.Season() { SeasonNumber = seasonNum, SeasonDate = DateTime.Parse("2011-03-20") };
			fileText += ds.SeasonInsertString(season2);
			for (int i = 1; i < tableList.Count; i++) //avoid headers at row 0
			{
				var rowData = tableList[i];
				DataScraper.Episode episode = new DataScraper.Episode();
				episode.EpisodeName = rowData.Descendants("a").ToList()[1].InnerText.Replace("'", "''");
				episode.SeasonId = seasonNum;
				episode.EpisodeNumber = i;
				episode.EpisodeDate = DateTime.Parse(rowData.Descendants("td").ToList()[2].InnerText);

				fileText += ds.EpisodeInsertString(episode);

				//season 2 products
				var productPage = pageGetter.Load("http://sharktanksuccess.blogspot.com/2012/10/shark-tank-season-2-all-episodes-33.html"); //use for partial product name completion
				var divList = productPage.DocumentNode.Descendants("div").ToList();
				foreach (var divElement in divList)
				{
					if (divElement.Attributes["class"] != null && divElement.Attributes["class"].Value == "post-body entry-content")
					{

						//Console.Out.WriteLine("Found content div");
						var episodeElementList = divElement.Descendants("ul").ToList();
						//fileText += ds.SeasonInsertString(1, new DateTime(2009, 8, 9));
						var episodeElement = episodeElementList[i - 1];
						//Console.Out.WriteLine("Episode " + episodeNum.ToString());
						//http://thetvdb.com/?tab=season&seriesid=100981&seasonid=83461&lid=7 possibly use for episode names and dates?
						//fileText += ds.EpisodeInsertString("",episodeNum,null,1);
						var productList = episodeElement.Descendants("li").ToList();
						foreach (var productElement in productList)
						{
							DataScraper.Product product = new DataScraper.Product() { EpisodeId = i };
							var linkProductList = productElement.Descendants("a").ToList();
							var linkProduct = linkProductList.Count == 0 ? null : linkProductList[0];
							if (productElement.InnerText != string.Empty)
							{
								if (linkProduct == null)
								{
									//Console.Out.WriteLine("   " + productElement.InnerText);
									string nameString = WebUtility.HtmlDecode(productElement.InnerText).Replace("'", "''");
									int dashPos = nameString.IndexOf("-");
									product.ProductName = WebUtility.HtmlDecode(productElement.InnerText).Replace("'", "''").Substring(0, dashPos - 1).Trim();
								}
								else
								{
									//Console.Out.WriteLine("   " + linkProduct.InnerText);
									product.ProductName = WebUtility.HtmlDecode(linkProduct.InnerText).Replace("'", "''").Trim();
								}
							}
							if (!string.IsNullOrEmpty(product.ProductName))
								fileText += ds.ProductInsertString(product);
						}
					}
				}
			}
			#endregion

			#region Season 3
			//season 3 season and episodes
			var tvDbSeason3Page = pageGetter.Load("http://thetvdb.com/?tab=season&seriesid=100981&seasonid=480445&lid=7");
			tableList = tvDbSeason3Page.GetElementbyId("listtable").Descendants("tr").ToList();
			seasonNum = 3;
			DataScraper.Season season3 = new DataScraper.Season() { SeasonNumber = seasonNum, SeasonDate = DateTime.Parse("2012-01-20") };
			fileText += ds.SeasonInsertString(season3);
			for (int i = 1; i < tableList.Count; i++) //avoid headers at row 0
			{
				var rowData = tableList[i];
				DataScraper.Episode episode = new DataScraper.Episode();
				episode.EpisodeName = rowData.Descendants("a").ToList()[1].InnerText.Replace("'", "''");
				episode.SeasonId = seasonNum;
				episode.EpisodeNumber = i;
				episode.EpisodeDate = DateTime.Parse(rowData.Descendants("td").ToList()[2].InnerText);

				fileText += ds.EpisodeInsertString(episode);

				//season 3 products
				var productPage = pageGetter.Load("http://sharktanksuccess.blogspot.com/2013/08/shark-tank-season-3.html"); //use for partial product name completion
				var divList = productPage.DocumentNode.Descendants("div").ToList();
				foreach (var divElement in divList)
				{
					if (divElement.Attributes["class"] != null && divElement.Attributes["class"].Value == "post-body entry-content")
					{

						//Console.Out.WriteLine("Found content div");
						var episodeElementList = divElement.Descendants("ul").ToList();
						//fileText += ds.SeasonInsertString(1, new DateTime(2009, 8, 9));
						var episodeElement = episodeElementList[i - 1];
						//Console.Out.WriteLine("Episode " + episodeNum.ToString());
						//http://thetvdb.com/?tab=season&seriesid=100981&seasonid=83461&lid=7 possibly use for episode names and dates?
						//fileText += ds.EpisodeInsertString("",episodeNum,null,1);
						var productList = episodeElement.Descendants("li").ToList();
						foreach (var productElement in productList)
						{
							DataScraper.Product product = new DataScraper.Product() { EpisodeId = i};
							var linkProductList = productElement.Descendants("a").ToList();
							var linkProduct = linkProductList.Count == 0 ? null : linkProductList[0];
							if (productElement.InnerText != string.Empty)
							{
								if (linkProduct == null)
								{
									//Console.Out.WriteLine("   " + productElement.InnerText);
									string nameString = WebUtility.HtmlDecode(productElement.InnerText).Replace("'", "''");
									int dashPos = nameString.IndexOf("-");
									if(dashPos != -1)
										product.ProductName = WebUtility.HtmlDecode(productElement.InnerText).Replace("'", "''").Substring(0, dashPos - 1).Trim();
								}
								else
								{
									//Console.Out.WriteLine("   " + linkProduct.InnerText);
									product.ProductName = WebUtility.HtmlDecode(linkProduct.InnerText).Replace("'", "''").Trim();
								}
							}
							if (!string.IsNullOrEmpty(product.ProductName))
								fileText += ds.ProductInsertString(product);
						}
					}
				}
			}
			#endregion

			#region Season 4
			//season 4 season and episodes
			/*var tvDbSeason4Page = pageGetter.Load("http://thetvdb.com/?tab=season&seriesid=100981&seasonid=497747&lid=7");
			tableList = tvDbSeason4Page.GetElementbyId("listtable").Descendants("tr").ToList();
			seasonNum = 4;
			DataScraper.Season season4 = new DataScraper.Season() { SeasonNumber = seasonNum, SeasonDate = DateTime.Parse("2012-09-14") };
			fileText += ds.SeasonInsertString(season4);
			for (int i = 1; i < tableList.Count; i++) //avoid headers at row 0
			{
				var rowData = tableList[i];
				DataScraper.Episode episode = new DataScraper.Episode();
				episode.EpisodeName = rowData.Descendants("a").ToList()[1].InnerText.Replace("'", "''");
				episode.SeasonId = seasonNum;
				episode.EpisodeNumber = i;
				episode.EpisodeDate = DateTime.Parse(rowData.Descendants("td").ToList()[2].InnerText);

				fileText += ds.EpisodeInsertString(episode);

				//season 4 products
				var productPage = pageGetter.Load("http://sharktanksuccess.blogspot.com/2012/10/shark-tank-season-4-all-episodes.html"); //use for partial product name completion
				var divList = productPage.DocumentNode.Descendants("div").ToList();
				foreach (var divElement in divList)
				{
					if (divElement.Attributes["class"] != null && divElement.Attributes["class"].Value == "post-body entry-content")
					{

						//Console.Out.WriteLine("Found content div");
						var episodeElementList = divElement.Descendants("ul").ToList();
						//fileText += ds.SeasonInsertString(1, new DateTime(2009, 8, 9));
						var episodeElement = episodeElementList[i - 1];
						//Console.Out.WriteLine("Episode " + episodeNum.ToString());
						//http://thetvdb.com/?tab=season&seriesid=100981&seasonid=83461&lid=7 possibly use for episode names and dates?
						//fileText += ds.EpisodeInsertString("",episodeNum,null,1);
						var productList = episodeElement.Descendants("li").ToList();
						foreach (var productElement in productList)
						{
							DataScraper.Product product = new DataScraper.Product() { EpisodeId = i };
							var linkProductList = productElement.Descendants("a").ToList();
							var linkProduct = linkProductList.Count == 0 ? null : linkProductList[0];
							if (productElement.InnerText != string.Empty)
							{
								if (linkProduct == null)
								{
									//Console.Out.WriteLine("   " + productElement.InnerText);
									string nameString = WebUtility.HtmlDecode(productElement.InnerText).Replace("'", "''");
									int dashPos = nameString.IndexOf("-");
									if (dashPos != -1)
										product.ProductName = WebUtility.HtmlDecode(productElement.InnerText).Replace("'", "''").Substring(0, dashPos - 1).Trim();
								}
								else
								{
									//Console.Out.WriteLine("   " + linkProduct.InnerText);
									product.ProductName = WebUtility.HtmlDecode(linkProduct.InnerText).Replace("'", "''").Trim();
								}
							}
							if (!string.IsNullOrEmpty(product.ProductName))
								fileText += ds.ProductInsertString(product);
						}
					}
				}
			}*/
			#endregion

			File.WriteAllText("C:\\temp\\output.sql",fileText);
			Console.WriteLine("Output sql insert script to C:\\temp\\output.sql.  Enter to close.");
			Console.ReadLine(); //pause until enter
		}
	}

	public class DataScraper
	{
		public DataScraper()
		{

		}

		public class Product
		{
			public string ProductName { get; set; }
			public string ProductDescription { get; set; }
			public int? ProductType { get; set; }
			public string ProductUrl { get; set; }
			public string PurchaseUrl { get; set; }
			public string ImageUrl { get; set; }
			public bool? WasFunded { get; set; }
			public int EpisodeId { get; set; }
		}

		public class Episode
		{
			public string EpisodeName { get; set; }
			public int EpisodeNumber { get; set; }
			public DateTime? EpisodeDate { get; set; }
			public int SeasonId { get; set; }
		}

		public class Season
		{
			public int SeasonNumber { get; set; }
			public DateTime? SeasonDate { get; set; }
		}

		public string ProductInsertString(Product p)
		{
			return ProductInsertString(p.ProductName, p.ProductDescription, p.ProductType, p.ProductUrl, p.PurchaseUrl, p.ImageUrl, p.WasFunded, p.EpisodeId);
		}

		private string ProductInsertString(string productName, string productDescription, int? productType, string productUrl, string purchaseUrl, string imageUrl, bool? wasFunded, int episodeId)
		{
			string result = string.Empty;
			result += "INSERT INTO Product (ProductName, ProductDescription, ProductType, ProductUrl, PurchaseUrl, ImageUrl, WasFunded, DateCreated, DateUpdated, EpisodeId) VALUES (" + Environment.NewLine;
			result += "'" + productName + "', " + Environment.NewLine;
			result += "'" + productDescription + "'" + ", " + Environment.NewLine;
			result += productType.HasValue ? productType.ToString() : "0" + "," + Environment.NewLine;
			result += "'" + productUrl + "'" + ", " + Environment.NewLine;
			result += "'" + purchaseUrl + "'" + ", " + Environment.NewLine;
			result += "'" + imageUrl + "'" + ", " + Environment.NewLine;
			result += wasFunded.HasValue ? (wasFunded.Value ? "1" : "0") : "null" + "," + Environment.NewLine;
			result += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ", " + Environment.NewLine;
			result += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ", " + Environment.NewLine;
			result += episodeId;
			result += ");";
			return result + Environment.NewLine + Environment.NewLine;
		}

		public string EpisodeInsertString(Episode e)
		{
			return EpisodeInsertString(e.EpisodeName, e.EpisodeNumber, e.EpisodeDate, e.SeasonId);
		}

		private string EpisodeInsertString(string episodeName, int episodeNumber, DateTime? episodeDate, int seasonId)
		{
			string result = string.Empty;
			result += "INSERT INTO Episode (EpisodeName, EpisodeNumber, EpisodeNumber, EpisodeDate, DateCreated, DateUpdated, SeasonId) VALUES (";
			result += "'" + episodeName + "'" + ", ";
			result += episodeNumber.ToString() + ", ";
			result += "'" + (episodeDate.HasValue ? episodeDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : "1900-01-01 11:59:59") + "'" + ", ";
			result += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ", ";
			result += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ", ";
			result += seasonId;
			result += ");";
			return result + Environment.NewLine + Environment.NewLine;
		}

		public string SeasonInsertString(Season s)
		{
			return SeasonInsertString(s.SeasonNumber, s.SeasonDate);
		}

		private string SeasonInsertString(int seasonNumber, DateTime? seasonDate)
		{
			string result = string.Empty;
			result += "INSERT INTO Season (SeasonNumber, SeasonDate, DateCreated, DateUpdated) VALUES (";
			result += seasonNumber.ToString() + ", ";
			result += "'" + (seasonDate.HasValue ? seasonDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : "1900-01-01 11:59:59") + "'" + ", ";
			result += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ", ";
			result += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			result += ");";
			return result + Environment.NewLine + Environment.NewLine;
		}
	}
}
