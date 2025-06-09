"use client";

import { useParams, useRouter } from "next/navigation";
import { useEffect } from "react";
import { Button, Card, Col, Descriptions, Row, Typography, Alert } from "antd";
import Link from "next/link";
import { useGetOrganizationByIdQuery } from "@/app/hooks/organization/useOrganizationQuery";
import { IProduct } from "@/app/models/product";
import { CheckCircleOutlined } from "@ant-design/icons";
import { AddRatingForm } from "@/app/components/addRating/addRatingForm";

const { Title } = Typography;

export default function OrganizationPage() {
  const params = useParams();
  const router = useRouter();
  const organizationId = params.id ? Number(params.id) : 0;

  const { data, isLoading } = useGetOrganizationByIdQuery(organizationId);

  useEffect(() => {
    const userId = localStorage.getItem("userId");
    if (!userId) {
      router.push("/login");
    }
  }, [router]);

  if (isLoading) {
    return (
      <div style={{ textAlign: "center", marginTop: 50 }}>
        <Title level={4}>Загрузка информации об организации...</Title>
      </div>
    );
  }

  if (!data?.succeeded) {
    return (
      <div style={{ textAlign: "center", marginTop: 50 }}>
        <Title level={3}>Организация не найдена</Title>
        <Button type="primary" onClick={() => router.push("/organization")}>
          Назад к списку организаций
        </Button>
      </div>
    );
  }

  const organization = data.response;

  return (
    <div style={{ padding: 24, maxWidth: 1000, margin: "0 auto" }}>
      <Card
        title={`Организация: ${organization.name}`}
        style={{ marginBottom: 24 }}
      >
        <Descriptions bordered column={1}>
          <Descriptions.Item label="ИНН">{organization.inn}</Descriptions.Item>
          <Descriptions.Item label="Email">
            {organization.email}
          </Descriptions.Item>
          <Descriptions.Item label="Юр. адрес">
            {organization.legalAddress}
          </Descriptions.Item>
          <Descriptions.Item label="Описание">
            {organization.description}
          </Descriptions.Item>
          <Descriptions.Item label="Подтверждена">
            {organization.isApproval ? (
              <span style={{ color: "green", fontWeight: 500 }}>
                Подтверждена <CheckCircleOutlined />
              </span>
            ) : (
              "Нет"
            )}
          </Descriptions.Item>
        </Descriptions>
      </Card>

      <Title level={3}>Продаваемые товары</Title>

      {organization.products.length === 0 ? (
        <Alert
          message="Организация пока не продаёт товары."
          type="info"
          showIcon
        />
      ) : (
        <Row gutter={[16, 16]}>
          {organization.products.map((product: IProduct) => (
            <Col span={8} key={product.id}>
              <Card
                title={
                  <Link href={`/product/${product.id}`}>{product.name}</Link>
                }
                extra={product.price + " ₽"}
              >
                <p>Оценка: {product.rating === 0 ? "-" : product.rating}</p>
                <p>Описание: {product.description}</p>
              </Card>
            </Col>
          ))}
        </Row>
      )}
      <div>
        <AddRatingForm isProduct={false} entityId={data.response.id} />
      </div>
    </div>
  );
}
