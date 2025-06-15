"use client";

import { Card, DatePicker, Select, Space } from "antd";
import { useRouter } from "next/navigation";
import { useState } from "react";
import dayjs from "dayjs";
import {
  useGetSellOrderAnalyticsByStatusQuery,
  useGetSellOrderAnalyticsQuery,
} from "@/app/hooks/analytics/analytics";
import {
  CartesianGrid,
  Legend,
  Line,
  LineChart,
  XAxis,
  YAxis,
  Tooltip,
  PieChart,
  Pie,
} from "recharts";
import { OrderStatus } from "@/app/models/order";
import { stat } from "fs";

const orderStatusOptions = [
  { value: OrderStatus.Created, label: "Создан" },
  { value: OrderStatus.Collected, label: "Собран" },
  { value: OrderStatus.InDelivery, label: "В доставке" },
  { value: OrderStatus.Close, label: "Закрыт" },
];

export default function OrganizationPage() {
  const router = useRouter();

  const [startDate, setStartDate] = useState<Date | undefined>(undefined);
  const [endDate, setEndDate] = useState<Date | undefined>(undefined);
  const [statuses, setStatuses] = useState<OrderStatus[]>([]);

  const { data: analytics, refetch: analyticsRefetch } =
    useGetSellOrderAnalyticsByStatusQuery({
      startDate: startDate,
      endDate: endDate,
      entityId: Number(localStorage.getItem("organizationId")),
      statuses: statuses,
    });

  const { data, isLoading, refetch } = useGetSellOrderAnalyticsQuery({
    startDate: startDate,
    endDate: endDate,
    entityId: Number(localStorage.getItem("organizationId")),
  });

  const handleStartDateChange = (date: dayjs.Dayjs | null) => {
    setStartDate(date ? date.toDate() : undefined);
  };

  const handleEndDateChange = (date: dayjs.Dayjs | null) => {
    setEndDate(date ? date.toDate() : undefined);
  };

  return (
    <div style={{ padding: 24, maxWidth: 1400, margin: "0 auto" }}>
      <Card
        title={
          <Space
            style={{
              justifyContent: "space-between",
              width: "100%",
              flexWrap: "wrap",
              rowGap: 8,
            }}
            direction="horizontal"
          >
            <span style={{ fontSize: 18, fontWeight: 500 }}>
              Аналитика заказов
            </span>
            <Space>
              <Select<OrderStatus[]>
                mode="multiple"
                allowClear
                style={{ width: 320 }}
                placeholder="Статус заказа"
                value={statuses}
                onChange={setStatuses}
                options={orderStatusOptions}
              />
              <DatePicker
                placeholder="Дата начала"
                onChange={handleStartDateChange}
                value={startDate ? dayjs(startDate) : null}
              />
              <DatePicker
                placeholder="Дата окончания"
                onChange={handleEndDateChange}
                value={endDate ? dayjs(endDate) : null}
              />
            </Space>
          </Space>
        }
        style={{ marginBottom: 24 }}
      >
        <div>
          <p>
            Выбранный период: {startDate ? startDate.toLocaleDateString() : "—"}{" "}
            – {endDate ? endDate.toLocaleDateString() : "—"}
          </p>
          <div style={{ display: "flex", alignItems: "flex-start", gap: 32 }}>
            {/* Основной график */}
            <div>
              <LineChart
                width={900}
                height={300}
                data={data?.response}
                margin={{ top: 5, right: 20, bottom: 5, left: 0 }}
              >
                <Line
                  type="monotone"
                  dataKey="value"
                  stroke="#8884d8"
                  name="количество заказов"
                />
                <CartesianGrid stroke="#ccc" strokeDasharray="5 5" />
                <XAxis dataKey="name" />
                <YAxis />
                <Tooltip />
                <Legend />
              </LineChart>
            </div>
            {/* PieChart справа */}
            <div>
              <PieChart width={400} height={300}>
                <Pie
                  data={analytics?.response}
                  dataKey="value"
                  nameKey="name"
                  cx="50%"
                  cy="50%"
                  outerRadius={100}
                  fill="#8884d8"
                  label={({ name, value }) => `${name}: ${value}`}
                />
              </PieChart>
            </div>
          </div>
        </div>
      </Card>
    </div>
  );
}
