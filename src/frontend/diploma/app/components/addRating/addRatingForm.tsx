import { Button, Card, Form, Input, List, Rate, Typography } from "antd";
import { useRouter } from "next/navigation";
import { useGetRatingForEntityQuery } from "@/app/hooks/rating/useRatingQuery";
import { useRatingMutation } from "@/app/hooks/rating/useRatingMutation";

const { Text } = Typography;

interface IAddRatinFromValues {
  comment: string;
  rating: number;
}

interface IAddRatingFormProps {
  isProduct: boolean;
  entityId: number;
}

export const AddRatingForm = (props: IAddRatingFormProps) => {
  const router = useRouter();
  const [form] = Form.useForm<IAddRatinFromValues>();

  const { data, isLoading, refetch } = useGetRatingForEntityQuery(
    props.entityId,
    props.isProduct
  );

  const { createRatingMutation } = useRatingMutation();

  const onFinish = async (data: IAddRatinFromValues) => {
    var res = await createRatingMutation.mutateAsync({
      ratingValue: data.rating,
      commentary: data.comment,
      isProduct: props.isProduct,
      entityId: props.entityId,
    });

    form.setFieldValue("comment", "");
    form.setFieldValue("rating", 0);
    refetch();
  };

  return (
    <div>
      <Card title="Оставьте отзыв о товаре" style={{ marginTop: 24 }}>
        <Form layout="vertical" form={form} onFinish={onFinish}>
          <Form.Item
            name="rating"
            label="Ваша оценка"
            rules={[{ required: true, message: "Пожалуйста, укажите оценку" }]}
          >
            <Rate allowClear defaultValue={0} />
          </Form.Item>

          <Form.Item
            name="comment"
            label="Ваш отзыв"
            rules={[{ required: true, message: "Пожалуйста, напишите отзыв" }]}
          >
            <Input.TextArea
              rows={4}
              placeholder="Поделитесь впечатлением о товаре..."
            />
          </Form.Item>

          <Form.Item>
            <Button type="primary" htmlType="submit">
              Оставить отзыв
            </Button>
          </Form.Item>
        </Form>
      </Card>
      <Card title="Отзывы других пользователей" style={{ marginTop: 24 }}>
        {data?.response.commentaries.length === 0 ? (
          <Text type="secondary">Пока нет отзывов</Text>
        ) : (
          <List
            itemLayout="vertical"
            dataSource={data?.response.commentaries}
            renderItem={(review) => (
              <List.Item key={review.id}>
                <List.Item.Meta
                  title={
                    <>
                      <strong>{review.userName}</strong>{" "}
                      <Rate disabled defaultValue={review.ratingValue} />
                    </>
                  }
                  description={
                    <Typography.Text type="secondary">
                      {review.createDate}
                    </Typography.Text>
                  }
                />
                <div>{review.commentary}</div>
              </List.Item>
            )}
          />
        )}
      </Card>
    </div>
  );
};
