namespace CourierServiceLibrary
{
    /// <summary>
    /// Перечисление для состояния заказов
    /// </summary>
    public enum Readiness
    {
        /// <summary>
        /// Выполнен
        /// </summary>
        Completed,
        /// <summary>
        /// Ожидает
        /// </summary>
        Pending,
        /// <summary>
        /// Отменен
        /// </summary>
        Canceled,
        /// <summary>
        /// Выполняется
        /// </summary>
        InProgress
    }
}
