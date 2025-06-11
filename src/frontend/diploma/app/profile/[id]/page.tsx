"use client";

import { useGetOrganizationByUserIdQuery } from "@/app/hooks/organization/useOrganizationQuery";
import { useParams, useRouter } from "next/navigation";
import {
  Button,
  Card,
  Descriptions,
  Drawer,
  Empty,
  Space,
  Typography,
  Alert,
  Col,
  Table,
  Popconfirm,
} from "antd";
import { DeleteOutlined } from "@ant-design/icons";
import { Key, useEffect, useState } from "react";
import { AddProductForm } from "@/app/components/addProductForm/addProductForm";
import { useCheckRoleUserQuery } from "@/app/hooks/user/useUserQuery";
import { AdminPanel } from "@/app/components/adminPanel/adminPanel";
import { productColumns, sellOrderColumns } from "./columns";
import { useProductMutation } from "@/app/hooks/product/useProductMutation";
import { useUserMutation } from "@/app/hooks/user/useUserMutation";

const { Title } = Typography;

export default function ProfilePage() {
  const [isAddProductModalOpen, setIsAddProductModalOpen] =
    useState<boolean>(false);

  const [productId, setProductId] = useState<number | undefined>();
  const [selectedRowKeys, setSelectedRowKeys] = useState<Key[]>([]);

  const { deleteProductMutation } = useProductMutation();
  const { logoutMutation } = useUserMutation();

  const params = useParams();
  const router = useRouter();
  const id = params.id;

  const { data: resultCheck } = useCheckRoleUserQuery("Admin");
  const { data, isLoading, refetch } = useGetOrganizationByUserIdQuery(
    id?.toString() ?? ""
  );

  useEffect(() => {
    if (data?.response.id) {
      localStorage.setItem("organizationId", data.response.id.toString());
    }
  }, [data]);

  const closeDrawer = () => {
    setIsAddProductModalOpen(false);
    setProductId(undefined);
  };

  const onSelectChange = (newSelectedRowKeys: Key[]) => {
    setSelectedRowKeys(newSelectedRowKeys);
  };

  const rowSelection = {
    selectedRowKeys,
    onChange: onSelectChange,
  };

  const handleDelete = async () => {
    await deleteProductMutation.mutateAsync(selectedRowKeys as number[]);
    refetch();
    setSelectedRowKeys([]);
  };

  const handleLogout = async () => {
    await logoutMutation.mutateAsync().then((res) => {
      if (res.succeeded) {
        localStorage.removeItem("userId");
        localStorage.removeItem("organizationId");
        router.push("/login");
      }
    });
  };

  return (
    <div>
      {resultCheck?.response ? (
        <AdminPanel />
      ) : (
        <div>
          {!data?.succeeded && (
            <div style={{ textAlign: "center", marginTop: 50 }}>
              <Title level={3}>Организация не найдена</Title>
              <Button
                type="primary"
                onClick={() => router.push("/organization/create")}
              >
                Добавить организацию
              </Button>
            </div>
          )}
          {data?.succeeded && data.response && (
            <div style={{ padding: 24, maxWidth: 1000, margin: "0 auto" }}>
              <Card
                title={
                  <Space
                    style={{ justifyContent: "space-between", width: "100%" }}
                  >
                    <span>Организация: {data.response.name}</span>
                    <Button danger onClick={handleLogout}>
                      Выйти
                    </Button>
                  </Space>
                }
                style={{ marginBottom: 24 }}
              >
                <Descriptions
                  title="Информация об организации"
                  bordered
                  column={1}
                >
                  <Descriptions.Item label="ИНН">
                    {data.response.inn}
                  </Descriptions.Item>
                  <Descriptions.Item label="Email">
                    {data.response.email}
                  </Descriptions.Item>
                  <Descriptions.Item label="Юридический адрес">
                    {data.response.legalAddress}
                  </Descriptions.Item>
                  <Descriptions.Item label="Описание">
                    {data.response.description}
                  </Descriptions.Item>
                  <Descriptions.Item label="Прошла верефикацию">
                    {data.response.isApproval ? "Да" : "Нет"}
                  </Descriptions.Item>
                </Descriptions>
              </Card>
              {!data.response.isApproval && (
                <Col span={24} style={{ marginBottom: 16 }}>
                  <Alert
                    message="Организация еще не прошла проверку, вы не моежет добавит товар"
                    type="warning"
                    showIcon
                  />
                </Col>
              )}
              <Card
                title={
                  <Space
                    style={{
                      display: "flex",
                      justifyContent: "space-between",
                      width: "100%",
                    }}
                  >
                    <span>Товары</span>
                    <Button
                      type="primary"
                      disabled={!data.response.isApproval}
                      onClick={() => setIsAddProductModalOpen(true)}
                    >
                      Добавить товар
                    </Button>
                  </Space>
                }
                style={{ marginBottom: 24 }}
              >
                {data.response.products.length > 0 ? (
                  <>
                    <Space style={{ marginBottom: 16 }}>
                      <Popconfirm
                        title="Вы уверены, что хотите удалить выбранные товары?"
                        onConfirm={handleDelete}
                        okText="Да"
                        cancelText="Нет"
                        disabled={selectedRowKeys.length === 0}
                      >
                        <Button
                          type="primary"
                          danger
                          icon={<DeleteOutlined />}
                          disabled={selectedRowKeys.length === 0}
                        />
                      </Popconfirm>
                    </Space>

                    <Table
                      rowKey="id"
                      columns={productColumns}
                      dataSource={data.response.products}
                      pagination={false}
                      rowSelection={rowSelection}
                      onRow={(record) => ({
                        onClick: () => {
                          setIsAddProductModalOpen(true);
                          setProductId(record.id);
                        },
                      })}
                    />
                  </>
                ) : (
                  <Empty description="Нет товаров" />
                )}
              </Card>

              <Card title="Заказы на продажу" style={{ marginBottom: 24 }}>
                {data.response.sellOrders.length > 0 ? (
                  <>
                    <Table
                      rowKey="id"
                      columns={sellOrderColumns}
                      dataSource={data.response.sellOrders}
                      pagination={false}
                      onRow={(record) => ({
                        onClick: () => {
                          router.push(`/order/${record.id}`);
                        },
                      })}
                    />
                  </>
                ) : (
                  <Empty description="Нет заказов на продажу" />
                )}
              </Card>

              <Card title="Заказы на покупку">
                {data.response.buyOrders.length > 0 ? (
                  <>
                    <Table
                      rowKey="id"
                      columns={sellOrderColumns}
                      dataSource={data.response.buyOrders}
                      pagination={false}
                      onRow={(record) => ({
                        onClick: () => {
                          router.push(`/order/${record.id}`);
                        },
                      })}
                    />
                  </>
                ) : (
                  <Empty description="Нет заказов на покупку" />
                )}
              </Card>
              <Drawer
                title="Добавление товара"
                width="60%"
                onClose={closeDrawer}
                open={isAddProductModalOpen}
                extra={
                  <Space>
                    <Button onClick={closeDrawer}>Отмена</Button>
                  </Space>
                }
              >
                <AddProductForm
                  organizationId={data.response.id}
                  onClose={() => {
                    setProductId(undefined);
                    setIsAddProductModalOpen(false);
                    refetch();
                  }}
                  productId={productId}
                />
              </Drawer>
            </div>
          )}
        </div>
      )}
    </div>
  );
}
