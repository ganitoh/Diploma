"use client";

import { Card, DatePicker, Select, Space } from "antd";
import { useRouter } from "next/navigation";
import { useState } from "react";
import dayjs from "dayjs";
import { useGetSellOrderAnalyticsQuery } from "@/app/hooks/analytics/analytics";
import {
  CartesianGrid,
  Legend,
  Line,
  LineChart,
  XAxis,
  YAxis,
  Tooltip,
} from "recharts";
import { useGetShortProductByOrganizationQuery } from "@/app/hooks/product/useProductQuery";

export default function OrganizationPage() {
  const router = useRouter();

  const [startDate, setStartDate] = useState<Date | undefined>(undefined);
  const [endDate, setEndDate] = useState<Date | undefined>(undefined);
  const [selectedProductId, setSelectedProductId] = useState<
    number | undefined
  >(undefined);

  const { data: products } = useGetShortProductByOrganizationQuery(
    Number(localStorage.getItem("organizationId"))
  );
  const { data, isLoading, refetch } = useGetSellOrderAnalyticsQuery({
    startDate: startDate,
    endDate: endDate,
    entityId: selectedProductId ?? 0,
  });

  const handleStartDateChange = (date: dayjs.Dayjs | null) => {
    setStartDate(date ? date.toDate() : undefined);
  };

  const handleEndDateChange = (date: dayjs.Dayjs | null) => {
    setEndDate(date ? date.toDate() : undefined);
  };

  return (
    <div style={{ padding: 24, maxWidth: 1200, margin: "0 auto" }}>
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
              Аналитика товаров
            </span>
            <Space>
              <Select
                style={{ width: 300 }}
                placeholder="Выберите товар"
                loading={isLoading}
                value={selectedProductId ?? undefined}
                onChange={(value) => setSelectedProductId(value)}
                options={
                  products?.response.map((product) => ({
                    label: product.name,
                    value: product.id,
                  })) || []
                }
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
          <div>
            <LineChart
              width={1000}
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
        </div>
      </Card>
    </div>
  );
}
