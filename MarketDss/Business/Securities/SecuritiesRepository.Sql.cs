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

        private string SelectSecurityBySymbolQuery =>
            @"SELECT *
                FROM Securities
                WHERE Symbol = @Symbol";

        private string InsertSecurityQuery =>
            @"INSERT INTO Securities
                    ( Symbol,  Sector,  PriceToEarningsRatio,  PriceLow52Weeks,  PriceHigh52Weeks,  MarketCapitalization,  NextExDividendDate,  NextDividendAmount,  ComputedRelativeStrengthIndex,  ComputedDividendYield)
            OUTPUT INSERTED.Id
            VALUES  (@Symbol, @Sector, @PriceToEarningsRatio, @PriceLow52Weeks, @PriceHigh52Weeks, @MarketCapitalization, @NextExDividendDate, @NextDividendAmount, @ComputedRelativeStrengthIndex, @ComputedDividendYield)";

        private string InsertSecurityDividendQuery =>
            @"INSERT INTO SecurityDividends
                    ( SecurityId,  Dividend,  ExDividendDate,  RecordDate,  AnnouncementDate,  PaymentDate)
            OUTPUT INSERTED.Id
            VALUES  (@SecurityId, @Dividend, @ExDividendDate, @RecordDate, @AnnouncementDate, @PaymentDate)";

        private string SelectSecurityDividendBySecurityIdQuery =>
            @"SELECT *
                FROM SecurityDividends
                WHERE SecurityId = @SecurityId";
    }
}
