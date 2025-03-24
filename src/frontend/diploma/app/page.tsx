"use client";

import { Button } from "antd";

import { useRouter } from "next/navigation";

export default function Home() {
  const router = useRouter()
  return (
  <div>
    <h1>Main page</h1>
    <Button onClick={() => router.push("/organization")}>home</Button>
  </div>
  );
}
