import { IOrganiaiton } from "@/app/models/organization";
import { ColumnsType } from "antd/es/table";
import { Table, Button, TimePicker, Switch, Space, Form } from "antd";

export const adminOrganizationColumns: ColumnsType<IOrganiaiton> = [
  {
    title: "ИНН",
    dataIndex: "inn",
  },
  {
    title: "Email",
    dataIndex: "email",
    key: "totalPrice",
    align: "center",
  },
  {
    title: "Адрес",
    dataIndex: "legalAddress",
    key: "totalPrice",
    align: "center",
  },
];
