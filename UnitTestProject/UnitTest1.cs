using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuperSimpleStockMarket;

namespace UnitTestProject
{
    [TestClass]
    public class MarketStocksTests
    {
        /// <summary>
        /// Add a stock which does not have a fixed dividend and check that it exists, and all its attributes are correct.
        /// </summary>
        [TestMethod]
        public void AddNoFixedDividend()
        {
            string msg = "";
            string stockSymbol = "TEA";
            StockType stockType = StockType.Common;
            double parValue = 100.0;
            double lastDividend = 0.0;

            MarketStocks stockCollection = new MarketStocks();
            try
            {
                stockCollection.Add(new Stock(stockSymbol, stockType, parValue, lastDividend));
            }
            catch (Exception e)
            {
                msg = e.Message;
            }

            Assert.AreEqual(1, stockCollection.Count, "Error message caught: " + msg);
            Assert.IsTrue(stockCollection.Contains(stockSymbol), "Stock Collection does not contain " + stockSymbol);
            Assert.AreEqual(stockType, stockCollection[stockSymbol].stockType, "Stock Type does not match");
            Assert.AreEqual(parValue, stockCollection[stockSymbol].parValue, "Par Value does not match");
            Assert.AreEqual(lastDividend, stockCollection[stockSymbol].lastDividend, "Last Dividend does not match");
        }

        /// <summary>
        /// Add a stock which includes a fixed dividend, and check that it exists, and all its attributes are correct.
        /// </summary>
        [TestMethod]
        public void AddWithFixedDividend()
        {
            string msg = "";
            string stockSymbol = "GIN";
            StockType stockType = StockType.Preferred;
            double parValue = 100.0;
            double lastDividend = 8.0;
            double fixedDividend = 0.02;

            MarketStocks stockCollection = new MarketStocks();
            try
            {
                stockCollection.Add(new Stock(stockSymbol, stockType, parValue, lastDividend, fixedDividend));
            }
            catch (Exception e)
            {
                msg = e.Message;
            }

            Assert.AreEqual(1, stockCollection.Count, "Error message caught: " + msg);
            Assert.IsTrue(stockCollection.Contains(stockSymbol), "Stock Collection does not contain " + stockSymbol);
            Assert.AreEqual(stockType, stockCollection[stockSymbol].stockType, "Stock Type does not match");
            Assert.AreEqual(parValue, stockCollection[stockSymbol].parValue, "Par Value does not match");
            Assert.AreEqual(lastDividend, stockCollection[stockSymbol].lastDividend, "Last Dividend does not match");
            Assert.AreEqual(fixedDividend, stockCollection[stockSymbol].fixedDividend, "Fixed Dividend does not match");
        }

    }

    [TestClass]
    public class StockTests
    {
        [TestMethod]
        public void CreateStockNoFixedDividendOK()
        {
            string msg = "";
            string stockSymbol = "TEA";
            StockType stockType = StockType.Common;
            double parValue = 100.0;
            double lastDividend = 0.0;

            try
            {
                Stock stock = new Stock(stockSymbol, stockType, parValue, lastDividend);

                Assert.AreNotEqual(null, stock);
            }
            catch (Exception e)
            {
                msg = e.Message;
                Assert.Fail("Create Stock failed with message " + msg);
            }
        }

        [TestMethod]
        public void CreateStockBadParValueNoFixedDividend()
        {
            string msg = "";
            string stockSymbol = "TEA";
            StockType stockType = StockType.Common;
            double parValue = 0.0;
            double lastDividend = 0.0;

            try
            {
                Stock stock = new Stock(stockSymbol, stockType, parValue, lastDividend);
                //the above code should throw an error therefore should never get to the next statement:
                Assert.Fail("Stock was created with bad Par Value " + parValue);
            }
            catch (Exception e)
            {
                msg = e.Message;
                Assert.AreNotEqual("", msg, "Exception correctly thrown, message was " + msg);
            }
        }

        [TestMethod]
        public void CreateStockBadLastDividendNoFixedDividend()
        {
            string msg = "";
            string stockSymbol = "TEA";
            StockType stockType = StockType.Common;
            double parValue = 100.0;
            double lastDividend = -10.0;

            try
            {
                Stock stock = new Stock(stockSymbol, stockType, parValue, lastDividend);
                //the above code should throw an error therefore should never get to the next statement:
                Assert.Fail("Stock was created with bad Last Dividend " + lastDividend);
            }
            catch (Exception e)
            {
                msg = e.Message;
                Assert.AreNotEqual("", msg, "Exception correctly thrown, message was " + msg);
            }
        }

