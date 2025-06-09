import { IProduct } from "@/app/models/product";
import { ColumnsType } from "antd/es/table";

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
