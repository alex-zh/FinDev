using System;

namespace BitfinexConnector
{
    public interface ICryptoConnector
    {
        event Action<string , string , decimal? /* шаг цена */, decimal? /* наш объема */, SecurityTypes? /* пока только один, но я планирую подключить фьючерсы */> NewInstrument;

        event Action<DateTime /* биржевое время в UTC */, string /* код пары */, string /* код счета */, decimal? /* текущая поза */, decimal? /* осталось денег на счете */, decimal? /* пнл */> PositionChanged;

        event Action<DateTime /* биржевое время в UTC */, string /* код пары */, decimal? /* последяя цена */, decimal? /* бид */, decimal? /* аск */> PriceChanged;

        event Action<DateTime /* биржевое время в UTC */, string /* код пары */, Tuple<decimal, decimal>[] /* биды */, Tuple<decimal, decimal>[] /* аски */> DomChanged;

        // для рассчета волы
        event Action<string /* код пары */, DateTime /* биржевое время в UTC */, decimal, decimal, decimal, decimal, decimal /* OHLCV */> HistoryChanged;

        event Action<DateTime /* биржевое время в UTC */, long, string /* код пары */, string /* код счета */, Sides?, decimal?, decimal?, OrderStates /* используется из стокшарпа значения */, Exception /* ошибка регистрации или отмена */> OrderChanged;

        event Action<bool /* true - подключение, false - отключение */, Exception /* если != null, то означает разрыв связи или же неверную авторизацию */> ConnectionChanged;

        void RegisterOrder(long userId, string symbol, string account, Sides side, decimal price, decimal volume);

        void CancelOrder(long userId);

        // робот уже имеет базу инструментов в файле, поэтому грузить инфу нужно только по запросу, а не при подключении
        void DownloadSymbols();

        void DownloadAccounts();

        void DownloadHistory(string symbol, TimeSpan timeFrame, DateTime fromUtc, DateTime toUtc);

        void Connect();

        void Disconnect();

        void SubscribePricing(string symbol);

        void UnsubscribePricing(string symbol);

        void SubscribeDom(string symbol);

        void UnsubscribeDom(string symbol);
    }
}
