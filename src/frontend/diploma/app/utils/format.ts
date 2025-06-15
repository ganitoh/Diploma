export function formatMoney(value: number | string): string {
  if (value === null || value === undefined) return "";

  const number =
    typeof value === "string"
      ? parseFloat(value.replace(/\s/g, "").replace(",", "."))
      : value;

  if (isNaN(number)) return "";

  return number.toLocaleString("ru-RU", {
    minimumFractionDigits: 2,
    maximumFractionDigits: 2,
  });
}
