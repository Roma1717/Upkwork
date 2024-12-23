using System.ComponentModel;

namespace Domain.Enum;

public enum Status
{
    [Description("Не рассмотрено")]
    NotConsidered=0,
    [Description("Одобрено")]
    Approved,
    [Description("Отказано")]
    Denied
}