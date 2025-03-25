"use client";

import { Button, Card, Form, Input } from "antd";
import { LockOutlined, MailOutlined } from "@ant-design/icons";
import { useState } from "react";
import { useRouter } from "next/navigation";
import style from "./page.module.css"
import { ConfirmEmail } from "../components/confirmEmail/confirmEmail";

export default function RegistrationPage() {

    const [loading, setLoading] = useState(false);
    const [email, setEmail] = useState<string>("gan@yandex.ru");
    const [emailCode, setEmailCode] = useState<number>(123);
    
    const [isEmailCodeConfirmed, setIsEmailCodeConfirmed ] = useState<boolean>(false)
    const router = useRouter();
  
    const onFinish = async () => {
      setLoading(true);
      //Отправка данных
      setIsEmailCodeConfirmed(true)
      setLoading(false);
    };

    return(
      <div className={style.container}>
        {isEmailCodeConfirmed ? <ConfirmEmail email={email} emailCode={emailCode}/> : 
        (<Card title="Регистрация" className={style.card}>
          <Form 
            name="register"
            layout="vertical"
            onFinish={onFinish}
            autoComplete="off"
          >
            <Form.Item
              name="email"
              label="Email"
              rules={[
                { required: true, message: "Введите email" },
                { type: "email", message: "Введите корректный email" },
              ]}
            >
              <Input  prefix={<MailOutlined />} placeholder="example@mail.com" />
            </Form.Item>
  
            <Form.Item
              name="password"
              label="Пароль"
              rules={[
                { required: true, message: "Введите пароль" },
                { min: 6, message: "Минимум 6 символов" },
              ]}
            >
              <Input.Password prefix={<LockOutlined />} placeholder="••••••" />
            </Form.Item>
  
            <Form.Item
              name="passwordRepeat"
              label="Повторите пароль"
              rules={[
                { required: true, message: "Повторите пароль" },
                { min: 6, message: "Минимум 6 символов" },
              ]}
            >
              <Input.Password prefix={<LockOutlined />} placeholder="••••••" />
            </Form.Item>
  
            <Form.Item>
              <Button type="primary" htmlType="submit" loading={loading} block>
                Зарегистрироваться
              </Button>
            </Form.Item>
          </Form>
        </Card>)}
      </div>
    );
} 