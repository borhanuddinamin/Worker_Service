using CSEData.Worker.Model;
using HtmlAgilityPack;
using Persistent.Entity;

namespace CSEData.Worker.MarketScrap
{
    public class ScrapData
    {
        public  ScrapData()
        {

         }

        public static HtmlDocument WebDocument()
        {
            var html = "https://www.cse.com.bd/market/current_price";
            HtmlWeb web = new HtmlWeb();
            var webDoc = web.Load(html);

            return webDoc;

        }
        public static bool IsMarketOpen()
        {
            var webDoc = WebDocument();
            bool result = false;
            var WebClassTag =webDoc.DocumentNode.SelectNodes($"//*[@class='market_status']");
          
            foreach(var data in WebClassTag)
            {
              var   Isopen = data.SelectSingleNode(".//span");
                if(Isopen.InnerText == "Open")
                {
                    result= true;
                    break;
                }
            }
            return result;

        }


        public static async Task<List<MarketModel>>  MarketData()
        {
            
            List<MarketModel> marketModels = new List<MarketModel>();
            int z = 0;
            var webDoc = WebDocument();
            var table = webDoc.DocumentNode.SelectSingleNode("//table");
            var tableRows = table.SelectNodes(".//tr");
            
            foreach (var row in tableRows)
            {
                if (z == 0)
                {
                    z++;
                    continue;
                }
                MarketModel marketModel = new MarketModel();
               var rowCells = row.SelectNodes(".//td");
                if(rowCells!=null)
               
                for (int i=1; i<6;i++)
                {
                        

                        
                         if (i == 1)
                        {
                            marketModel.StockCodeName = rowCells[i].InnerText;
                        }
                        else
                        {
                            
                            if (i == 2)
                            {
                                marketModel.LtpPrice = decimal.Parse(rowCells[i].InnerText);
                                
                            }
                            else if (i == 3)
                            {
                                marketModel.Open = decimal.Parse(rowCells[i].InnerText);
                                
                            }
                            else if (i == 4)
                            {
                                marketModel.High = decimal.Parse(rowCells[i].InnerText);
                                
                            }
                            else if (i == 5)
                            {
                                marketModel.Low = decimal.Parse(rowCells[i].InnerText);
                                
                            }
                        }
                    }
                marketModels.Add(marketModel);
            }

            return marketModels;
        }
       

    }
}
