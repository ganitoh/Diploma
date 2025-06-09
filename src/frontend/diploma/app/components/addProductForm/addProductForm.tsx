import { Button, Form, Input, InputNumber, Select } from "antd";
import { useRouter } from "next/navigation";
import styles from "./page.module.css";
import {
  ICreateProduct,
  IProduct,
  MeasurementType,
} from "@/app/models/product";
import { useProductMutation } from "@/app/hooks/product/useProductMutation";
import { useGetProductByIdQuery } from "@/app/hooks/product/useProductQuery";
import { useEffect, useState } from "react";
import { getProductById } from "@/app/http/products";

interface IProductFromValues {
  name: string;
  price: number;
  availableCount: number;
  measurementType: MeasurementType;
  description: string;
}

interface IAddProductFormProps {
  organizationId: number;
  onClose(): void;
  productId?: number;
}

const MeasurementTypeLabels: Record<MeasurementType, string> = {
  [MeasurementType.Thing]: "Шт",
  [MeasurementType.Gram]: "Грамм",
  [MeasurementType.Kg]: "Килограмм",
  [MeasurementType.Tones]: "Тонны",
};

export const AddProductForm = (props: IAddProductFormProps) => {
  const router = useRouter();
  const [product, setProduct] = useState<IProduct | undefined>(undefined);
  const [form] = Form.useForm<IProductFromValues>();

  const { createProductMutation, updateProductMutation } = useProductMutation();

  useEffect(() => {
    if (props.productId) {
      getProductById(props.productId).then((res) => {
        setProduct(res.response);
        form.setFieldsValue({
          name: res.response.name,
          price: res.response.price,
          availableCount: res.response.availableCount,
          measurementType: res.response.measurementType,
          description: res.response.description,
        });
      });
    } else {
      setProduct(undefined);
      form.resetFields();
    }
  }, [props.productId]);

  const onFinish = async (values: IProductFromValues) => {
    const productData: ICreateProduct = {
      sellOrganizationId: props.organizationId,
      name: values.name,
      price: values.price,
      availableCount: values.availableCount,
      description: values.description,
      measurementType: values.measurementType,
    };

    if (props.productId) {
      updateProductMutation
        .mutateAsync({
          id: props.productId,
          ...productData,
        })
        .then((res) => {
          if (res.succeeded) {
            form.resetFields();
            props.onClose();
          }
        });
    } else {
      createProductMutation.mutateAsync(productData).then((res) => {
        if (res.succeeded) {
          form.resetFields();
          props.onClose();
        }
      });
    }
  };

  return (
    <div>
      <Form form={form} layout="vertical" onFinish={onFinish}>
        <Form.Item
          label="Название"
          name="name"
          rules={[{ required: true, message: "Введите название товара" }]}
        >
          <Input />
        </Form.Item>

        <Form.Item
          label="Цена"
          name="price"
          rules={[{ required: true, message: "Введите цену" }]}
        >
          <InputNumber style={{ width: "100%" }} min={0} />
        </Form.Item>
        <Form.Item
          label="Доступное количество"
          name="availableCount"
          rules={[{ required: true, message: "Введите доступное количество" }]}
        >
          <InputNumber style={{ width: "100%" }} min={0} />
        </Form.Item>
        <Form.Item
          label="Единица измерения"
          name="measurementType"
          rules={[{ required: true, message: "Выберите единицу измерения" }]}
        >
          <Select placeholder="Выберите тип измерения">
            {Object.entries(MeasurementTypeLabels).map(([value, label]) => (
              <Select.Option key={value} value={value}>
                {label}
              </Select.Option>
            ))}
          </Select>
        </Form.Item>
        <Form.Item label="Описание" name="description">
          <Input.TextArea rows={3} />
        </Form.Item>
        <Form.Item>
          <Button type="primary" htmlType="submit">
            {props.productId ? "Сохранить" : "Добавить"}
          </Button>
        </Form.Item>
      </Form>
    </div>
  );
};
