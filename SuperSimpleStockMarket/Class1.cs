using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;


namespace SuperSimpleStockMarket
{
    /// <summary>
    /// Stock type may be either Common or Preferred
    /// </summary>
    public enum StockType { Common, Preferred };

    /// <summary>
    /// A trade may be Buy or Sell
    /// </summary>
    public enum BuyOrSell { Buy, Sell };

    /// <summary>
    /// MarketStocks is a lookup for the stocks that are available in the market.
    /// The key is the stock symbol.
    /// </summary>
    public class MarketStocks : KeyedCollection<string, Stock>
    {
        protected override string GetKeyForItem(Stock stock)
        {
            // In this example, the key is the stock symbol.
            return stock.symbol;
        }
    }

    /// <summary>
    /// Representation of a Stock.
    /// </summary>
    public class Stock
    {
        public string symbol { get; set; }
        public StockType stockType { get; set; }
        public double parValue { get; set;  }
        public double lastDividend { get; set;  }
        public double? fixedDividend { get; set; }

        /// <summary>
        /// A Stock with no fixed dividend.
        /// </summary>
        /// <param name="symbol">The symbol for the stock</param>
        /// <param name="stockType">The type for the stock</param>
        /// <param name="parValue">The Par value in pence</param>
        /// <param name="lastDividend">The Last Dividend value in pence</param>
        public Stock(string symbol, StockType stockType, double parValue, double lastDividend)
        {
            if (stockType == StockType.Preferred)
                throw new Exception("Preferred stock must have a Fixed Dividend value");
            CheckParValue(parValue);
            CheckLastDividend(lastDividend);

            this.symbol = symbol;
            this.stockType = stockType;
            this.parValue = parValue;
            this.lastDividend = lastDividend;
            this.fixedDividend = null;
        }

        /// <summary>
        /// A Stock that includes a fixed dividend.
        /// </summary>
        /// <param name="symbol">The symbol for the stock</param>
        /// <param name="stockType">The type for the stock</param>
        /// <param name="parValue">The Par value in pence (must be greater than zero)</param>
        /// <param name="lastDividend">The Last Dividend value in pence (must not be negative)</param>
        /// <param name="fixedDividend">The Fixed Dividend percentage as a decimal, e.g. 0.02 to represent 2% (must be greater than 0 and at most 1)</param>
        public Stock(string symbol, StockType stockType, double parValue, double lastDividend, double fixedDividend)
        {
            CheckParValue(parValue);
            CheckLastDividend(lastDividend);
            CheckFixedDividend(fixedDividend);

            this.symbol = symbol;
            this.stockType = stockType;
            this.parValue = parValue;
            this.lastDividend = lastDividend;
            this.fixedDividend = fixedDividend;
        }

        /// <summary>
        /// Calculate the dividend yield for a given price
        /// </summary>
        /// <param name="price">The price in pence (must be greater than zero)</param>
        /// <returns>The dividend yield</returns>
        public double CalculateDividendYield(double price)
        {
            CheckPrice(price);

            double yield = 0.0;

            switch (this.stockType)
            {
                case StockType.Common:
                    yield = lastDividend / price;
                    break;
                case StockType.Preferred:
                    {
                        if (fixedDividend.HasValue)
                            yield = fixedDividend.Value * parValue / price;
                        else
                            throw new Exception("Fixed Dividend value must be set for Preferred stock.");
                    break;
                    }
                default:
                    throw new Exception("Stock type not recognised.");
            }

            return yield;
        }

        /// <summary>
        /// Calculate the P / E Ratio for a given price
        /// </summary>
        /// <param name="price">The price in pence (must be greater than zero)</param>
        /// <returns>The P / E Ratio</returns>
        public double CalculatePERatio(double price)
        {
            CheckPrice(price);

            double pERatio = 0.0;

            if (fixedDividend.HasValue)
                pERatio = price / fixedDividend.Value;
            else
                throw new Exception("Stock does not have a fixed dividend set therefore PE Ratio cannot be calculated.");

            return pERatio;
        }

