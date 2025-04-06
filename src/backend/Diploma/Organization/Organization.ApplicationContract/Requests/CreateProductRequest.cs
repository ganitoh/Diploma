﻿using Organization.Domain.Enums;

namespace Organization.ApplicationContract.Requests;

/// <summary>
/// Запрос на создание товара
/// </summary>
public class CreateProductRequest
{
    /// <summary>
    /// Наиминование
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Цена за ед.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Доступное количество 
    /// </summary>
    public int AvailableCount { get; set; }

    /// <summary>
    /// Всего продано
    /// </summary>
    public int TotalSold { get; set; }

    /// <summary>
    /// Единица измерения
    /// </summary>
    public MeasurementType MeasurementType { get; set; }

    /// <summary>
    /// Есть в наличии
    /// </summary>
    public bool IsStock { get; set; }

    /// <summary>
    /// Идентификатор продающей организации
    /// </summary>
    public int SellOrganizationId { get; set; }
}