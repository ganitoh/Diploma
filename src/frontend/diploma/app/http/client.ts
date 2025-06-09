import axios from "axios";

function createAuthenticatedClient(baseURL: string) {
  const client = axios.create({
    baseURL,
    headers: {
      "Content-Type": "application/json",
      Accept: "application/json",
    },
    withCredentials: true,
  });

  client.interceptors.response.use(
    (res) => res,
    async (err) => {
      if (err.response?.status === 401) {
        try {
          await axios.post(
            `${process.env.NEXT_PUBLIC_IDENTITY_APP_API_URL!}/Identity/Refresh`,
            {},
            { withCredentials: true }
          );
          return client(err.config); // retry original
        } catch {
          window.location.href = "/login";
        }
      }

      return Promise.reject(err);
    }
  );

  return client;
}

export const organizaitonClient = createAuthenticatedClient(
  process.env.NEXT_PUBLIC_ORGANIZATION_APP_API_URL!
);
export const identityClient = createAuthenticatedClient(
  process.env.NEXT_PUBLIC_IDENTITY_APP_API_URL!
);
