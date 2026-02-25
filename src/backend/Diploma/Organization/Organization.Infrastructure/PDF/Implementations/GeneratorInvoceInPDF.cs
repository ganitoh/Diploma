using Organizaiton.Application.Common.PDF;
using Organization.Domain.Models;
using PdfSharp.Drawing;
using PdfSharp.Fonts;
using PdfSharp.Pdf;

namespace Organization.Infrastructure.PDF.Implementations;

/// <summary>
/// Генерация ПДФ накладной для заказа
/// </summary>
public class GeneratorInvoceInPDF : IGenerateInvoiceForOrder
{
    public void Generate(Order order, Stream stream)
    {
        GlobalFontSettings.UseWindowsFontsUnderWindows = true;
        var doc = new PdfDocument();
        doc.Info.Title = $"Накладная №{order.Id}";
        var page = doc.AddPage();
        page.Orientation = PdfSharp.PageOrientation.Landscape;
        var gfx = XGraphics.FromPdfPage(page);
    
        var leftMargin = 40;
        var topMargin = 40;
        var width = (int)page.Width - leftMargin * 2;
        var y = topMargin;
    
        var titleFont = new XFont("Verdana", 20, XFontStyleEx.Bold);
        var normalFont = new XFont("Verdana", 12, XFontStyleEx.Regular);
        var tableHeaderFont = new XFont("Verdana", 12, XFontStyleEx.Bold);
    
        // Заголовок
        gfx.DrawString($"НАКЛАДНАЯ №{order.Id}", titleFont, XBrushes.Black,
            new XRect(0, y, page.Width, 40), XStringFormats.TopCenter);
        y += 45;
    
        // Основная информация
        gfx.DrawString($"Дата создания: {order.CreateDate:dd.MM.yyyy}", normalFont, XBrushes.Black, leftMargin, y); y += 20;
        gfx.DrawString($"Продавец: {order.SellerOrganization?.Name}", normalFont, XBrushes.Black, leftMargin, y); y += 20;
        gfx.DrawString($"Покупатель: {order.BuyerOrganization?.Name}", normalFont, XBrushes.Black, leftMargin, y); y += 25;
    
        // Таблица товаров
        var tableY = y;
        var rowHeight = 25;
        var col1 = leftMargin;
        var col2 = col1 + 230;
        var col3 = col2 + 80;
        var col4 = col3 + 80;
        var col5 = col4 + 80;
    
        
        gfx.DrawRectangle(XPens.Black, col1, tableY, width, rowHeight);
        gfx.DrawString("Товар", tableHeaderFont, XBrushes.Black, new XRect(col1, tableY, 230, rowHeight), XStringFormats.Center);
        gfx.DrawString("Кол-во", tableHeaderFont, XBrushes.Black, new XRect(col2, tableY, 80, rowHeight), XStringFormats.Center);
        gfx.DrawString("Ед.", tableHeaderFont, XBrushes.Black, new XRect(col3, tableY, 80, rowHeight), XStringFormats.Center);
        gfx.DrawString("Цена", tableHeaderFont, XBrushes.Black, new XRect(col4, tableY, 80, rowHeight), XStringFormats.Center);
        gfx.DrawString("Сумма", tableHeaderFont, XBrushes.Black, new XRect(col5, tableY, 90, rowHeight), XStringFormats.Center);
    
        tableY += rowHeight;
    
        
        gfx.DrawRectangle(XPens.Black, col1, tableY, width, rowHeight);
        gfx.DrawLine(XPens.Black, col2, tableY, col2, tableY + rowHeight);
        gfx.DrawLine(XPens.Black, col3, tableY, col3, tableY + rowHeight);
        gfx.DrawLine(XPens.Black, col4, tableY, col4, tableY + rowHeight);
        gfx.DrawLine(XPens.Black, col5, tableY, col5, tableY + rowHeight);
    
        gfx.DrawString(order.Product?.Name ?? "-", normalFont, XBrushes.Black, new XRect(col1 + 5, tableY + 5, 225, rowHeight), XStringFormats.TopLeft);
        gfx.DrawString(order.Quantity.ToString(), normalFont, XBrushes.Black, new XRect(col2, tableY, 80, rowHeight), XStringFormats.Center);
        gfx.DrawString("шт.", normalFont, XBrushes.Black, new XRect(col3, tableY, 80, rowHeight), XStringFormats.Center);
        gfx.DrawString($"{order.Product?.Price:N2}", normalFont, XBrushes.Black, new XRect(col4, tableY, 80, rowHeight), XStringFormats.Center);
        gfx.DrawString($"{order.TotalPrice:N2}", normalFont, XBrushes.Black, new XRect(col5, tableY, 90, rowHeight), XStringFormats.Center);
    
        tableY += rowHeight + 15;
        
        gfx.DrawString($"ИТОГО: {order.TotalPrice:N2} руб.", titleFont, XBrushes.Black, new XRect(0, tableY, page.Width - leftMargin, 30), XStringFormats.TopRight);
        tableY += 40;
        
        var signatureLineWidth = 150;
        gfx.DrawString("Продавец", normalFont, XBrushes.Black, leftMargin, tableY);
        gfx.DrawLine(XPens.Black, leftMargin + 70, tableY + 12, leftMargin + 70 + signatureLineWidth, tableY + 12);
    
        gfx.DrawString("Покупатель", normalFont, XBrushes.Black, col3, tableY);
        gfx.DrawLine(XPens.Black, col3 + 90, tableY + 12, col3 + 90 + signatureLineWidth, tableY + 12);
    
        doc.Save(stream, false);
    }
}