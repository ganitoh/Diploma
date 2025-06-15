import { Button, Empty, Input, List } from "antd";
import { SendOutlined } from "@ant-design/icons";
import { useEffect, useState } from "react";
import { useSignalR } from "@/app/context/ChatSignalRContext";
import { ICreateMessage, IMessage } from "@/app/models/message";
import { IOrder } from "@/app/models/order";
import { useGetChatByOrderIdQuery } from "@/app/hooks/chat/useChatQuery";
import { getCookie } from "@/app/utils/cookie";

interface ChatFormProps {
  order: IOrder;
}

export const ChatForm = ({ order }: ChatFormProps) => {
  const [message, setMessage] = useState("");
  const connection = useSignalR();
  const { data, refetch } = useGetChatByOrderIdQuery(order.id);

  const myUserId = getCookie("access_token");

  useEffect(() => {
    if (!connection) return;

    const handleReceiveMessage = (message: IMessage) => {
      refetch();
    };

    connection.on("ReceiveMessagesAsync", handleReceiveMessage);

    return () => {
      connection.off("ReceiveMessagesAsync", handleReceiveMessage);
    };
  }, [connection]);

  const handleSendMessage = async () => {
    if (!message.trim()) return;
    if (!connection || connection.state !== "Connected") return;

    const dataToSend: ICreateMessage = {
      chatId: data?.response.id ?? 1, // поправь если надо
      orderId: order.id,
      Text: message,
    };

    try {
      await connection.invoke("SendMessage", dataToSend);
      setMessage("");
      refetch();
    } catch (err) {
      console.error("Ошибка отправки сообщения:", err);
    }
  };

  return (
    <div style={{ paddingBottom: 64 }}>
      <List
        size="small"
        dataSource={data?.response.Messages}
        renderItem={(msg: IMessage, idx: number) => {
          const isMe = msg.userId === myUserId;
          return (
            <List.Item
              key={msg.Id || idx}
              style={{
                justifyContent: isMe ? "flex-start" : "flex-end",
                display: "flex",
                padding: "4px 0",
                border: "none",
                background: "none",
              }}
            >
              <div
                style={{
                  maxWidth: "70%",
                  padding: "8px 16px",
                  borderRadius: 16,
                  background: isMe ? "#f6ffed" : "#e6f7ff",
                  alignSelf: isMe ? "flex-start" : "flex-end",
                  textAlign: isMe ? "left" : "right",
                  boxShadow: "0 1px 4px #0001",
                  marginLeft: isMe ? 0 : "auto",
                  marginRight: isMe ? "auto" : 0,
                }}
              >
                <div style={{ fontSize: 12, color: "#888" }}>
                  {isMe ? "Вы" : order.sellerOrganizationName}
                </div>
                <div style={{ fontSize: 16 }}>{msg.Text}</div>
              </div>
            </List.Item>
          );
        }}
        style={{ marginBottom: 16, maxHeight: 300, overflowY: "auto" }}
        locale={{
          emptyText: (
            <Empty
              description="Начните чат..."
              image={Empty.PRESENTED_IMAGE_SIMPLE}
            />
          ),
        }}
      />
      <div
        style={{
          position: "absolute",
          bottom: 0,
          left: 0,
          right: 0,
          padding: "12px",
          borderTop: "1px solid #f0f0f0",
          backgroundColor: "#fff",
        }}
      >
        <div style={{ display: "flex", gap: 8 }}>
          <Input
            value={message}
            onChange={(e) => setMessage(e.target.value)}
            placeholder="Введите сообщение..."
            onPressEnter={handleSendMessage}
          />
          <Button
            type="primary"
            icon={<SendOutlined />}
            onClick={handleSendMessage}
          />
        </div>
      </div>
    </div>
  );
};
