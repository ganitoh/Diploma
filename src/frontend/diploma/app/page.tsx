"use client";

import { Card, List, Typography } from "antd";
import { Content } from "antd/es/layout/layout";
import { FireOutlined, ShopOutlined,InfoCircleOutlined } from "@ant-design/icons";


import { useRouter } from "next/navigation";
import { useGetTopSellingProductsQuery } from "./hooks/product/useProductQuery";
import { useGetTopOrganizationByRatingQuery } from "./hooks/organization/useOrganizationQuery";

const {Title, Paragraph} = Typography

const topProductsMok = {
  response: [
    { name: 'Сахар-песок 1кг', price: '59 ₽' },
    { name: 'Молоко 3.2% 1л', price: '89 ₽' },
    { name: 'Свеча зажигания Bosch', price: '320 ₽' },
    { name: 'Масляный фильтр Mann', price: '540 ₽' },
    { name: 'Хлеб пшеничный 500г', price: '39 ₽' },
    { name: 'Тормозные колодки LADA', price: '1 290 ₽' },
  ]
};

const topOrganizationsMok = {
  response: [
    { name: 'Сахарный завод "Кристал-2"', ratingValue: 4.9 },
    { name: 'Поставщик "МолСнаб"', ratingValue: 4.8 },
    { name: 'Торговая сеть "Фермерские продукты"', ratingValue: 4.8 },
    { name: 'Склад запчастей "АвтоДеталь"', ratingValue: 4.5 },
    { name: 'СТО "ТехАвто"', ratingValue: 4.6 },
    { name: 'Поставщик "СахарТрейд"', ratingValue: 4.4 },
  ]
};

export default function Home() {
  const router = useRouter()
  const {data: topProducts} = useGetTopSellingProductsQuery(5)
  const {data: topOrganizations, isLoading} = useGetTopOrganizationByRatingQuery(5)


  return (
  <div>
    <Content style={{ padding: '24px', maxWidth: 1200, margin: '0 auto' }}>
        <Title level={2}><FireOutlined /> Топ продаваемых товаров</Title>
        <List
          grid={{ gutter: 16, column: 3 }}
          dataSource={topProductsMok?.response}
          renderItem={item => (
            <List.Item>
              <Card title={item.name}>Цена: {item.price}</Card>
            </List.Item>
          )}
        />

        <Title level={2} style={{ marginTop: 40 }}><ShopOutlined /> Топ организаций</Title>
        <List
          grid={{ gutter: 16, column: 3 }}
          dataSource={topOrganizationsMok?.response}
          renderItem={item => (
            <List.Item>
              <Card title={item.name}>Рейтинг: {item.ratingValue}</Card>
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