        [TestMethod]
        public void CreatePreferredStockNoFixedDividend()
        {
            string msg = "";
            string stockSymbol = "TEA";
            StockType stockType = StockType.Preferred;
            double parValue = 100.0;
            double lastDividend = 0.0;

            try
            {
                Stock stock = new Stock(stockSymbol, stockType, parValue, lastDividend);
                //the above code should throw an error therefore should never get to the next statement:
                Assert.Fail("Stock was incorrectly created as Preferred but with no Fixed Divident value.");
            }
            catch (Exception e)
            {
                msg = e.Message;
                Assert.AreNotEqual("", msg, "Exception correctly thrown, message was " + msg);
            }
        }

        [TestMethod]
        public void CreateStockBadParValueWithFixedDividend()
        {
            string msg = "";
            string stockSymbol = "GIN";
            StockType stockType = StockType.Preferred;
            double parValue = 0.0;
            double lastDividend = 8.0;
            double fixedDividend = 0.02;

            try
            {
                Stock stock = new Stock(stockSymbol, stockType, parValue, lastDividend, fixedDividend);
                //the above code should throw an error therefore should never get to the next statement:
                Assert.Fail("Stock was created with bad Par Value " + parValue);
            }
            catch (Exception e)
            {
                msg = e.Message;
                Assert.AreNotEqual("", msg, "Exception correctly thrown, message was " + msg);
            }
        }

        [TestMethod]
        public void CreateStockBadLastDividendWithFixedDividend()
        {
            string msg = "";
            string stockSymbol = "GIN";
            StockType stockType = StockType.Preferred;
            double parValue = 100.0;
            double lastDividend = -10.0;
            double fixedDividend = 0.02;

            try
            {
                Stock stock = new Stock(stockSymbol, stockType, parValue, lastDividend, fixedDividend);
                //the above code should throw an error therefore should never get to the next statement:
                Assert.Fail("Stock was created with bad Last Dividend " + lastDividend);
            }
            catch (Exception e)
            {
                msg = e.Message;
                Assert.AreNotEqual("", msg, "Exception correctly thrown, message was " + msg);
            }
        }

        [TestMethod]
        public void CreateStockBadFixedDividendWithFixedDividend()
        {
            string msg = "";
            string stockSymbol = "GIN";
            StockType stockType = StockType.Preferred;
            double parValue = 100.0;
            double lastDividend = 8.0;
            double fixedDividend = 0.0;

            try
            {
                Stock stock = new Stock(stockSymbol, stockType, parValue, lastDividend, fixedDividend);
                //the above code should throw an error therefore should never get to the next statement:
                Assert.Fail("Stock was created with bad Fixed Dividend " + fixedDividend);
            }
            catch (Exception e)
            {
                msg = e.Message;
                Assert.AreNotEqual("", msg, "Exception correctly thrown, message was " + msg);
            }
        }

        [TestMethod]
        public void TestCalculateDividendYieldBadPrice()
        {
            string msg = "";
            string stockSymbol = "GIN";
            StockType stockType = StockType.Preferred;
            double parValue = 100.0;
            double lastDividend = 8.0;
            double fixedDividend = 0.02;
            double price = 0.0;
            double dividendYield = 0.0;

            try
            {
                Stock stock = new Stock(stockSymbol, stockType, parValue, lastDividend, fixedDividend);
                dividendYield = stock.CalculateDividendYield(price);
                //the above code should throw an error therefore should never get to the next statement:
                Assert.Fail("Dividend Yield was calculated with bad price " + price);
            }
            catch (Exception e)
            {
                msg = e.Message;
                Assert.AreNotEqual("", msg, "Exception correctly thrown, message was " + msg);
            }
        }

        [TestMethod]
        public void TestCalculateDividendYieldCommon()
        {
            string msg = "";
            string stockSymbol = "POP";
            StockType stockType = StockType.Common;
            double parValue = 100.0;
            double lastDividend = 8.0;
            double price = 40.0;
            double dividendYield = -1.0;

            try
            {
                Stock stock = new Stock(stockSymbol, stockType, parValue, lastDividend);
                dividendYield = stock.CalculateDividendYield(price);

                //hand calc for common stock
                double expectedDividendYield = lastDividend / price;
                Assert.AreEqual(expectedDividendYield, dividendYield);
            }
            catch (Exception e)
            {
                msg = e.Message;
                Assert.Fail("Exception thrown, message was " + msg);
            }
        }