        private void CheckPrice(double price)
        {
            if (price < 0.0)
                throw new Exception("Price must not be negative.");
            else if (price == 0.0)
                throw new Exception("Price must not be zero.");
        }

        private void CheckParValue(double parValue)
        {
            if (parValue <= 0.0)
                throw new Exception("Par Value must be greater than zero.");
        }

        private void CheckLastDividend(double lastDividend)
        {
            if (lastDividend < 0.0)
                throw new Exception("Last Dividend value must not be negative.");
        }

        private void CheckFixedDividend(double fixedDividend)
        {
            if (fixedDividend < 0.0)
                throw new Exception("Fixed Dividend value must be greater than zero.");
            else if (fixedDividend > 1.0)
                throw new Exception("Fixed Dividend value must not be greater than one.");
        }
    }

    /// <summary>
    /// Representation of a Trade
    /// </summary>
    public class Trade : IComparable
    {
        public DateTime dateTimeOfTrade { get; set; }
        public StockType stockType { get; set; }
        public int quantity { get; set; }
        public BuyOrSell buyOrSell { get; set; }
        public double price { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dateTimeOfTrade">The timestamp of the trade</param>
        /// <param name="stockType">The stock type traded</param>
        /// <param name="quantity">The number of stocks traded (must be greater than zero)</param>
        /// <param name="buyOrSell">Indicates whether the trade is Buy or Sell</param>
        /// <param name="price">The price of each stock in pence (must be greater than zero)</param>
        public Trade(DateTime dateTimeOfTrade, StockType stockType, int quantity, BuyOrSell buyOrSell, double price)
        {
            CheckQuantity(quantity);
            CheckPrice(price);

            this.dateTimeOfTrade = dateTimeOfTrade;
            this.stockType = stockType;
            this.quantity = quantity;
            this.buyOrSell = buyOrSell;
            this.price = price;
        }

        /// <summary>
        /// Trades are sortable by reverse date time stamp.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;

            Trade other = obj as Trade;
            if (other != null)
            {
                //make trades sortable by reverse date time order, by reversing what is returned by DateTime.CompareTo.
                //this can simply be done by changing the sign of the returned value.
                int dateTimeCompareValue = this.dateTimeOfTrade.CompareTo(other.dateTimeOfTrade);
                dateTimeCompareValue *= -1;
                return dateTimeCompareValue;
            }
            else
                throw new ArgumentException("Object is not a Trade");
        }

        private void CheckQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new Exception("Trade quantity must be greater than zero.");
        }

