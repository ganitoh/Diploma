"use client";

import { FC, useEffect, createContext, useState, useContext } from "react";
import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import { getCookie } from "../utils/cookie";

export const ChatSignalRContext = createContext<HubConnection | null>(null);

export const ChatSignalRProvider: FC<any> = ({ children }) => {
  const [connection, setConnection] = useState<HubConnection | null>(null);

  useEffect(() => {
    const token = getCookie("access_token");
    console.log("Токен:", token);

    if (!token) {
      console.warn("Нет токена — SignalR не подключается!");
      return;
    }

    const newConnection = new HubConnectionBuilder()
      .withUrl(`${process.env.NEXT_PUBLIC_CHAT_APP_API_URL}/hubs/chat`, {
        accessTokenFactory: () => token,
      })
      .withAutomaticReconnect()
      .build();

    newConnection
      .start()
      .then(() => {
        console.log("Текущее состояние подключения:", newConnection.state);
        setConnection(newConnection);
      })
      .catch((error) => {
        console.error("Ошибка подключения SignalR:", error);
      });

    // Чистим соединение при размонтировании
    return () => {
      if (newConnection) {
        newConnection.stop();
      }
    };
  }, []);

  // Отписка при закрытии окна
  useEffect(() => {
    const handleBeforeUnload = async () => {
      if (connection) await connection.stop();
    };

    window.addEventListener("beforeunload", handleBeforeUnload);
    return () => window.removeEventListener("beforeunload", handleBeforeUnload);
  }, [connection]);

  return (
    <ChatSignalRContext.Provider value={connection}>
      {children}
    </ChatSignalRContext.Provider>
  );
};

export const useSignalR = () => {
  return useContext(ChatSignalRContext);
};
