export const base64ToArrayBuffer = (base64: string): ArrayBuffer => {
  const binaryString = window.atob(base64);
  const length = binaryString.length;
  const bytes = new Uint8Array(length);

  for (let i = 0; i < length; i++) {
    bytes[i] = binaryString.charCodeAt(i);
  }

  return bytes.buffer;
};

export const downloadFile = async (file: any) => {
  const blob = new Blob([base64ToArrayBuffer(file.response.content)], {
    type: file.response.mimeType,
  });
  const url = URL.createObjectURL(blob);
  const link = document.createElement("a");
  link.href = url;
  link.download = `${file.response.name}.pdf`;
  link.click();
  URL.revokeObjectURL(url);
};
