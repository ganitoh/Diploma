import { Button, Card, Form, Input, Typography } from "antd";
import { useRouter } from "next/navigation";
import { useState } from "react";
import styles from "./page.module.css"

const { Title, Text } = Typography;

interface IAdminPanelProps{
    userId?: string
}

export const AdminPanel = (props: IAdminPanelProps) => {
    const router = useRouter();

      return (
        <div className={styles.container}>
            <h1>Панель админа</h1>
        </div>
      );
}