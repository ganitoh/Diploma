import {
  Button,
  Card,
  Form,
  Input,
  Popconfirm,
  Space,
  Table,
  Tag,
  Typography,
} from "antd";
import { useRouter } from "next/navigation";
import styles from "./page.module.css";
import { useEffect, useState } from "react";
import { IOrganiaiton } from "@/app/models/organization";
import {
  useGetNotVerifyOrganizationQuery,
  useGetPagedOrganizationQuery,
} from "@/app/hooks/organization/useOrganizationQuery";
import { adminOrganizationColumns } from "./columns";
import { useOrganizationMutation } from "@/app/hooks/organization/useOrganizationMutation";
import { ColumnsType } from "antd/es/table";
import { CheckCircleOutlined, DeleteOutlined } from "@ant-design/icons";
import { useUserMutation } from "@/app/hooks/user/useUserMutation";

const { Title, Text } = Typography;

interface IAdminPanelProps {
  userId?: string;
}

export const AdminPanel = (props: IAdminPanelProps) => {
  const [currentPage, setCurrentPage] = useState(1);
  const [pageSize, setPageSuze] = useState<number>(10);

  const router = useRouter();

  const { logoutMutation } = useUserMutation();
  const { data, isLoading, refetch } = useGetNotVerifyOrganizationQuery();
  const {
    data: pagedData,
    isLoading: isLoadingPagged,
    refetch: paggedRefetch,
  } = useGetPagedOrganizationQuery({
    pageNumber: currentPage,
    pageSize: pageSize,
  });

  const { verifyOrganizationMutation } = useOrganizationMutation();

  const handleVerify = async (id: number) => {
    await verifyOrganizationMutation.mutateAsync(id).then((res) => {
      if (res.succeeded) {
        refetch();
      }
    });
  };

  const handleNotVerify = async (id: number) => {
    await verifyOrganizationMutation.mutateAsync(id).then((res) => {
      if (res.succeeded) {
        refetch();
      }
    });
  };

  const handleLogout = async () => {
    await logoutMutation.mutateAsync().then((res) => {
      if (res.succeeded) {
        localStorage.removeItem("userId");
        router.push("/login");
      }
    });
  };

  const adminAdditionalOrganizationColumns: ColumnsType<IOrganiaiton> = [
    {
      title: "Статус",
      dataIndex: "isApproval",
      align: "center",
      render: (isApproval: boolean) =>
        isApproval ? (
          <Tag icon={<CheckCircleOutlined />} color="success">
            Подтверждена
          </Tag>
        ) : (
          <Tag color="warning">Не подтверждена</Tag>
        ),
    },
    {
      title: "Действия",
      dataIndex: "actions",
      align: "center",
      render: (_: any, record: IOrganiaiton) =>
        record.isApproval ? null : (
          <Popconfirm
            title="Подтвердить отмену организацию?"
            onConfirm={() => handleNotVerify(record.id)}
            okText="Да"
            cancelText="Нет"
          >
            <Button type="primary" icon={<CheckCircleOutlined />} size="small">
              Подтвердить
            </Button>
          </Popconfirm>
        ),
    },
  ];

  const adminAdditionalAllOrganizationColumns: ColumnsType<IOrganiaiton> = [
    {
      title: "Статус",
      dataIndex: "isApproval",
      align: "center",
      render: (isApproval: boolean) =>
        isApproval ? (
          <Tag icon={<CheckCircleOutlined />} color="success">
            Подтверждена
          </Tag>
        ) : (
          <Tag color="warning">Не подтверждена</Tag>
        ),
    },
    {
      title: "Действия",
      dataIndex: "actions",
      align: "center",
      render: (_: any, record: IOrganiaiton) =>
        !record.isApproval ? null : (
          <Popconfirm
            title="Отменить подтверждение?"
            onConfirm={() => handleNotVerify(record.id)}
            okText="Да"
            cancelText="Нет"
          >
            <Button type="default" icon={<CheckCircleOutlined />} size="small">
              Отменить подтверждение
            </Button>
          </Popconfirm>
        ),
    },
  ];

  return (
    <div style={{ padding: 24 }}>
      <div
        style={{
          display: "flex",
          justifyContent: "space-between",
          alignItems: "center",
          marginBottom: 16,
        }}
      >
        <h2 style={{ margin: 0 }}>Список не подтвержденных организаций</h2>
        <Button danger onClick={handleLogout}>
          Выйти
        </Button>
      </div>
      <Table
        rowKey="id"
        columns={[
          ...adminOrganizationColumns,
          ...adminAdditionalOrganizationColumns,
        ]}
        dataSource={data?.response}
        loading={isLoading}
        pagination={false}
      />
      <div style={{ marginTop: 48 }}>
        <Title level={4}>Все организации</Title>
        <Table
          rowKey="id"
          columns={[
            ...adminOrganizationColumns,
            ...adminAdditionalAllOrganizationColumns,
          ]}
          dataSource={pagedData?.response.entities}
          loading={isLoadingPagged}
          pagination={{
            current: currentPage,
            pageSize: pageSize,
            total: pagedData?.response.totalCount || 0,
            onChange: (page) => setCurrentPage(page),
            showSizeChanger: false,
          }}
        />
      </div>
    </div>
  );
};
