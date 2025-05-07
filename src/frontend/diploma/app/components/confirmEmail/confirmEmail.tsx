import { Button, Card, Form, Input, Typography } from "antd";
import { useRouter } from "next/navigation";
import { useState } from "react";
import styles from "./page.module.css"

const { Title, Text } = Typography;


interface IConfirmEmailProps{
    email: string
}

export const ConfirmEmail = (props: IConfirmEmailProps) => {
    const router = useRouter();
    const [loading, setLoading] = useState(false);
    const [verifying, setVerifying] = useState(false);
    const [code, setCode] = useState("");

    const resendEmail = async () => {
        setLoading(true);
        setLoading(false);
    };

    const onSubmit = async () => {
      };

      return (
        <div className={styles.container}>
          <Card className={styles.card}>
            <Title level={3}>Подтвердите ваш Email</Title>
            <Text>
              Мы отправили 6-значный код на <b>{props.email}</b>. Введите его ниже, чтобы подтвердить учетную запись.
            </Text>
    
            <Form onFinish={onSubmit} style={{ marginTop: 20 }}>
              <Form.Item
                rules={[{ required: true, len: 6, message: "Введите 6-значный код" }]}
              >
                <Input
                  maxLength={6}
                  value={code}
                  onChange={(e) => setCode(e.target.value)}
                  placeholder="Введите код"
                  style={{ textAlign: "center", fontSize: "18px", letterSpacing: "5px" }}
                />
              </Form.Item>
              <Form.Item>
                <Button type="primary" htmlType="submit" loading={verifying} block>
                  Подтвердить Email
                </Button>
              </Form.Item>
            </Form>
    
            <Text>Не получили код?</Text>
            <Button type="link" onClick={resendEmail} loading={loading}>
              Отправить снова
            </Button>
          </Card>
        </div>
      );
}