        [TestMethod]
        public void TestCalculateDividendYieldPreferred()
        {
            string msg = "";
            string stockSymbol = "GIN";
            StockType stockType = StockType.Preferred;
            double parValue = 100.0;
            double lastDividend = 8.0;
            double fixedDividend = 0.02;
            double price = 40.0;
            double dividendYield = -1.0;

            try
            {
                Stock stock = new Stock(stockSymbol, stockType, parValue, lastDividend, fixedDividend);
                dividendYield = stock.CalculateDividendYield(price);

                //hand calc for preferred stock
                double expectedDividendYield = fixedDividend * parValue / price;
                Assert.AreEqual(expectedDividendYield, dividendYield);
            }
            catch (Exception e)
            {
                msg = e.Message;
                Assert.Fail("Exception thrown, message was " + msg);//not handled...
            }
        }
    }

    [TestClass]
    public class TradeTests
    {
        [TestMethod]
        public void TestCreateTradeOK()
        {
            DateTime dateTimeOfTrade = DateTime.Now;
            StockType stockType = StockType.Common;
            int quantity = 1;
            BuyOrSell buyOrSell = BuyOrSell.Buy;
            double price = 40.0;
            string msg = "";

            try
            {
                Trade trade = new Trade(dateTimeOfTrade, stockType, quantity, buyOrSell, price);
            }
            catch (Exception e)
            {
                msg = e.Message;
                Assert.Fail("Exception thrown, message is " + msg);
            }
        }

        [TestMethod]
        public void TestTradeCompareToIsReverseOfDateTime()
        {
            DateTime dateTimeOfTradeEarly = DateTime.Now;
            DateTime dateTimeOfTradeLate = dateTimeOfTradeEarly + new TimeSpan(hours:0, minutes:1, seconds:0);
            StockType stockType = StockType.Common;
            int quantity = 1;
            BuyOrSell buyOrSell = BuyOrSell.Buy;
            double price = 40.0;
            string msg = "";

            try
            {
                Trade tradeEarly = new Trade(dateTimeOfTradeEarly, stockType, quantity, buyOrSell, price);
                Trade tradeLate = new Trade(dateTimeOfTradeLate, stockType, quantity, buyOrSell, price);
                int tradeCompare = tradeEarly.CompareTo(tradeLate);
                int dateTimeCompare = dateTimeOfTradeEarly.CompareTo(dateTimeOfTradeLate);
                //I have coded Trade.CompareTo to be the negative of dateTime.CompareTo:
                Assert.AreEqual(-1 * dateTimeCompare, tradeCompare, "Trade CompareTo does not return the reverse of DateTime.CompereTo.");
            }
            catch (Exception e)
            {
                msg = e.Message;
                Assert.Fail("Exception thrown, message is " + msg);
            }
        }

        [TestMethod]
        public void TestCreateTradeQuantityZero()
        {
            DateTime dateTimeOfTrade = DateTime.Now;
            StockType stockType = StockType.Common;
            int quantity = 0;
            BuyOrSell buyOrSell = BuyOrSell.Buy;
            double price = 40.0;
            string msg = "";

            try
            {
                Trade trade = new Trade(dateTimeOfTrade, stockType, quantity, buyOrSell, price);
            }
            catch (Exception e)
            {
                msg = e.Message;
            }
            Assert.AreEqual("Trade quantity must be greater than zero.", msg);
        }

        [TestMethod]
        public void TestCreateTradePriceZero()
        {
            DateTime dateTimeOfTrade = DateTime.Now;
            StockType stockType = StockType.Common;
            int quantity = 10;
            BuyOrSell buyOrSell = BuyOrSell.Buy;
            double price = 0.0;
            string msg = "";

            try
            {
                Trade trade = new Trade(dateTimeOfTrade, stockType, quantity, buyOrSell, price);
            }
            catch (Exception e)
            {
                msg = e.Message;
            }
            Assert.AreEqual("Trade price must be greater than zero.", msg);
        }        
    }

