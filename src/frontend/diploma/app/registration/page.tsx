"use client";

import { Button } from "antd/es/radio";

export default function RegistrationPage() {

    const reg = () =>{
        var response = fetch(`${process.env.NEXT_PUBLIC_IDENTITY_API_URL}/Registration?password=${123}&email=${'emial@email.com'}`)
    }

    return (
     <div>
      <h1>Registration</h1>
      <Button onClick={() => reg()}>Регистрация</Button>
     </div>
    );
  }
  