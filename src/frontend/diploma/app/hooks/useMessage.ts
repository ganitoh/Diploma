import { message } from "antd"
import { NoticeType } from "antd/es/message/interface"

export const useMessage = (key: string) => {
  const Success = () => {
    alert("Успешно!")
    message.open({
      key: key,
      type: "success",
      content: "Успешно!",
      duration: 3
    })
  }

  const Error = (e: any, duration: number = 3) => {
    message.open({
      key: key,
      type: "error",
      content: e.response.data.message,
      duration: duration
    })
  }

  const Process = () => {
    message.open({
      key: key,
      type: "loading",
      content: "Обработка...",
      duration: 0
    })
  }

  const Message = (content: string, type: NoticeType) => {
    message.open({
      key: key,
      type: type,
      content: content,
      duration: 7
    })
  }

  return {
    Success,
    Error,
    Process,
    Message
  }
}
