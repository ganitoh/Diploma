"use client";

import { Button } from "antd";
import { useRouter } from "next/navigation";

export default function OrganizationPage() {

    const router = useRouter()

    return(
        <div>
            <h1>Organization</h1>
            <Button onClick={() => router.push("/")}>получить организации</Button>
        </div>
    );
}