"use client";

import { Button, Card, Form, Input } from "antd";
import { LockOutlined, MailOutlined } from "@ant-design/icons";
import { useState } from "react";
import { useRouter } from "next/navigation";
import style from "./page.module.css"
import { ConfirmEmail } from "../components/confirmEmail/confirmEmail";
import { useUserMutation } from "../hooks/user/useUserMutation";
import { log } from "console";

interface RegistraitonFromValues{
  email: string
  password: string
}

export default function RegistrationPage() {

    const [loading, setLoading] = useState(false);
    const [form] = Form.useForm<RegistraitonFromValues>()
    const [email, setEmail] = useState<string>();
    const { registrationMutation } = useUserMutation()
    
    const [isEmailCodeConfirmedProcess, setIsEmailCodeConfirmedProcess ] = useState<boolean>(false)
    const router = useRouter();
  
    const onFinish = () => {
      setLoading(true);
      registrationMutation.mutateAsync({
        email : form.getFieldValue("email"),
        password: form.getFieldValue("password")
      }).then(res => {
        console.log(res)
        if (res.succeeded){
          router.push("/")
        }
      })

      //TODO: надо доделть подтверждение почты
      //setIsEmailCodeConfirmed(true)
      setLoading(false);
    };

    return(
      <div className={style.container}>
        {isEmailCodeConfirmedProcess && email ? <ConfirmEmail email={email}/> : 
        (<Card title="Регистрация" className={style.card}>
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