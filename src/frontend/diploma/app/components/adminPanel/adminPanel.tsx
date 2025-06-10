import { Button, Card, Form, Input, Typography } from "antd";
import { useRouter } from "next/navigation";
import styles from "./page.module.css";
import { useState } from "react";
import { IOrganiaiton } from "@/app/models/organization";

const { Title, Text } = Typography;

interface IAdminPanelProps {
  userId?: string;
}

export const AdminPanel = (props: IAdminPanelProps) => {
  const [organizations, setOrganizations] = useState<IOrganiaiton[]>([]);
  const [loading, setLoading] = useState<boolean>(false);

  useEffect(() => {
    fetchOrganizations();
  }, []);

  const fetchOrganizations = async () => {
    setLoading(true);
    try {
      const response = await axios.get("/api/organization/all");
      setOrganizations(response.data.response);
    } catch (error) {
      message.error("Не удалось загрузить организации");
    } finally {
      setLoading(false);
    }
  };

  const handleVerify = async (id: number) => {
    try {
      await axios.patch(`/api/organization/verify/${id}`);
      message.success("Организация подтверждена");
      fetchOrganizations(); // обновить список
    } catch (error) {
      message.error("Ошибка при подтверждении");
    }
  };

  const columns = [
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
    },
    {
      title: "Адрес",
      dataIndex: "legalAddress",
    },
    {
      title: "Статус",
      dataIndex: "isApproval",
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
      render: (_: any, record: IOrganization) =>
        record.isApproval ? null : (
          <Popconfirm
            title="Подтвердить организацию?"
            onConfirm={() => handleVerify(record.id)}
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

  return (
    <div style={{ padding: 24 }}>
      <h2>Список организаций</h2>
      <Table
        rowKey="id"
        columns={columns}
        dataSource={organizations}
        loading={loading}
        pagination={{ pageSize: 10 }}
      />
    </div>
  );
};
