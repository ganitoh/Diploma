"use client";

import { Card, List, Typography } from "antd";
import { Content } from "antd/es/layout/layout";
import {
  FireOutlined,
  ShopOutlined,
  InfoCircleOutlined,
} from "@ant-design/icons";

import { useRouter } from "next/navigation";
import { useGetTopSellingProductsQuery } from "./hooks/product/useProductQuery";
import { useGetTopOrganizationByRatingQuery } from "./hooks/organization/useOrganizationQuery";
import Link from "next/link";

const { Title, Paragraph } = Typography;

export default function Home() {
  const router = useRouter();
  const { data: topProducts, isLoading } = useGetTopSellingProductsQuery(5);
  const { data: topOrganizations } = useGetTopOrganizationByRatingQuery(5);

  return (
    <div>
      <Content style={{ padding: "24px", maxWidth: 1200, margin: "0 auto" }}>
        <Title level={2}>
          <FireOutlined /> Топ продаваемых товаров
        </Title>
        <List
          grid={{ gutter: 16, column: 3 }}
          dataSource={topProducts?.response}
          renderItem={(item) => (
            <List.Item>
              <Card
                title={<Link href={`/product/${item.id}`}>{item.name}</Link>}
              >
                <div>Цена: {item.price}</div>
                <div style={{ marginTop: 8 }}>
                  Оценка: {item.rating === 0 ? "-" : item.rating}
                </div>
              </Card>
            </List.Item>
          )}
        />

        <Title level={2} style={{ marginTop: 40 }}>
          <ShopOutlined /> Топ организаций
        </Title>
        <List
          grid={{ gutter: 16, column: 3 }}
          dataSource={topOrganizations?.response}
          renderItem={(item) => (
            <List.Item>
              <Card
                title={
                  <Link href={`/organization/${item.id}`}>{item.name}</Link>
                }
              >
                <div style={{ marginTop: 8 }}>
                  Оценка: {item.ratingValue === 0 ? "-" : item.ratingValue}
                </div>
              </Card>
            </List.Item>
          )}
        />

        <Title level={2} style={{ marginTop: 40 }}>
          <InfoCircleOutlined /> О портале
        </Title>
        <Paragraph>
          Добро пожаловать на наш портал! Здесь вы найдете лучшие фермерские и
          локальные продукты от проверенных организаций. Мы объединяем продавцов
          и покупателей, чтобы обеспечить качественные и свежие продукты.
        </Paragraph>
      </Content>
    </div>
  );
}
