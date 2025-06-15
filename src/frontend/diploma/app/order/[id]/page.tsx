"use client";

import { useParams, useRouter } from "next/navigation";
import { useEffect, useState } from "react";
import {
  Button,
  Card,
  Descriptions,
  Typography,
  Alert,
  Tag,
  Drawer,
} from "antd";
import { SendOutlined } from "@ant-design/icons";
import { useGetOrderByIdQuery } from "@/app/hooks/order/useOrderQuery";
import { OrderStatus } from "@/app/models/order";
import { ChatForm } from "@/app/components/chat/chatFrom";
import { useDownloadFile } from "@/app/hooks/useDownloadFile";
import { getInvoiceForOrder } from "@/app/http/order";

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

  const [chatOpen, setChatOpen] = useState(false);

  const { download: excelDownload, isLoading: isExcelLoading } =
    useDownloadFile(() => getInvoiceForOrder(orderId));

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
    <div style={{ padding: 24, maxWidth: 900, margin: "0 auto" }}>
      <Card
        title={<>Заказ №{order.id}</>}
        style={{ marginBottom: 24 }}
        extra={
          <div>
            <Button type="primary" onClick={() => setChatOpen(true)}>
              Чат с продавцом
            </Button>
            <Button
              style={{ marginLeft: 20 }}
              type="primary"
              onClick={excelDownload}
            >
              Скачать накладную
            </Button>
          </div>
        }
      >
        <Descriptions title="Информация о заказе" bordered column={1}>
          <Descriptions.Item label="Статус">
            <Tag color={statusColorMap[order.status] || "default"}>
              {order.statusText}
            </Tag>
          </Descriptions.Item>
          <Descriptions.Item label="Дата создания">
            {order.createDate}
          </Descriptions.Item>
          <Descriptions.Item label="Дата доставки">
            {order.deliveryDate ?? "-"}
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

        <Descriptions title="Информация о твоаре" bordered column={1}>
          <Descriptions.Item label="Наиминование">
            {order.product.name}
          </Descriptions.Item>
          <Descriptions.Item label="Цена за ед.">
            {order.product.price}
          </Descriptions.Item>
          <Descriptions.Item label="Описание">
            {order.product.description}
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

      <Drawer
        title="Чат с продавцом"
        placement="right"
        onClose={() => setChatOpen(false)}
        open={chatOpen}
        width={400}
      >
        <ChatForm order={data.response} />
      </Drawer>
    </div>
  );
}
