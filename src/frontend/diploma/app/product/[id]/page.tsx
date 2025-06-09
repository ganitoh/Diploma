"use client";

import { useGetProductByIdQuery } from "@/app/hooks/product/useProductQuery";
import { useParams, useRouter } from "next/navigation";
import {
  Button,
  Card,
  Descriptions,
  Modal,
  Form,
  InputNumber,
  Typography,
  Alert,
  Col,
} from "antd";
import { useEffect, useState } from "react";
import { MeasurementType } from "@/app/models/product";
import { useCheckRoleUserQuery } from "@/app/hooks/user/useUserQuery";
import { AdminPanel } from "@/app/components/adminPanel/adminPanel";
import Link from "next/link";
import { useOrderMutation } from "@/app/hooks/order/useOrderMutation";
import { AddRatingForm } from "@/app/components/addRating/addRatingForm";
import "../../globals.css";

const { Title } = Typography;

const MeasurementTypeLabels: Record<MeasurementType, string> = {
  [MeasurementType.Thing]: "Шт",
  [MeasurementType.Gram]: "Грамм",
  [MeasurementType.Kg]: "Килограмм",
  [MeasurementType.Tones]: "Тонны",
};

export default function ProductPage() {
  const [isEditProductModalOpen, setIsEditProductModalOpen] =
    useState<boolean>(false);
  const [isOrderModalOpen, setIsOrderModalOpen] = useState<boolean>(false);
  const [form] = Form.useForm();
  const params = useParams();
  const router = useRouter();
  const productId = params.id ? Number(params.id) : 0;

  const { createOrderMutation } = useOrderMutation();

  const { data: resultCheck } = useCheckRoleUserQuery("Admin");
  const { data, isLoading } = useGetProductByIdQuery(productId);

  useEffect(() => {
    const userId = localStorage.getItem("userId");
    if (!userId) {
      router.push("/login");
    }
  }, [router]);

  if (isLoading) {
    return (
      <div style={{ textAlign: "center", marginTop: 50 }}>
        <Title level={4}>Загрузка данных о товаре...</Title>
      </div>
    );
  }

  if (!data?.succeeded) {
    return (
      <div style={{ textAlign: "center", marginTop: 50 }}>
        <Title level={3}>Товар не найден</Title>
        <Button type="primary" onClick={() => router.push("/products")}>
          Вернуться к списку товаров
        </Button>
      </div>
    );
  }

  const product = data.response;

  // Открыть форму создания заказа
  const openOrderModal = () => {
    form.resetFields();
    setIsOrderModalOpen(true);
  };

  // Закрыть форму создания заказа
  const closeOrderModal = () => {
    setIsOrderModalOpen(false);
  };

  // Обработчик отправки формы
  const onOrderFinish = async () => {
    var response = await createOrderMutation.mutateAsync({
      sellerOrganizationId: data.response.sellOrganizationId,
      quantity: form.getFieldValue("quantity"),
      productId: data.response.id,
    });

    alert("Заказ создан");
    router.push(`/order/${response.response}`);
  };

  return (
    <div className="main-div">
      {resultCheck?.response && <AdminPanel />}

      <Card
        title={<>Товар: {product.name}</>}
        extra={
          resultCheck?.response && (
            <Button
              type="primary"
              onClick={() => setIsEditProductModalOpen(true)}
            >
              Редактировать
            </Button>
          )
        }
        style={{ marginBottom: 24 }}
      >
        <Descriptions title="Информация о товаре" bordered column={1}>
          <Descriptions.Item label="Название">{product.name}</Descriptions.Item>
          <Descriptions.Item label="Описание">
            {product.description}
          </Descriptions.Item>
          <Descriptions.Item label="Цена">{product.price} ₽</Descriptions.Item>
          <Descriptions.Item label="Единица измерения">
            {MeasurementTypeLabels[product.measurementType]}
          </Descriptions.Item>
          <Descriptions.Item label="В наличии">
            {product.availableCount}{" "}
            {MeasurementTypeLabels[product.measurementType]}
          </Descriptions.Item>
          <Descriptions.Item label="Организация">
            <Link
              href={`/organization/${product.sellOrganizationId}`}
              style={{ color: "#1890ff", textDecoration: "underline" }}
            >
              {product.sellOrganizationName}
            </Link>
          </Descriptions.Item>
          <Descriptions.Item label="оценка">{product.rating}</Descriptions.Item>
        </Descriptions>

        <div style={{ textAlign: "right", marginTop: 16 }}>
          <Button
            type="primary"
            disabled={!product.isStock}
            onClick={openOrderModal}
          >
            Сделать заказ
          </Button>
        </div>

        {!product.isStock && (
          <Col span={24} style={{ marginTop: 16 }}>
            <Alert
              message="Товар неактивен. Пользователи не могут его заказывать."
              type="warning"
              showIcon
            />
          </Col>
        )}
      </Card>
      <div>
        <AddRatingForm isProduct={true} entityId={data.response.id} />
      </div>

      {/* Модальное окно для создания заказа */}
      <Modal
        title="Создание заказа"
        open={isOrderModalOpen}
        onCancel={closeOrderModal}
        footer={null}
        destroyOnClose
      >
        <Form
          form={form}
          layout="vertical"
          onFinish={onOrderFinish}
          initialValues={{ quantity: 1 }}
        >
          <Form.Item label="Товар">
            <InputNumber
              value={product.price}
              disabled
              formatter={(value) => `${product.name}`}
              style={{ width: "100%" }}
            />
          </Form.Item>

          <Form.Item
            label="Количество"
            name="quantity"
            rules={[
              { required: true, message: "Укажите количество" },
              { type: "number", min: 1, message: "Минимум 1" },
            ]}
          >
            <InputNumber
              addonAfter={MeasurementTypeLabels[product.measurementType]}
              style={{ width: "100%" }}
              min={1}
            />
          </Form.Item>

          <Form.Item>
            <div style={{ textAlign: "right" }}>
              <Button onClick={closeOrderModal} style={{ marginRight: 8 }}>
                Отмена
              </Button>
              <Button
                type="primary"
                htmlType="submit"
                disabled={!product.isStock}
              >
                Подтвердить
              </Button>
            </div>
          </Form.Item>
        </Form>
      </Modal>
    </div>
  );
}
