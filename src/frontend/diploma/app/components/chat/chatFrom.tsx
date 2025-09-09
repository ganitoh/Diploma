import { Button, Empty, Input, List } from "antd";
import { SendOutlined } from "@ant-design/icons";
import { useEffect, useRef, useState } from "react";
import { useSignalR } from "@/app/context/ChatSignalRContext";
import { ICreateMessage, IMessage } from "@/app/models/message";
import { IOrder } from "@/app/models/order";
import { useGetChatByOrderIdQuery } from "@/app/hooks/chat/useChatQuery";
import { getCookie } from "@/app/utils/cookie";

interface ChatFormProps {
  order: IOrder;
  isSellOrder: boolean;
}

export const ChatForm = ({ order, isSellOrder }: ChatFormProps) => {
  const [message, setMessage] = useState("");
  const connection = useSignalR();
  const { data, refetch } = useGetChatByOrderIdQuery(order.id);

  const myUserId = localStorage.getItem("userId");

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
      chatId: data?.response.id ?? 1,
      orderId: order.id,
      Text: message,
      userId: "1",
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
        dataSource={data?.response.messages}
        renderItem={(msg: IMessage, idx: number) => {
          const isMe = msg.userId === myUserId;
          return (
            <List.Item
              key={msg.Id || idx}
              style={{
                justifyContent: isMe ? "flex-end" : "flex-start",
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
                  background: isMe ? "#e6f7ff" : "#f6ffed",
                  alignSelf: isMe ? "flex-end" : "flex-start",
                  textAlign: isMe ? "right" : "left",
                  boxShadow: "0 1px 4px #0001",
                  marginLeft: isMe ? "auto" : 0,
                  marginRight: isMe ? 0 : "auto",
                }}
              >
                <div style={{ fontSize: 12, color: "#888" }}>
                  {isMe
                    ? "Вы"
                    : isSellOrder
                    ? order.buyerOrganizationName
                    : order.sellerOrganizationName}
                </div>
                <div style={{ fontSize: 16 }}>{msg.text}</div>
              </div>
            </List.Item>
          );
        }}
        style={{ marginBottom: 16, overflowY: "auto" }}
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
