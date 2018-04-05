namespace MarketDss.Business.Securities
{
    public partial class SecuritiesRepository
    {
        private string SelectAllSecuritiesQuery =>
            @"SELECT *
                FROM Securities";

        private string SelectSecurityDividendBySymbolAndDateQuery =>
            @"SELECT *
                FROM SecurityDividends
                WHERE       Symbol = @Symbol
                        AND Date = @Date";
    }
}
