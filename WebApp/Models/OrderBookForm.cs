using System;

namespace WebApp.Models
{
    public class OrderBookForm
    {
        public string Provider { get; set; }
        public string Symbol { get; set; }
        public string SymbolTitle { get; set; }

        public override string ToString()
        {
            return $"DataProvider: {Provider}, Symbol: {Symbol}, SymbolTitle: {SymbolTitle}";
        }

        public void UseDefaultProviderIfEmpty(string value)
        {
            Provider = String.IsNullOrEmpty(Provider) ? value : Provider;
        }
        
        public void UseDefaultSymbolIfEmpty(string value)
        {
            Symbol = String.IsNullOrEmpty(Symbol) ? value : Symbol;
        }
    }
}