    [TestClass]
    public class StockMarketTests
    {
        [TestMethod]
        public void CreateStockMarketOK()
        {
            try
            {
                StockMarket stockMarket = new StockMarket();
            }
            catch (Exception e)
            {
                Assert.Fail("Exception thrown with message " + e.Message);
            }

        }

        [TestMethod]
        public void TestAddAStockOK()
        {
            try
            {
                StockMarket stockMarket = new StockMarket();

                string stockSymbol = "TEA";
                StockType stockType = StockType.Common;
                double parValue = 100.0;
                double lastDividend = 0.0;

                Stock stockTEA = new Stock(stockSymbol, stockType, parValue, lastDividend);

                bool success = stockMarket.AddStock(stockTEA);
                Assert.IsTrue(success, "Adding stock to market was unsuccessful");
            }
            catch (Exception e)
            {
                Assert.Fail("Exception thrown with message " + e.Message);
            }

        }

        [TestMethod]
        public void TestTradeSomeStockOK()
        {
            try
            {
                StockMarket stockMarket = new StockMarket();

                string stockSymbol = "TEA";
                StockType stockType = StockType.Common;
                double parValue = 100.0;
                double lastDividend = 0.0;

                Stock stockTEA = new Stock(stockSymbol, stockType, parValue, lastDividend);

                bool success = stockMarket.AddStock(stockTEA);
                Assert.IsTrue(success, "Adding stock to market was unsuccessful");

                int shareQuantity = 1;
                BuyOrSell buyOrSell = BuyOrSell.Buy;
                double tradedPrice = 40.0;

                success = stockMarket.TradeSomeStock(stockSymbol, shareQuantity, buyOrSell, tradedPrice);

                Assert.IsTrue(success, "Trading stock was unsuccessful");
                Assert.AreEqual(1, stockMarket.TradeCount(), "Trade count was " + stockMarket.TradeCount().ToString());
            }
            catch (Exception e)
            {
                Assert.Fail("Exception thrown with message " + e.Message);
            }
        }

        [TestMethod]
        public void TestTradeSomeStockSymbolNotInMarket()
        {
            try
            {
                StockMarket stockMarket = new StockMarket();

                string stockSymbol = "TEA";
                StockType stockType = StockType.Common;
                double parValue = 100.0;
                double lastDividend = 0.0;

                Stock stockTEA = new Stock(stockSymbol, stockType, parValue, lastDividend);

                bool success = stockMarket.AddStock(stockTEA);
                Assert.IsTrue(success, "Adding stock to market was unsuccessful");

                int shareQuantity = 1;
                BuyOrSell buyOrSell = BuyOrSell.Buy;
                double tradedPrice = 40.0;
                string stockSymbolNotInMarket = "BOB";

                success = stockMarket.TradeSomeStock(stockSymbolNotInMarket, shareQuantity, buyOrSell, tradedPrice);

                Assert.IsFalse(success, "Trading stock was successful but should not have been because the stock did not exist in the market");
                Assert.AreEqual(0, stockMarket.TradeCount(), "Trade count was " + stockMarket.TradeCount().ToString());
            }
            catch (Exception e)
            {
                Assert.Fail("Exception thrown with message " + e.Message);
            }
        }

