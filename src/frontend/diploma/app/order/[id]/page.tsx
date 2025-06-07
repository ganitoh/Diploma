"use client";

import { useParams, useRouter } from "next/navigation";
import { useEffect } from "react";
import { Button, Card, Descriptions, Typography, Alert, Tag } from "antd";
import { useGetOrderByIdQuery } from "@/app/hooks/order/useOrderQuery";
import { OrderStatus } from "@/app/models/order";

const { Title } = Typography;

const statusColorMap: Record<string, string> = {
  Pending: "blue",
  Processing: "orange",
  Completed: "green",
  Cancelled: "red",
};

export default function OrderPage() {
  const params = useParams();
  const router = useRouter();
  const orderId = params.id ? Number(params.id) : 0;

  const { data, isLoading } = useGetOrderByIdQuery(orderId);

  useEffect(() => {
    const userId = localStorage.getItem("userId");
    if (!userId) {
      router.push("/login");
    }
  }, [router]);

  if (isLoading) {
    return (
      <div style={{ textAlign: "center", marginTop: 50 }}>
        <Title level={4}>Загрузка данных заказа...</Title>
      </div>
    );
  }

  if (!data?.succeeded) {
    return (
      <div style={{ textAlign: "center", marginTop: 50 }}>
        <Title level={3}>Заказ не найден</Title>
        <Button type="primary" onClick={() => router.push("/orders")}>
          Вернуться к заказам
        </Button>
      </div>
    );
  }

  const order = data.response;

  return (
    <div style={{ padding: 24, maxWidth: 800, margin: "0 auto" }}>
      <Card title={<>Заказ №{order.id}</>} style={{ marginBottom: 24 }}>
        <Descriptions title="Информация о заказе" bordered column={1}>
          <Descriptions.Item label="Статус">
            <Tag color={statusColorMap[order.status] || "default"}>
              {order.statusText}
            </Tag>
          </Descriptions.Item>
          <Descriptions.Item label="Дата создания">
            {new Date(order.createDate).toLocaleDateString()}
          </Descriptions.Item>
          <Descriptions.Item label="Дата доставки">
            {new Date(order.deliveryDate).toLocaleDateString()}
          </Descriptions.Item>
          <Descriptions.Item label="Общая сумма">
            {order.totalPrice} ₽
          </Descriptions.Item>
          <Descriptions.Item label="Организация">
            <Button
              type="link"
              href={`/organization/${order.sellerOrganizationId}`}
            >
              Перейти к организации
            </Button>
          </Descriptions.Item>
        </Descriptions>

        {order.status === OrderStatus.Close && (
          <Alert
            message="Этот заказ закрыт"
            type="error"
            showIcon
            style={{ marginTop: 16 }}
          />
        )}
      </Card>
    </div>
  );
}
