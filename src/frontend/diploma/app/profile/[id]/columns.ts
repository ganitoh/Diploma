import { IOrder } from "@/app/models/order";
import { IProduct } from "@/app/models/product";
import { ColumnsType } from "antd/es/table";
import { Tag } from "antd";
import { formatMoney } from "@/app/utils/format";

const statusColorMap: Record<string, string> = {
  Pending: "blue",
  Processing: "orange",
  Completed: "green",
  Cancelled: "red",
};

export const productColumns: ColumnsType<IProduct> = [
  {
    title: "Название",
    dataIndex: "name",
    key: "name",
    align: "center",
  },
  {
    title: "Описание",
    dataIndex: "description",
    key: "description",
    align: "center",
    render: (description: string) => (description ? description : "-"),
  },
  {
    title: "Цена (₽)",
    dataIndex: "price",
    key: "price",
    align: "center",
    render: (price: number) => `${formatMoney(price)} ₽`,
  },
];

export const sellOrderColumns: ColumnsType<IOrder> = [
  {
    title: "Стоимость",
    dataIndex: "totalPrice",
    key: "totalPrice",
    align: "center",
    render: (totalPrice: number) => `${formatMoney(totalPrice)} ₽`,
  },
  {
    title: "Дата создания",
    dataIndex: "createDate",
    key: "createDate",
    align: "center",
  },
  {
    title: "Статус",
    dataIndex: "statusText",
    key: "statusText",
    align: "center",
  },
];