        private void CheckPrice(double price)
        {
            if (price <= 0.0)
                throw new Exception("Trade price must be greater than zero.");
        }
    }

    /// <summary>
    /// Representation of a super simple stock market
    /// </summary>
    public class StockMarket
    {
        public MarketStocks marketStocks;
        public List<Trade> trades;

        public StockMarket()
        {
            marketStocks = new MarketStocks();
            trades = new List<Trade>();
        }

        public bool AddStock(Stock stock)
        {
            try
            {
                marketStocks.Add(stock);
                return true;
            }
            catch
            {
                //I am catching whatever is thrown because I don't want the market to be brought down by a bad stock.
                return false;
            }
        }

        /// <summary>
        /// Make a trade
        /// </summary>
        /// <param name="stockType">Type of stock to be traded (this must exist in the market)</param>
        /// <param name="shareQuantity">Number of stocks to be traded (must be greater than zero)</param>
        /// <param name="buyOrSell">Buy or Sell</param>
        /// <param name="tradedPrice">The price in pence per stock (must be greater than zero)</param>
        /// <returns></returns>
        public bool TradeSomeStock(string stockSymbol, int shareQuantity, BuyOrSell buyOrSell, double tradedPrice)
        {
            bool success = false;

            //check the stock symbol exists
            if (marketStocks.Contains(stockSymbol))
            {
                //record the trade by taking a timestamp, recording it in the trade object and adding it to the list of trades.
                try
                {
                    Trade trade = new Trade(DateTime.Now, marketStocks[stockSymbol].stockType, shareQuantity, buyOrSell, tradedPrice);
                    if (trade != null)
                    {
                        trades.Add(trade);
                        success = true;
                    }
                }
                catch
                {
                    //I am catching whatever is thrown because I don't want the market to be brought down by a bad trade.
                    success = false;
                }
            }

            return success;
        }

        /// <summary>
        /// Number of individual stocks contained in the market
        /// </summary>
        /// <returns>Number of stocks</returns>
        public int StockCount()
        {
            return marketStocks.Count();
        }

        /// <summary>
        /// Number of individual trades ever done in the market
        /// </summary>
        /// <returns>Number of trades</returns>
        public int TradeCount()
        {
            return trades.Count();
        }

        /// <summary>
        /// Calculate the Volume Weighted Stock Price for a given stock based on trades in the last 15 minutes
        /// </summary>
        /// <param name="stockType">The type of stock to calculate the Volume Weighted Stock Price for (this must exist in the market)</param>
        /// <returns>The Volume Weighted Stock Price in pence</returns>
        public double CalculateVolumeWeightedStockPrice(StockType stockType)
        {
            double volumeWeightedStockPrice = 0.0;

            //calculation can only be done if trades have been done in the stock market
            if (trades.Count == 0)
            {
                throw new Exception("Volume Weighted Stock Price can only be calculated when one or more trades have occurred in the stock market");
            }

            //take a timestamp for when 15 minutes ago was, so we know which trades to include
            DateTime fifteenMinutesAgo = DateTime.Now.Subtract(new TimeSpan(hours: 0, minutes: 15, seconds: 0));

            //sort the list (it is not guaranteed to be sorted).  Trades are sorted in reverse timestamp order.
            trades.Sort();

            //run through the list
            double numerator = 0.0;
            double denominator = 0.0;
            foreach (Trade stockTrade in trades)
            {
                //once the timestamp gets older than fifteen minutes ago we can stop calculating.
                if (stockTrade.dateTimeOfTrade < fifteenMinutesAgo)
                    break;
                if (stockTrade.stockType == stockType)
                {
                    numerator += stockTrade.price * stockTrade.quantity;
                    denominator += stockTrade.quantity;
                }
            }
            if (denominator == 0.0)
            {
                //the denominator only remains zero if no trades of the given stock type were identified in the last 15 minutes
                throw new Exception("Volume Weighted Stock Price can only be calculated for stocks of the given type that have been traded in the last 15 minutes");
            }
            volumeWeightedStockPrice = numerator / denominator;

            return volumeWeightedStockPrice;
        }

        /// <summary>
        /// Calculate the GBCE All Share Index for the market
        /// </summary>
        /// <returns>The GBCE All Share Index</returns>
        public double CalculateGBCEAllShareIndex()
        {
            double gBCEAllShareIndex = 0.0;

            //calculation can only be done if trades have been done in the stock market
            if (trades.Count == 0)
            {
                throw new Exception("GCBE All Share Index can only be calculated when one or more trades have occurred in the stock market");
            }

            //calculate the geometric mean: order is unimportant so simply run through the list of trades.
            double productOfPrices = 1.0;
            foreach (Trade stockTrade in trades)
            {
                //For a relatively small number of trades, the productOfPrices will get very large.  This could effect the accuracy of the calculation, or for enough trades overflow double (*10^308)
                //To mitigate, think about whether there is a library method for calculating the geometric mean.
                productOfPrices *= stockTrade.price;
            }
            gBCEAllShareIndex = Math.Pow(productOfPrices, 1.0/trades.Count());//implicit cast int to double OK

            return gBCEAllShareIndex;
        }
    }
}
