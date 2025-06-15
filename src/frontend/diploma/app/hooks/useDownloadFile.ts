import { useState } from "react";
import { IDownloadDocument, IResponse } from "../models/api";
import { useMessage } from "./useMessage";
import { base64ToArrayBuffer } from "../utils/download";

const DOWNLOAD_FILE_KEY = "certificationBodies";

export const useDownloadFile = (
  fetch: () => Promise<IResponse<IDownloadDocument>>
) => {
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [isSuccess, setIsSuccess] = useState<boolean>(false);
  const { Success, Error, Process } = useMessage(DOWNLOAD_FILE_KEY);

  const download = async () => {
    setIsLoading(true);
    Process();

    try {
      const data = await fetch();

      const blob = new Blob([base64ToArrayBuffer(data.response.content)], {
        type: "application/pdf",
      });
      const url = window.URL.createObjectURL(blob);
      const link = document.createElement("a");
      link.href = url;
      link.setAttribute("download", data.response.fileName);

      document.body.appendChild(link);

      link.click();

      link.parentNode?.removeChild(link);

      setIsSuccess(true);
      Success();
    } catch (e: any) {
      console.log(e);
      setIsSuccess(false);
      Error(e);
    } finally {
      setIsLoading(false);
    }
  };

  return {
    download,
    isLoading,
    isSuccess,
  };
};
