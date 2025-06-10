import { IOrder } from "@/app/models/order";
import { IProduct } from "@/app/models/product";
import { ColumnsType } from "antd/es/table";
import { Tag } from "antd";

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
    render: (price: number) => `${price} ₽`,
  },
];

export const sellOrderColumns: ColumnsType<IOrder> = [
  {
    title: "Стоимость",
    dataIndex: "totalPrice",
    key: "totalPrice",
    align: "center",
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
  {
    title: "Покупающая организация",
    dataIndex: "buyerOrganizationName",
    key: "buyerOrganizationName",
    align: "center",
  },
];
