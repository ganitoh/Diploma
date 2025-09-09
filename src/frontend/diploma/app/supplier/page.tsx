"use client";

import { useEffect, useState } from "react";
import { useGetPagedOrganizationQuery } from "../hooks/organization/useOrganizationQuery";
import style from "./page.module.css";
import Table, { ColumnsType } from "antd/es/table";
import { IOrganiaiton } from "../models/organization";
import Title from "antd/es/typography/Title";
import {
  Button,
  Card,
  Collapse,
  Drawer,
  Form,
  Input,
  InputNumber,
  Modal,
  Space,
} from "antd";
import { useOrganizationMutation } from "../hooks/organization/useOrganizationMutation";
import { getOrganizationById } from "../http/organization";
import { IProduct, MeasurementType } from "../models/product";
import { AddProductForm } from "../components/addProductForm/addProductForm";
import { formatMoney } from "../utils/format";
import { getPagedProduct } from "../http/products";

interface IExternalOrganizaiontFromValues {
  name: string;
  description?: string;
  inn: string;
  email: string;
  legalAddress: string;
}

export default function SupplierOrganizationPage() {
  const [currentPage, setCurrentPage] = useState<number>(1);
  const [pageSize, setPageSuze] = useState<number>(10);
  const [currentProductPage, setCurrentProductPage] = useState<number>(1);
  const [pageProductSize, setPageProductSize] = useState<number>(3);
  const [isProductModalOpen, setIsProductModalOpen] = useState<boolean>(false);
  const [orgProducts, setOrgProducts] = useState<IProduct[] | undefined>(
    undefined
  );
  const [orgProductsCount, setOrgProductsCount] = useState<number>(0);
  const [selectOrganizationId, setSelectOrganizationId] = useState<
    number | undefined
  >(undefined);
  const [isAddExtrnalOrgModalOpen, setIsAddExtrnalOrgModalOpen] =
    useState<boolean>(false);

  const { createOrganizationMutation } = useOrganizationMutation();

  const [form] = Form.useForm<IExternalOrganizaiontFromValues>();
  const [productForm] = Form.useForm<IExternalOrganizaiontFromValues>();

  const {
    data: pagedData,
    isLoading: isLoadingPagged,
    refetch: paggedRefetch,
  } = useGetPagedOrganizationQuery({
    pageNumber: currentPage,
    pageSize: pageSize,
    isExternal: true,
  });

  useEffect(() => {
    if (selectOrganizationId) {
      getOrganizationById(selectOrganizationId).then((res) => {
        form.setFieldsValue({
          name: res.response.name,
          description: res.response.description,
          inn: res.response.inn,
          email: res.response.email,
          legalAddress: res.response.legalAddress,
        });
      });

      getPagedProduct({
        pageNumber: currentProductPage,
        pageSize: pageProductSize,
        organizationId: selectOrganizationId,
      }).then((res) => {
        if (res.succeeded) {
          setOrgProducts(res.response.entities);
          setOrgProductsCount(res.response.totalCount);
        }
      });
    } else {
      form.resetFields();
    }
  }, [selectOrganizationId]);

  const onFinish = async (values: IExternalOrganizaiontFromValues) => {
    createOrganizationMutation.mutateAsync({
      name: values.name,
      description: values.description ?? "",
      inn: values.inn,
      email: values.email,
      legalAddress: values.legalAddress,
      isExternal: true,
    });

    await paggedRefetch();
    form.resetFields();
    setIsAddExtrnalOrgModalOpen(false);
  };

  const closeDrawer = () => {
    setIsAddExtrnalOrgModalOpen(false);
    setSelectOrganizationId(undefined);
  };

  const closeOrderModal = () => {
    setIsProductModalOpen(false);
  };

  const adminOrganizationColumns: ColumnsType<IOrganiaiton> = [
    {
      title: "Название",
      dataIndex: "name",
    },
    {
      title: "ИНН",
      dataIndex: "inn",
    },
    {
      title: "Email",
      dataIndex: "email",
      key: "totalPrice",
      align: "center",
    },
    {
      title: "Адрес",
      dataIndex: "legalAddress",
      key: "totalPrice",
      align: "center",
    },
  ];

  const productColumns: ColumnsType<IProduct> = [
    {
      title: "Название",
      dataIndex: "name",
      key: "name",
      align: "center",
    },
    {
      title: "Описание",
      dataIndex: "description",
      key: "description",
      align: "center",
      render: (description: string) => (description ? description : "-"),
    },
    {
      title: "Цена (₽)",
      dataIndex: "price",
      key: "price",
      align: "center",
      render: (price: number) => `${formatMoney(price)} ₽`,
    },
  ];

  return (
    <div>
      <div style={{ marginTop: 48 }}>
        <Card
          title={
            <Space
              style={{
                display: "flex",
                justifyContent: "space-between",
                width: "100%",
              }}
            >
              <span>Все внешние организации</span>
              <Button
                type="primary"
                onClick={() => setIsAddExtrnalOrgModalOpen(true)}
              >
                Добавить внешнего поставщика
              </Button>
            </Space>
          }
          style={{ marginBottom: 24 }}
        >
          <Table
            rowKey="id"
            columns={adminOrganizationColumns}
            dataSource={pagedData?.response.entities}
            loading={isLoadingPagged}
            onRow={(record) => ({
              onClick: () => {
                setIsAddExtrnalOrgModalOpen(true);
                setSelectOrganizationId(record.id);
              },
            })}
            pagination={{
              current: currentPage,
              pageSize: pageSize,
              total: pagedData?.response.totalCount || 0,
              onChange: (page) => setCurrentPage(page),
              showSizeChanger: false,
            }}
          />
        </Card>
      </div>
      <Drawer
        title="Добавление внешнего поставщика"
        width="50%"
        onClose={closeDrawer}
        open={isAddExtrnalOrgModalOpen}
        extra={
          <Space>
            <Button onClick={closeDrawer}>Отмена</Button>
          </Space>
        }
      >
        <div>
          <Form
            form={form}
            layout="vertical"
            onFinish={onFinish}
            initialValues={{}}
          >
            <Form.Item
              label="Наименование"
              name="name"
              rules={[{ required: true, message: "Введите наименование" }]}
            >
              <Input />
            </Form.Item>

            <Form.Item
              label="ИНН"
              name="inn"
              rules={[
                { required: true, message: "Введите ИНН" },
                {
                  pattern: /^\d{10}$/,
                  message: "ИНН должен содержать 10 цифр",
                },
              ]}
            >
              <Input />
            </Form.Item>

            <Form.Item
              label="Email"
              name="email"
              rules={[
                { required: true, message: "Введите email" },
                { type: "email", message: "Некорректный email" },
              ]}
            >
              <Input />
            </Form.Item>

            <Form.Item
              label="Юридический адрес"
              name="legalAddress"
              rules={[{ required: true, message: "Введите юридический адрес" }]}
            >
              <Input />
            </Form.Item>

            <Form.Item label="Описание" name="description">
              <Input.TextArea rows={4} />
            </Form.Item>

            <Form.Item>
              <Button type="primary" htmlType="submit">
                {selectOrganizationId == null
                  ? "Создать организацию"
                  : "сохранить"}
              </Button>
            </Form.Item>
          </Form>
          {selectOrganizationId ? (
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
                    onClick={() => {
                      setIsProductModalOpen(true);
                    }}
                  >
                    Добавить товар
                  </Button>
                </Space>
              }
              style={{ marginBottom: 24 }}
            >
              <Table
                rowKey="id"
                columns={productColumns}
                dataSource={orgProducts}
                onRow={(record) => ({
                  onClick: () => {},
                })}
                pagination={{
                  current: currentProductPage,
                  pageSize: pageProductSize,
                  total: orgProductsCount,
                  onChange: (page) => setCurrentProductPage(page),
                  showSizeChanger: false,
                }}
              />
            </Card>
          ) : (
            <></>
          )}
        </div>
      </Drawer>
      {/* Модальное окно для создания товара*/}
      {selectOrganizationId ? (
        <Modal
          title="Создание заказа"
          open={isProductModalOpen}
          onCancel={closeOrderModal}
          footer={null}
          destroyOnClose
        >
          <AddProductForm
            onClose={() => {
              setIsProductModalOpen(false);
              getPagedProduct({
                pageNumber: currentProductPage,
                pageSize: pageProductSize,
                organizationId: selectOrganizationId,
              }).then((res) => {
                if (res.succeeded) {
                  setOrgProducts(res.response.entities);
                  setOrgProductsCount(res.response.totalCount);
                }
              });
            }}
            organizationId={selectOrganizationId}
          ></AddProductForm>
        </Modal>
      ) : (
        <></>
      )}
    </div>
  );
}
