"use client";

import { Button, Input, Menu } from "antd";
import Layout, { Content, Footer, Header } from "antd/es/layout/layout";
import Link from "next/link";
import ReactQueryProvider from "./providers/ReactQueryProvider";
import { UserOutlined, SearchOutlined } from "@ant-design/icons";
import { useRouter } from "next/navigation";

const items = [
  {key : "home" , label : <Link href={"/"}>Home</Link>} ,
  {key : "organization" , label : <Link href={"/organization"}>Organization</Link>} ]

  
export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {

  const router = useRouter();

  const handleAuthClick = () => {
    var userId = localStorage.getItem("userId")

    if (userId){
      router.push(`/profile/${userId}`);
    }
    else{
      router.push("/login");
    }
  };

  return (
    <html lang="en">
      <body>
        <Layout style={{minHeight: "100vh"}}>
          <Header className="headre" style={{
            borderRadius: "10px",
            display: "flex",
            justifyContent: "space-between",
            alignItems: "center",}}>
            <div style={{ flex: "0 0 auto", width : "400px" }}>
              <Menu theme="dark" mode="horizontal" items={items} />
            </div>
            <div
              style={{
                flex: "1 1 auto",
                display: "flex",
                justifyContent: "left",
                padding: "0 16px",
                minWidth: 0,
              }}
              >
                <Input
                placeholder="Поиск товаров..."
                prefix={<SearchOutlined />}
                style={{ maxWidth: 500, width: "100%" }}
                />
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
            <Content style={{padding: "0 48"}}>
              {children}
            </Content>
          </ReactQueryProvider>
          <Footer style={{textAlign: "center"}}>Diploma AMTI Created by Vadim Gan 2025</Footer>
        </Layout>
      </body>
    </html>
  );
}
