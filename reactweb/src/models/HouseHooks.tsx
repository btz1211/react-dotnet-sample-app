import { House } from "./House";
import config from "../config";
import { useNavigate } from "react-router-dom";
import { useMutation, useQuery, useQueryClient } from "react-query";
import axios, { AxiosError, AxiosResponse } from "axios";
import Problem from "../components/util/Problem";

export const useFetchHouses = () => {
  return useQuery<House[], AxiosError>("houses", () =>
    axios.get(`${config.baseApiUrl}/houses`).then((resp) => resp.data)
  );
};

export const useFetchHouse = (id: string) => {
  return useQuery<House, AxiosError>(["houses", id], () =>
    axios.get(`${config.baseApiUrl}/houses/${id}`).then((resp) => resp.data)
  );
};

export const useAddHouse = () => {
  const nav = useNavigate();
  const queryClient = useQueryClient();

  return useMutation<AxiosResponse, AxiosError<Problem>, House>(
    (h) => axios.post(`${config.baseApiUrl}/houses`, h),
    {
      onSuccess: () => {
        // invalidates the cache
        queryClient.invalidateQueries("houses");
        nav("/");
      },
    }
  );
};

export const useUpdateHouse = () => {
  const nav = useNavigate();
  const queryClient = useQueryClient();

  return useMutation<AxiosResponse, AxiosError<Problem>, House>(
    (h) => axios.put(`${config.baseApiUrl}/houses`, h),
    {
      onSuccess: (_, house) => {
        // invalidate cache
        queryClient.invalidateQueries("houses");
        nav(`/house/${house.id}`);
      },
    }
  );
};

export const useDeleteHouse = () => {
  const nav = useNavigate();
  const queryClient = useQueryClient();

  return useMutation<AxiosResponse, AxiosError, House>(
    (h) => axios.delete(`${config.baseApiUrl}/houses/${h.id}`),
    {
      onSuccess: () => {
        // invalidate cache
        queryClient.invalidateQueries("houses");
        nav(`/`);
      },
    }
  );
};
