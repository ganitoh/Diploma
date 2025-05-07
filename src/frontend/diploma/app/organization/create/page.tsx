"use client";

import React from 'react';
import { useRouter } from 'next/navigation';
import { Button, Card, Form, Input, message } from 'antd';
import { useOrganizationMutation } from '@/app/hooks/organization/useOrganizationMutation';


interface OrganizationFormValues {
    name: string;
    description?: string;
    inn: string;
    email: string;
    legalAddress: string;
}

export default function CreateOrganizationPage() {
    const [form] = Form.useForm();
    const router = useRouter();

    const { createOrganizationMutation } = useOrganizationMutation()

    const onFinish = async (values: OrganizationFormValues) => {
        createOrganizationMutation.mutateAsync({
            name: values.name,
            description: values.description ?? "",
            inn: values.inn,
            email: values.email,
            legalAddress: values.legalAddress
        })

        router.push("/")
    };

    return (
        <div style={{ maxWidth: 600, margin: '0 auto', padding: 24 }}>
            <Card title="Создание организации">

                <Form
                    form={form}
                    layout="vertical"
                    onFinish={onFinish}
                    initialValues={{}}
                >
                    <Form.Item
                        label="Наименование"
                        name="name"
                        rules={[{ required: true, message: 'Введите наименование' }]}
                    >
                        <Input />
                    </Form.Item>

                    <Form.Item
                        label="ИНН"
                        name="inn"
                        rules={[
                            { required: true, message: 'Введите ИНН' },
                            { pattern: /^\d{10}$/, message: 'ИНН должен содержать 10 цифр' },
                        ]}
                    >
                        <Input />
                    </Form.Item>

                    <Form.Item
                        label="Email"
                        name="email"
                        rules={[
                            { required: true, message: 'Введите email' },
                            { type: 'email', message: 'Некорректный email' },
                        ]}
                    >
                        <Input />
                    </Form.Item>

                    <Form.Item
                        label="Юридический адрес"
                        name="legalAddress"
                        rules={[{ required: true, message: 'Введите юридический адрес' }]}
                    >
                        <Input />
                    </Form.Item>

                    <Form.Item label="Описание" name="description">
                        <Input.TextArea rows={4} />
                    </Form.Item>

                    <Form.Item>
                        <Button type="primary" htmlType="submit">
                            Создать организацию
                        </Button>
                    </Form.Item>
                </Form>
            </Card>
        </div>
    );
}
