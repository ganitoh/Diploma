using Organization.Domain.Models;
using Organization.Infrastructure.PDF.Abstractions;
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
        doc.Info.Title = $"Накладная \u2116{order.Id}";
        var page = doc.AddPage();
        var gfx = XGraphics.FromPdfPage(page);

        int y = 40;
        var titleFont = new XFont("Verdana", 20, XFontStyleEx.Bold);
        var normalFont = new XFont("Verdana", 12, XFontStyleEx.Regular);
        
        
        gfx.DrawString($"Накладная \u2116{order.Id}", titleFont, XBrushes.Black, new XRect(0, y, page.Width, 40), XStringFormats.TopCenter);
        y += 40;
        gfx.DrawString($"Номер заказа: {order.Id}", normalFont, XBrushes.Black, 40, y); y += 20;
        gfx.DrawString($"Дата создания: {order.CreateDate:dd.MM.yyyy}", normalFont, XBrushes.Black, 40, y); y += 20;
        gfx.DrawString($"Продавец: {order.SellerOrganization?.Name}", normalFont, XBrushes.Black, 40, y); y += 20;
        gfx.DrawString($"Покупатель: {order.BuyerOrganization?.Name}", normalFont, XBrushes.Black, 40, y); y += 20;
        gfx.DrawString($"Товар: {order.Product?.Name}", normalFont, XBrushes.Black, 40, y); y += 20;
        gfx.DrawString($"Количество: {order.Quantity}", normalFont, XBrushes.Black, 40, y); y += 20;
        gfx.DrawString($"Цена за ед.: {order.Product?.Price} руб.", normalFont, XBrushes.Black, 40, y); y += 20;
        gfx.DrawString($"Общая стоимость: {order.TotalPrice} руб.", titleFont, XBrushes.Black, 40, y); y += 20;

        doc.Save(stream, false);
    }
}