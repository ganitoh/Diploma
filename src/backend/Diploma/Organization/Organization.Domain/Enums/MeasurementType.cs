using System.ComponentModel;

namespace Organization.Domain.Enums;

/// <summary>
/// Тип измерения товара
/// </summary>
public enum MeasurementType
{
    [Description("Штука")]
    Thing,
    [Description("Грамм")]
    Gram,
    [Description("Килограмм")]
    Kg,
    [Description("Тона")]
    Tones,
}