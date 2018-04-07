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

        private string UpdateSecurityByIdQuery =>
            @"UPDATE Securities
                SET     Symbol = @Symbol,
                        Sector = @Sector,
                        PriceToEarningsRatio = @PriceToEarningsRatio,
                        PriceLow52Weeks = @PriceLow52Weeks,
                        PriceHigh52Weeks = @PriceHigh52Weeks,
                        MarketCapitalization = @MarketCapitalization,
                        NextExDividendDate = @NextExDividendDate,
                        NextDividendAmount = @NextDividendAmount,
                        ComputedRelativeStrengthIndex = @ComputedRelativeStrengthIndex,
                        ComputedDividendYield = @ComputedDividendYield
                WHERE Id = @Id";

        private string InsertSecurityDividendQuery =>
            @"INSERT INTO SecurityDividends
                    ( SecurityId,  Dividend,  ExDividendDate,  RecordDate,  AnnouncementDate,  PaymentDate)
            OUTPUT INSERTED.Id
            VALUES  (@SecurityId, @Dividend, @ExDividendDate, @RecordDate, @AnnouncementDate, @PaymentDate)";

        private string SelectSecurityDividendBySecurityIdQuery =>
            @"SELECT *
                FROM SecurityDividends
                WHERE SecurityId = @SecurityId";

        private string UpdateSecurityDividendByIdQuery =>
            @"UPDATE SecurityDividends
                SET     SecurityId = @SecurityId,
                        Dividend = @Dividend,
                        ExDividendDate = @ExDividendDate,
                        RecordDate = @RecordDate,
                        AnnouncementDate = @AnnouncementDate,
                        PaymentDate = @PaymentDate
                WHERE Id = @Id";
    }
}
