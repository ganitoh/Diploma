"use client";

import { Button, Card, Form, Input } from "antd";
import { LockOutlined, MailOutlined } from "@ant-design/icons";
import { useState } from "react";
import { useRouter } from "next/navigation";
import style from "./page.module.css"
import { useUserMutation } from "../hooks/user/useUserMutation";
import Link from "antd/es/typography/Link";

interface LoginFromValues{
  email: string
  password: string
}

export default function LoginPage() {

    const [loading, setLoading] = useState(false);
    const [form] = Form.useForm<LoginFromValues>()
    const { loginMutation } = useUserMutation()
    const router = useRouter();
  
    const onFinish = () => {
      setLoading(true);
      loginMutation.mutateAsync({
        email : form.getFieldValue("email"),
        password: form.getFieldValue("password")
      }).then(res => {
        console.log(res)
        if (res.succeeded){
          localStorage.setItem("userId", res.response)
          router.push(`/profile/${res.response}`)
        }
      })

      setLoading(false);
    };

    return(
      <div className={style.container}>
        <Card title="Войти в систему" className={style.card}>
          <Form 
            form={form}
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
  
            <Form.Item>
              <Button type="primary" htmlType="submit" loading={loading} block>
                Войти
              </Button>
            <Link href="/registration">Регистарция</Link>
            </Form.Item>
          </Form>
        </Card>
      </div>
    );
} 