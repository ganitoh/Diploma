"use client";

import { Card, List, Typography } from "antd";
import { Content } from "antd/es/layout/layout";
import { FireOutlined, ShopOutlined,InfoCircleOutlined } from "@ant-design/icons";


import { useRouter } from "next/navigation";

const {Title, Paragraph} = Typography

const topProducts = [
  { name: 'Молоко «Ферма»', price: '120₽', key: '1' },
  { name: 'Хлеб «Домашний»', price: '60₽', key: '2' },
  { name: 'Сыр «Российский»', price: '300₽', key: '3' },
  { name: 'Сыр «Российский»', price: '300₽', key: '3' },
  { name: 'Сыр «Российский»', price: '300₽', key: '3' },
  { name: 'Сыр «Российский»', price: '300₽', key: '3' },
  { name: 'Сыр «Российский»', price: '300₽', key: '3' },
];

const topOrganizations = [
  { name: 'Фермерская лавка', rating: 4.8, key: '1' },
  { name: 'Эко Продукты', rating: 4.7, key: '2' },
  { name: 'Зелёный рынок', rating: 4.6, key: '3' },
];

export default function Home() {
  const router = useRouter()
  return (
  <div>
    <Content style={{ padding: '24px', maxWidth: 1200, margin: '0 auto' }}>
        <Title level={2}><FireOutlined /> Топ продаваемых товаров</Title>
        <List
          grid={{ gutter: 16, column: 3 }}
          dataSource={topProducts}
          renderItem={item => (
            <List.Item>
              <Card title={item.name}>Цена: {item.price}</Card>
            </List.Item>
          )}
        />

        <Title level={2} style={{ marginTop: 40 }}><ShopOutlined /> Топ организаций</Title>
        <List
          grid={{ gutter: 16, column: 3 }}
          dataSource={topOrganizations}
          renderItem={item => (
            <List.Item>
              <Card title={item.name}>Рейтинг: {item.rating}</Card>
            </List.Item>
          )}
        />

        <Title level={2} style={{ marginTop: 40 }}><InfoCircleOutlined /> О портале</Title>
        <Paragraph>
          Добро пожаловать на наш портал! Здесь вы найдете лучшие фермерские и локальные продукты от проверенных организаций. 
          Мы объединяем продавцов и покупателей, чтобы обеспечить качественные и свежие продукты.
        </Paragraph>
      </Content>
  </div>
  );
}
