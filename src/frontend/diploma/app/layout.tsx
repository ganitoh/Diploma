"use client";

import { Button, Empty, Input, List, Menu } from "antd";
import Layout, { Content, Footer, Header } from "antd/es/layout/layout";
import Link from "next/link";
import ReactQueryProvider from "./providers/ReactQueryProvider";
import { UserOutlined, SearchOutlined } from "@ant-design/icons";
import { usePathname, useRouter } from "next/navigation";
import { use, useEffect, useState } from "react";
import { IProductShort } from "./models/product";
import { serarchProducts } from "./http/products";
import { ChatSignalRProvider, useSignalR } from "./context/ChatSignalRContext";
import { checkRole } from "./http/user";

const defaultItems = [
  { key: "home", label: <Link href="/">Главная</Link> },
  {
    key: "analytics",
    label: "Аналитика",
    children: [
      {
        key: "orders",
        label: <Link href="/analytics/orders">Заказы</Link>,
      },
      {
        key: "products",
        label: <Link href="/analytics/products">Товары</Link>,
      },
    ],
  },
];

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  const connection = useSignalR();
  const [search, setSearch] = useState<string>("");
  const [items, setItems] = useState(defaultItems);
  const [searchShortProducts, setSearchShortProducts] = useState<
    IProductShort[]
  >([]);

  const pathname = usePathname();
  const router = useRouter();

  const handleAuthClick = () => {
    var userId = localStorage.getItem("userId");

    if (userId) {
      router.push(`/profile`);
    } else {
      router.push("/login");
    }
  };

  useEffect(() => {
    checkRole("Admin").then((res) => {
      if (res.succeeded) {
        setItems((prev) => [
          ...prev,
          {
            key: "supplierModule",
            label: <Link href="/supplier">Поставщики</Link>,
          },
        ]);
      }
    });
  }, []);

  useEffect(() => {
    setSearch("");
  }, [pathname]);

  useEffect(() => {
    if (search != "") {
      serarchProducts(search).then((res) => {
        setSearchShortProducts(res.response);
      });
    }
  }, [search]);

  return (
    <html lang="en">
      <body>
        <Layout style={{ minHeight: "100vh" }}>
          <Header
            className="headre"
            style={{
              borderRadius: "10px",
              display: "flex",
              justifyContent: "space-between",
              alignItems: "center",
            }}
          >
            <div style={{ flex: "0 0 auto", width: "400px" }}>
              <Menu theme="dark" mode="horizontal" items={items} />
            </div>
            <div
              style={{
                flex: "1 1 auto",
                display: "flex",
                justifyContent: "center",
                alignItems: "start",
                flexDirection: "column",
                position: "relative",
              }}
            >
              <Input
                placeholder="Поиск товаров..."
                onChange={(e) => setSearch(e.target.value)}
                prefix={<SearchOutlined />}
                style={{ maxWidth: 500, width: "100%" }}
              />

              {search && (
                <div
                  style={{
                    position: "absolute",
                    top: "40px",
                    width: "100%",
                    maxWidth: 500,
                    zIndex: 1000,
                    background: "#fff",
                    border: "1px solid #ddd",
                    borderRadius: 4,
                    boxShadow: "0 2px 8px rgba(0, 0, 0, 0.15)",
                  }}
                >
                  {searchShortProducts.length > 0 ? (
                    <List
                      size="small"
                      bordered
                      dataSource={searchShortProducts}
                      renderItem={(item) => (
                        <List.Item>
                          <Link href={`/product/${item.id}`}>{item.name}</Link>
                        </List.Item>
                      )}
                    />
                  ) : (
                    <div style={{ padding: "12px" }}>
                      <Empty
                        description="Ничего не найдено"
                        image={Empty.PRESENTED_IMAGE_SIMPLE}
                      />
                    </div>
                  )}
                </div>
              )}
            </div>
            <div style={{ flex: "0 0 auto" }}>
              <Button
                icon={<UserOutlined />}
                shape="circle"
                onClick={handleAuthClick}
              />
            </div>
          </Header>
          <ReactQueryProvider>
            <ChatSignalRProvider>
              <Content style={{ padding: "0 48" }}>{children}</Content>
            </ChatSignalRProvider>
          </ReactQueryProvider>
          <Footer style={{ textAlign: "center" }}>
            Diploma AMTI Created by Vadim Gan 2025
          </Footer>
        </Layout>
      </body>
    </html>
  );
}
