namespace MarketDss.Business.Securities
{
    public partial class SecuritiesRepository
    {
        private string SelectAllSecuritiesQuery =>
            @"SELECT *
                FROM Securities";
    }
}