        [TestMethod]
        public void TestCalculateVolumeWeightedStockPriceCommonOK()
        {
            //happily the standard test data contains multiple Common stocks so this also covers the test case of multiple trades
            StockMarket stockMarket = new StockMarket();

            SetUpStockMarketWithStocks(stockMarket);
            Assert.IsTrue(stockMarket.StockCount() > 0, "Stock market contains zero stocks.");

            SetUpStockMarketWithTrades(stockMarket);
            Assert.IsTrue(stockMarket.TradeCount() > 0, "Stock market contains zero trades.");

            double result = 0.0;
            try
            {
                result = stockMarket.CalculateVolumeWeightedStockPrice(StockType.Common);
            }
            catch (Exception e)
            {
                Assert.Fail("Exception thrown with message " + e.Message);
            }

            //Standard data:
            //the number of traded shares per Common stock (TEA=1, POP=2, ALE=3, JOE=5) in last 15 minutes
            //priced at 10, 20, 30, 50 pence.
            //Calculation is sum(price*quantity)/sum(quantity).
            //Brackets not required due to compiler implementation of basic mathematics rules for natural numbers:
            double numerator = 10.0 * 1 + 20.0 * 2 + 30.0 * 3 + 50.0 * 5;
            double denominator = 1 + 2 + 3 + 5;
            double expected = numerator / denominator;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestCalculateVolumeWeightedStockPricePreferredOK()
        {
            //happily the standard test data only contains one Preferred stock so this also covers the test case of a single trade
            StockMarket stockMarket = new StockMarket();

            SetUpStockMarketWithStocks(stockMarket);
            Assert.IsTrue(stockMarket.StockCount() > 0, "Stock market contains zero stocks.");

            SetUpStockMarketWithTrades(stockMarket);
            Assert.IsTrue(stockMarket.TradeCount() > 0, "Stock market contains zero trades.");

            double result = 0.0;
            try
            {
                result = stockMarket.CalculateVolumeWeightedStockPrice(StockType.Preferred);
            }
            catch (Exception e)
            {
                Assert.Fail("Exception thrown with message " + e.Message);
            }

            //Standard data:
            //4 traded shares for the single Preferred stock in last 15 minutes
            //priced at 40 pence.
            //Calculation is sum(price*quantity)/sum(quantity).
            double numerator = 40.0 * 4;
            double denominator = 4;
            double expected = numerator / denominator;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestCalculateVolumeWeightedStockPriceTradesAllOver15MinutesAgo()
        {
            StockMarket stockMarket = new StockMarket();

            SetUpStockMarketWithStocks(stockMarket);
            Assert.IsTrue(stockMarket.StockCount() > 0, "Stock market contains zero stocks.");

            SetUpStockMarketWithTrades(stockMarket);
            Assert.IsTrue(stockMarket.TradeCount() > 0, "Stock market contains zero trades.");

            //wait 15 minutes.  There will be a better way of testing this but I have not yet investigated!
            System.Threading.Thread.Sleep(1000 * 60 * 15);

            string msg = "";
            try
            {
                double result = stockMarket.CalculateVolumeWeightedStockPrice(StockType.Common);
            }
            catch (Exception e)
            {
                msg = e.Message;
            }

            Assert.AreEqual("Volume Weighted Stock Price can only be calculated for stocks of the given type that have been traded in the last 15 minutes", msg);
        }

        [TestMethod]
        public void TestCalculateVolumeWeightedStockPriceTradesSpanning15MinutesAgo()
        {
            //happily the standard test data contains multiple Common stocks so this also covers the test case of multiple trades
            StockMarket stockMarket = new StockMarket();

            SetUpStockMarketWithStocks(stockMarket);
            Assert.IsTrue(stockMarket.StockCount() > 0, "Stock market contains zero stocks.");

            //add a trade that won't be part of the calculation
            string stockSymbolPOP = "POP";
            int shareQuantity = 6;
            double tradedPrice = 60.0;
            BuyOrSell buyOrSell = BuyOrSell.Sell;
            bool success = stockMarket.TradeSomeStock(stockSymbolPOP, shareQuantity, buyOrSell, tradedPrice);

            //now wait 15 minutes
            System.Threading.Thread.Sleep(1000 * 60 * 15);

            //and add trades that will be included in the calculation
            SetUpStockMarketWithTrades(stockMarket);
            Assert.IsTrue(stockMarket.TradeCount() > 0, "Stock market contains zero trades.");

            double result = 0.0;
            try
            {
                result = stockMarket.CalculateVolumeWeightedStockPrice(StockType.Common);
            }
            catch (Exception e)
            {
                Assert.Fail("Exception thrown with message " + e.Message);
            }

            //Standard data:
            //the number of traded shares per Common stock (TEA=1, POP=2, ALE=3, JOE=5) in last 15 minutes
            //priced at 10, 20, 30, 50 pence.
            //Calculation is sum(price*quantity)/sum(quantity).
            //Brackets not required due to compiler implementation of basic mathematics rules for natural numbers:
            double numerator = 10.0 * 1 + 20.0 * 2 + 30.0 * 3 + 50.0 * 5;
            double denominator = 1 + 2 + 3 + 5;
            double expected = numerator / denominator;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestCalculateVolumeWeightedStockPriceCommonNoTrades()
        {
            StockMarket stockMarket = new StockMarket();

            string msg = "";
            try
            {
                double result = stockMarket.CalculateVolumeWeightedStockPrice(StockType.Common);
            }
            catch (Exception e)
            {
                msg = e.Message;
            }

            Assert.AreEqual("Volume Weighted Stock Price can only be calculated when one or more trades have occurred in the stock market", msg);
        }

        [TestMethod]
        public void TestCalculateVolumeWeightedStockPriceNoTradesOfGivenStockType()
        {
            StockMarket stockMarket = new StockMarket();

            SetUpStockMarketWithStocks(stockMarket);
            Assert.IsTrue(stockMarket.StockCount() > 0, "Stock market contains zero stocks.");

            //add two Common trades

            string stockSymbolTEA = "TEA";
            int shareQuantity = 1;
            BuyOrSell buyOrSell = BuyOrSell.Buy;
            double tradedPrice = 10.0;
            bool success = stockMarket.TradeSomeStock(stockSymbolTEA, shareQuantity, buyOrSell, tradedPrice);

            System.Threading.Thread.Sleep(1000);

            string stockSymbolPOP = "POP";
            shareQuantity = 2;
            tradedPrice = 20.0;
            success = stockMarket.TradeSomeStock(stockSymbolPOP, shareQuantity, buyOrSell, tradedPrice);

            string msg = "";
            try
            {
                double result = stockMarket.CalculateVolumeWeightedStockPrice(StockType.Preferred);
            }
            catch (Exception e)
            {
                msg = e.Message;
            }

            Assert.AreEqual("Volume Weighted Stock Price can only be calculated for stocks of the given type that have been traded in the last 15 minutes", msg);
        }

        [TestMethod]
        public void TestCalculateVolumeWeightedStockPriceNoTradesInLast15Minutes()
        {
            StockMarket stockMarket = new StockMarket();

            SetUpStockMarketWithStocks(stockMarket);
            Assert.IsTrue(stockMarket.StockCount() > 0, "Stock market contains zero stocks.");

            SetUpStockMarketWithTrades(stockMarket);
            Assert.IsTrue(stockMarket.TradeCount() > 0, "Stock market contains zero trades.");

            //wait 15 minutes.  There will be a better way of testing this but I have not yet investigated!
            System.Threading.Thread.Sleep(1000 * 60 * 15);

            string msg = "";
            try
            {
                double result = stockMarket.CalculateVolumeWeightedStockPrice(StockType.Common);
            }
            catch (Exception e)
            {
                msg = e.Message;
            }

            Assert.AreEqual("Volume Weighted Stock Price can only be calculated for stocks of the given type that have been traded in the last 15 minutes", msg);
        }

        [TestMethod]
        public void TestCalculateGBCEAllShareIndexOK()
        {
            StockMarket stockMarket = new StockMarket();

            SetUpStockMarketWithStocks(stockMarket);
            Assert.IsTrue(stockMarket.StockCount() > 0, "Stock market contains zero stocks.");

            SetUpStockMarketWithTrades(stockMarket);
            Assert.IsTrue(stockMarket.TradeCount() > 0, "Stock market contains zero trades.");

            double result = 0.0;
            try
            {
                result = stockMarket.CalculateGBCEAllShareIndex();
            }
            catch (Exception e)
            {
                Assert.Fail("Exception thrown with message " + e.Message);
            }

            //Standard data:
            //prices of the 5 trades are 10, 20, 30, 40, and 50 pence.
            //Calculation is the nth root of the product of all prices, where n is the number of trades
            //nth root is the same as raising to the power 1/n
            double product = 10.0 * 20.0 * 30.0 * 40.0 * 50.0;
            double expected = Math.Pow(product, 0.2);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestCalculateGBCEAllShareIndexSingleTrade()
        {
            StockMarket stockMarket = new StockMarket();

            SetUpStockMarketWithStocks(stockMarket);
            Assert.IsTrue(stockMarket.StockCount() > 0, "Stock market contains zero stocks.");

            //add a single Common trade

            string stockSymbolTEA = "TEA";
            int shareQuantity = 1;
            BuyOrSell buyOrSell = BuyOrSell.Buy;
            double tradedPrice = 10.0;
            bool success = stockMarket.TradeSomeStock(stockSymbolTEA, shareQuantity, buyOrSell, tradedPrice);

            double result = 0.0;
            try
            {
                result = stockMarket.CalculateGBCEAllShareIndex();
            }
            catch (Exception e)
            {
                Assert.Fail("Exception thrown with message " + e.Message);
            }

            //Standard data:
            //price of the single trade is 10 pence, and we take the 1st root
            //the result of this calculation is simply the single traded price!

            Assert.AreEqual(tradedPrice, result);
        }

        [TestMethod]
        public void TestCalculateGBCEAllShareIndexNoTrades()
        {
            StockMarket stockMarket = new StockMarket();

            string msg = "";
            try
            {
                double result = stockMarket.CalculateGBCEAllShareIndex();
            }
            catch (Exception e)
            {
                msg = e.Message;
            }

            Assert.AreEqual("GCBE All Share Index can only be calculated when one or more trades have occurred in the stock market", msg);
        }

        /// <summary>
        /// method to set up the stock market with some stocks, for use within test methods
        /// </summary>
        public void SetUpStockMarketWithStocks(StockMarket stockMarket)
        {
            try
            {
                //add all the stocks

                string stockSymbolTEA = "TEA";
                StockType stockType = StockType.Common;
                double parValue = 100.0;
                double lastDividend = 0.0;
                Stock stockTEA = new Stock(stockSymbolTEA, stockType, parValue, lastDividend);
                bool success = stockMarket.AddStock(stockTEA);

                string stockSymbolPOP = "POP";
                stockType = StockType.Common;
                parValue = 100.0;
                lastDividend = 8.0;
                Stock stockPOP = new Stock(stockSymbolPOP, stockType, parValue, lastDividend);
                success = stockMarket.AddStock(stockPOP);

                string stockSymbolALE = "ALE";
                stockType = StockType.Common;
                parValue = 60.0;
                lastDividend = 23.0;
                Stock stockALE = new Stock(stockSymbolALE, stockType, parValue, lastDividend);
                success = stockMarket.AddStock(stockALE);

                string stockSymbolGIN = "GIN";
                stockType = StockType.Preferred;
                parValue = 100.0;
                lastDividend = 8.0;
                double fixedDividend = 0.02;
                Stock stockGIN = new Stock(stockSymbolGIN, stockType, parValue, lastDividend, fixedDividend);
                success = stockMarket.AddStock(stockGIN);

                string stockSymbolJOE = "JOE";
                stockType = StockType.Common;
                parValue = 250.0;
                lastDividend = 13.0;
                Stock stockJOE = new Stock(stockSymbolJOE, stockType, parValue, lastDividend);
                success = stockMarket.AddStock(stockJOE);

            }
            catch (Exception e)
            {
                Assert.Fail("Exception thrown with message " + e.Message);
            }
        }

        /// <summary>
        /// method to set up the stock market with some stocks and some trades, for use within test methods
        /// Important: stocks must already be set up in the stock market
        /// </summary>
        public void SetUpStockMarketWithTrades(StockMarket stockMarket)
        {
            try
            {
                //make base trades: buy an increasing number of stocks (1, 2, ...) priced at 10 pence intervals starting at 10 pence.
                //we need to wait a second between each trade to get different timestamps so I can test properly.

                string stockSymbolTEA = "TEA";
                int shareQuantity = 1;
                BuyOrSell buyOrSell = BuyOrSell.Buy;
                double tradedPrice = 10.0;
                bool success = stockMarket.TradeSomeStock(stockSymbolTEA, shareQuantity, buyOrSell, tradedPrice);

                System.Threading.Thread.Sleep(1000);

                string stockSymbolPOP = "POP";
                shareQuantity = 2;
                tradedPrice = 20.0;
                success = stockMarket.TradeSomeStock(stockSymbolPOP, shareQuantity, buyOrSell, tradedPrice);

                System.Threading.Thread.Sleep(1000);

                string stockSymbolALE = "ALE";
                shareQuantity = 3;
                tradedPrice = 30.0;
                success = stockMarket.TradeSomeStock(stockSymbolALE, shareQuantity, buyOrSell, tradedPrice);

                System.Threading.Thread.Sleep(1000);

                string stockSymbolGIN = "GIN";
                shareQuantity = 4;
                tradedPrice = 40.0;
                success = stockMarket.TradeSomeStock(stockSymbolGIN, shareQuantity, buyOrSell, tradedPrice);

                System.Threading.Thread.Sleep(1000);

                string stockSymbolJOE = "JOE";
                shareQuantity = 5;
                tradedPrice = 50.0;
                success = stockMarket.TradeSomeStock(stockSymbolJOE, shareQuantity, buyOrSell, tradedPrice);

            }
            catch (Exception e)
            {
                Assert.Fail("Exception thrown with message " + e.Message);
            }
        }

    }
}
