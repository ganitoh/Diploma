using System.ComponentModel;

namespace Organization.Domain.Enums;

public enum MeasurementType
{
    [Description("Штука")]
    Thing,
    [Description("Грамм")]
    Gram,
    [Description("Килограмм")]
    Kg,
    [Description("Тона")]
    Tones
}