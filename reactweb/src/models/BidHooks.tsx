import axios, { AxiosResponse } from "axios";
import { Bid } from "./Bid";
import { useMutation, useQuery, useQueryClient } from "react-query";
import { AxiosError } from "axios";
import config from "../config";
import Problem from "../components/util/Problem";

export const useFetchBids = (houseId: string) => {
  return useQuery<Bid[], AxiosError>(["bids", houseId], () => 
    axios
      .get(`${config.baseApiUrl}/houses/${houseId}/bids`)
      .then((resp) => resp.data)
  );
};

export const useAddeBid = () => {
    const queryClient = useQueryClient();

    return useMutation<AxiosResponse, AxiosError<Problem>, Bid>(
        (b) => axios.post(`${config.baseApiUrl}/houses/${b.houseId}/bids`, b), {
            onSuccess: (_, bid) => {
                queryClient.invalidateQueries(["bids", bid.houseId]);
            }
        }
    );
};
