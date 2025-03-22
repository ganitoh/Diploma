import { Layout, Menu } from "antd";
import { Content, Footer, Header } from "antd/es/layout/layout";
import Link from "next/link";

const items = [
  { key: "home", label: <Link href={"/"}>Home</Link> },
  { key: "organization", label: <Link href={"/organization"}>Organization</Link> }
]

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en">
      <body>
        <Layout style={{ minHeight : "100vh" }}>
          <Header style={{borderRadius: "10px"}}>
            <Menu theme="dark" mode="horizontal" items={items} style={{flex: 1, minWidth: 0}}/>
          </Header>
          <Content style={{ padding: "0 48px"}}> {children} </Content>
          <Footer style={{ textAlign: "center"}}>Diploma 2025 AMTI Created by Vadim Gan</Footer>
        </Layout>
      </body>
    </html>
  );
}
