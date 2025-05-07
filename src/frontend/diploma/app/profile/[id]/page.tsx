"use client";

import { useGetOrganizationByUserIdQuery } from '@/app/hooks/organization/useOrganizationQuery';
import { useParams, useRouter } from 'next/navigation';
import { Button, Card, Descriptions, Empty, Form, Input, InputNumber, List, Modal, Space, Typography } from 'antd';
import { useState } from 'react';

const { Title } = Typography

interface IProductFromValues {
  id: number;
  name: string;
  price: number;
  description: string;
}

export default function ProfilePage() {
  const [isAddProductModalOpen, setIsAddProductModalOpen] = useState<boolean>(false);
  const params = useParams();
  const router = useRouter();
  const [form] = Form.useForm<IProductFromValues>();
  const id = params.id;

  const { data, isLoading } = useGetOrganizationByUserIdQuery(id?.toString() ?? "")

  const handleCancel = () => setIsAddProductModalOpen(false);

  const handleAddProduct = async () => {}

  return (
    <div>
      {!data?.succeeded && (
        <div style={{ textAlign: 'center', marginTop: 50 }}>
          <Title level={3}>Организация не найдена</Title>
          <Button type="primary" onClick={() => router.push('/organization/create')}>
            Добавить организацию
          </Button>
        </div>
      )}
      {data?.succeeded && data.response && (
        <div style={{ padding: 24, maxWidth: 1000, margin: '0 auto' }}>

          <Card title={<>Организация: {data.response.name}</>} style={{ marginBottom: 24 }}>
            <Descriptions title="Информация об организации" bordered column={1}>
              <Descriptions.Item label="ИНН">{data.response.inn}</Descriptions.Item>
              <Descriptions.Item label="Email">{data.response.email}</Descriptions.Item>
              <Descriptions.Item label="Юридический адрес">{data.response.legalAddress}</Descriptions.Item>
              <Descriptions.Item label="Описание">{data.response.description}</Descriptions.Item>
              <Descriptions.Item label="Прошла верефикацию">{data.response.isApproval ? 'Да' : 'Нет'}</Descriptions.Item>
            </Descriptions>
          </Card>

          <Card title={
            <Space style={{ display: 'flex', justifyContent: 'space-between', width: '100%' }}>
              <span>Товары</span>
              <Button type="primary" onClick={() => setIsAddProductModalOpen(true)}>
                Добавить товар
              </Button>
            </Space>
          } style={{ marginBottom: 24 }}>
            {data.response.products.length > 0 ? (
              <List
                itemLayout="vertical"
                dataSource={data.response.products}
                renderItem={(product) => (
                  <List.Item key={product.id}>
                    <List.Item.Meta title={product.name} description={product.description} />
                    Цена: {product.price} ₽
                  </List.Item>
                )}
              />
            ) : (
              <Empty description="Нет товаров" />
            )}
          </Card>

          <Card title="Заказы на продажу" style={{ marginBottom: 24 }}>
            {data.response.sellOrders.length > 0 ? (
              <List
                dataSource={data.response.sellOrders}
                renderItem={(order) => (
                  <List.Item key={order.id}>
                    Цена: {order.totalPrice} ₽
                  </List.Item>
                )}
              />
            ) : (
              <Empty description="Нет заказов на продажу" />
            )}
          </Card>

          <Card title="Заказы на покупку">
            {data.response.buyOrders.length > 0 ? (
              <List
                dataSource={data.response.buyOrders}
                renderItem={(order) => (
                  <List.Item key={order.id}>
                    Цена: {order.totalPrice} ₽
                  </List.Item>
                )}
              />
            ) : (
              <Empty description="Нет заказов на покупку" />
            )}
          </Card>
        </div>
      )}
      <Modal
        title="Добавить товар"
        open={isAddProductModalOpen}
        onCancel={handleCancel}
        onOk={handleAddProduct}
        okText="Сохранить"
        cancelText="Отмена"
      >
        <Form form={form} layout="vertical">
          <Form.Item
            label="Название"
            name="name"
            rules={[{ required: true, message: 'Введите название товара' }]}
          >
            <Input />
          </Form.Item>

          <Form.Item
            label="Описание"
            name="description"
            rules={[{ required: true, message: 'Введите описание' }]}
          >
            <Input.TextArea rows={3} />
          </Form.Item>

          <Form.Item
            label="Цена"
            name="price"
            rules={[{ required: true, message: 'Введите цену' }]}
          >
            <InputNumber style={{ width: '100%' }} min={0} />
          </Form.Item>
        </Form>
      </Modal>
    </div>
  );
}