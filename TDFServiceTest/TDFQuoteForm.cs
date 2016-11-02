using Model.Quote;
using Quote.TDF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TDFServiceTest
{
    public partial class TDFQuoteForm : Form
    {
        private TDFQuote _quote = null;
        public TDFQuoteForm(TDFQuote quote)
        {
            InitializeComponent();

            _quote = quote;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string input = this.tbSecuCodes.Text.Trim();
            //var secuItem = _quote.Quote.GetSecurity(secuCode);
            List<string> secuCodeList = input.Split(';').ToList();
            var data = _quote.Quote.GetMarketData(secuCodeList);
            this.rtbQuote.Text = GetDataStr(data);
        }


        private string GetDataStr(Dictionary<string, MarketData> datas)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var kv in datas)
            {
                sb.AppendFormat("SecuCode: {0}, Price: {1}, B1: {2}, S1: {3}\n", kv.Key, kv.Value.CurrentPrice, kv.Value.BuyPrice1, kv.Value.SellPrice1);
            }

            return sb.ToString();
        }
    }
}
